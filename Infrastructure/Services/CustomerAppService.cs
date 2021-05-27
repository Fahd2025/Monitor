using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AppInfo> VerfiyAppInfoAsync(AppInfo appInfo)
        {
            var _repoAppInfo = _unitOfWork.Repository<AppInfo>();

            // get by id
            AppInfo _app = await _repoAppInfo.GetByIdAsync(appInfo.Id);
            if (_app != null) return _app;

            // get by name
            _app = await _repoAppInfo.Query().Where(m => m.Name.Trim().ToLower() == appInfo.Name.Trim().ToLower()).FirstOrDefaultAsync();
            if (_app != null) return _app;

            _repoAppInfo.Add(appInfo);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return appInfo;
        }

        public async Task<Customer> VerfiyCustomerAsync(Customer customer)
        {
            var _repoCustomer = _unitOfWork.Repository<Customer>();

            // get by id
            Customer _customer = await _repoCustomer.GetByIdAsync(customer.Id);
            if (_customer != null) 
            {
                _customer = customer;
                 _repoCustomer.Update(_customer);
                await _unitOfWork.Complete();
                return _customer;
            }

            // get by tax number
            _customer = await _repoCustomer.Query().Where(m => !string.IsNullOrEmpty(customer.TaxNumber) && m.TaxNumber.Trim().ToLower() == customer.TaxNumber.Trim().ToLower()).FirstOrDefaultAsync();
            if (_customer != null) 
            {
                _customer.Name = customer.Name;
                _customer.Address = customer.Address;
                _customer.Phone = customer.Phone;
                _customer.TaxNumber = customer.TaxNumber;
                 _repoCustomer.Update(_customer);
                await _unitOfWork.Complete();
                return _customer;
            }

            if (string.IsNullOrEmpty(customer.TaxNumber))
            {
                // get by name
                _customer = await _repoCustomer.Query().Where(m => m.Name.Trim().ToLower() == customer.Name.Trim().ToLower()).FirstOrDefaultAsync();
                if (_customer != null) 
                {
                    _customer.Name = customer.Name;
                    _customer.Address = customer.Address;
                    _customer.Phone = customer.Phone;
                    _customer.TaxNumber = customer.TaxNumber;
                    _repoCustomer.Update(_customer);
                    await _unitOfWork.Complete();
                    return _customer;
                }
            }

            _repoCustomer.Add(customer);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return customer;
        }

        public async Task<CustomerApp> VerfiyCustomerAppAsync(int id, AppInfo appInfo, Customer customer, DateTimeOffset installDate, string appVersion, string appSerial, string sysInfo)
        {
            // 1- verfiy app
            var _appInfo = await VerfiyAppInfoAsync(appInfo);

            // 2- verfiy customer
            var _customer = await VerfiyCustomerAsync(customer);

            // 3- verfiy customer app
            var _repoCustomerApp = _unitOfWork.Repository<CustomerApp>();

            // get by id
            CustomerApp _customerApp = await _repoCustomerApp.GetByIdAsync(id);
            if (_customerApp != null && _customer.TaxNumber == customer.TaxNumber)
            {
                //_repoCustomerApp.Update(_customerApp);
            }
            else
            {
                // get by app serial
                _customerApp = await _repoCustomerApp.Query().Where(m => m.AppSerial.Trim().ToLower() == appSerial.Trim().ToLower()).FirstOrDefaultAsync();
                if (_customerApp != null && _customer.TaxNumber == customer.TaxNumber)
                {
                    //_repoCustomerApp.Update(_customerApp);
                }
                else
                {
                    // 4- add new customer app
                    _customerApp = new CustomerApp()
                    {
                        AppInfoId = _appInfo.Id,
                        CustomerId = _customer.Id,
                        InstallDate = installDate,
                        AppVersion = appVersion,
                        AppSerial = appSerial,
                        SysInfo = sysInfo
                    };

                    _repoCustomerApp.Add(_customerApp);
                }
            }
            // 4- save to db
            await _unitOfWork.Complete();

            // 5- add to Track app
           await VerfiyTrackAppAsync(_customerApp.Id,_customerApp.AppSerial, DateTime.UtcNow,0,0);

            // 6- return customer App
            return _customerApp;
        }

        public async Task<TrackApp> VerfiyTrackAppAsync(int customerAppId, string appLicense, DateTimeOffset salesStartDate, int salesCount, decimal SalesTotal)
        {
            // 1- verfiy track app
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();

            // get by customer app id
            TrackApp _trackApp = await _repoTrackApp.Query().Where(m => m.CustomerAppId == customerAppId).FirstOrDefaultAsync();
            if (_trackApp != null)
            {
                // update
                _trackApp.AppLicense = appLicense;
                _trackApp.LastCheckDate = DateTimeOffset.Now;
                _repoTrackApp.Update(_trackApp);
            }
            else
            {
                // new 
                _trackApp = new TrackApp();
                _trackApp.CustomerAppId = customerAppId;
                _trackApp.AppLicense = appLicense;
                _trackApp.LastCheckDate = DateTimeOffset.Now;
                _repoTrackApp.Add(_trackApp);
            }

            // 2- save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // 3- create track app log
            _unitOfWork.Repository<TrackAppLog>().Add(new TrackAppLog()
            {
                TrackAppId = _trackApp.Id,
                LogDate = DateTimeOffset.Now,
                SalesStartDate = salesStartDate,
                SalesCount = salesCount,
                SalesTotal = SalesTotal
            });

            await _unitOfWork.Complete();

            return _trackApp;
        }


        public async Task<TrackApp> UpdateAppStatus(int customerAppId, AppStatus status)
        {
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();

            // get by customer app id
            TrackApp _trackApp = await _repoTrackApp.Query().Where(m => m.CustomerAppId == customerAppId).FirstOrDefaultAsync();
            if (_trackApp != null)
            {
                // update
                switch (status)
                {
                    case AppStatus.Activated:
                        _trackApp.DoActivation = false;
                        break;
                    case AppStatus.DefenseActivated:
                        _trackApp.DoDefense = false;
                        break;
                }
                _trackApp.Status = status;

                _repoTrackApp.Update(_trackApp);
                await _unitOfWork.Complete();
            }

            return _trackApp;
        }
    }
}