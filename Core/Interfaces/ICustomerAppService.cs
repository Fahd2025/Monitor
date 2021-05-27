using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICustomerAppService
    {
        Task<AppInfo> VerfiyAppInfoAsync(AppInfo appInfo);

        Task<Customer> VerfiyCustomerAsync(Customer customer);

        Task<CustomerApp> VerfiyCustomerAppAsync(int id, AppInfo appInfo, Customer customer, DateTimeOffset installDate, string appVersion, string appSerial, string sysInfo);
    }
}