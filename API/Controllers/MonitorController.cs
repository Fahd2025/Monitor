using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MonitorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAppService _customerAppService;
        private readonly IMapper _mapper;
        public MonitorController(IUnitOfWork unitOfWork, ICustomerAppService customerAppService,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _customerAppService = customerAppService;
            _mapper = mapper;
        }

        [HttpGet("apps")]
        public async Task<ActionResult<IReadOnlyList<AppInfo>>> GetApps()
        {
            var _repoAppInfo = _unitOfWork.Repository<AppInfo>();
            var apps = await _repoAppInfo.ListAllAsync();
            return Ok(apps);
        }

        [HttpGet("apps/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppInfo>> GetApps(int id)
        {
            var _repoAppInfo = _unitOfWork.Repository<AppInfo>();
            var app = await _repoAppInfo.GetByIdAsync(id);
            return Ok(app);
        }

        [HttpGet("customers")]
        public async Task<ActionResult<IReadOnlyList<Customer>>> GetCustomers()
        {
            var _repoCustomer = _unitOfWork.Repository<Customer>();
            var customers = await _repoCustomer.ListAllAsync();
            return Ok(customers);
        }

        [HttpGet("customers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetCustomers(int id)
        {
             var _repoCustomer = _unitOfWork.Repository<Customer>();
            var customer = await _repoCustomer.GetByIdAsync(id);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<CustomerAppDto>>> GetCustomerApps([FromQuery] CustomerAppSpecParams customerAppSpecParams)
        {
            var _repoCustomerApp = _unitOfWork.Repository<CustomerApp>();

            var countSpec = new CustomerAppsSpecification(customerAppSpecParams, SpecJustForCount: true);
            var totalItemCount = await _repoCustomerApp.CountAsync(countSpec);

            var spec = new CustomerAppsSpecification(customerAppSpecParams);
            var customerApps = await _repoCustomerApp.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<CustomerAppDto>>(customerApps);

            return Ok(new Pagination<CustomerAppDto>(customerAppSpecParams.PageIndex, customerAppSpecParams.PageSize, totalItemCount, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerAppDto>> GetCustomerApp(int id)
        {
            var _repoCustomerApp = _unitOfWork.Repository<CustomerApp>();
            var spec = new CustomerAppsSpecification(id);
            var customerApp = await _repoCustomerApp.GetEntityWithSpec(spec);
            if (customerApp == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<CustomerAppDto>(customerApp));
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostCustomerApp(CustomerAppDto customerAppDto)
        {
            var newAppInfo = new AppInfo()
            {
                Id = customerAppDto.AppInfoId,
                Name = customerAppDto.AppInfo_Name,
                Description = customerAppDto.AppInfo_Description
            };

            var newCustomer = new Customer()
            {
                Id = customerAppDto.CustomerId,
                Name = customerAppDto.Customer_Name,
                Phone = customerAppDto.Customer_Phone,
                Address = customerAppDto.Customer_Address,
                TaxNumber = customerAppDto.Customer_TaxNumber,
                LogoUrl = customerAppDto.Customer_LogoUrl,
                Description = customerAppDto.Customer_Description
            };

            var customerApp = await _customerAppService.VerfiyCustomerAppAsync(customerAppDto.Id, newAppInfo, newCustomer,customerAppDto.InstallDate, customerAppDto.AppVersion, customerAppDto.AppSerial, customerAppDto.SysInfo);

            if (customerApp == null) return BadRequest(new ApiResponse(400, "Problem creating customer App"));

            return Ok(customerApp.Id);
        }
        
        [HttpGet("trackapps")]
        public async Task<ActionResult<IReadOnlyList<TrackAppDto>>> GetTrackApps([FromQuery] TrackAppSpecParams trackAppSpecParams)
        {
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();

            var countSpec = new TrackAppsSpecification(trackAppSpecParams, SpecJustForCount: true);
            var totalItemCount = await _repoTrackApp.CountAsync(countSpec);

            var spec = new TrackAppsSpecification(trackAppSpecParams);
            var trackApps = await _repoTrackApp.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<TrackAppDto>>(trackApps);

            return Ok(new Pagination<TrackAppDto>(trackAppSpecParams.PageIndex, trackAppSpecParams.PageSize, totalItemCount, data));
        }

        [HttpGet("trackapps/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<TrackAppDto>>> GetTrackApps(int id)
        {
            var _repoCustomerApp = _unitOfWork.Repository<CustomerApp>();
            var customerApp = await _repoCustomerApp.GetByIdAsync(id);
            if (customerApp == null) return NotFound(new ApiResponse(404));

            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();
            var spec = new TrackAppsSpecification(id);
            var trackApps = await _repoTrackApp.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<TrackAppDto>>(trackApps));
        }

        [HttpPost("trackapps")]
        public async Task<ActionResult<IReadOnlyList<TrackAppDto>>> PostTrackApp(TrackAppDto trackAppDto)
        {
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();
            var trackApps = await _repoTrackApp.ListAllAsync();
            return Ok(trackApps);           
        }

        [HttpDelete("trackapps/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrackApp>> DeleteTrackApps(int id)
        {
            var _repoCustomerApp = _unitOfWork.Repository<CustomerApp>();
            var customerApp = await _repoCustomerApp.GetByIdAsync(id);
            if (customerApp == null) return NotFound(new ApiResponse(404));
            
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();
            var spec = new TrackAppsSpecification(id, SpecJustForDelete:true);
            var trackApps = await _repoTrackApp.ListAsync(spec);
            _repoTrackApp.DeleteRange(trackApps);
            await _unitOfWork.Complete();
            return Ok(trackApps);
        }

        [HttpGet("trackapplogs")]
        public async Task<ActionResult<IReadOnlyList<TrackAppLog>>> GetTrackAppLogs([FromQuery] TrackAppLogSpecParams trackAppLogSpecParams)
        {
             var _repoTrackAppLog = _unitOfWork.Repository<TrackAppLog>();

            var countSpec = new TrackAppLogsSpecification(trackAppLogSpecParams, SpecJustForCount: true);
            var totalItemCount = await _repoTrackAppLog.CountAsync(countSpec);

            var spec = new TrackAppLogsSpecification(trackAppLogSpecParams);
            var trackAppLogs = await _repoTrackAppLog.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<TrackAppLogDto>>(trackAppLogs);

            return Ok(new Pagination<TrackAppLogDto>(trackAppLogSpecParams.PageIndex, trackAppLogSpecParams.PageSize, totalItemCount, data));
        }

        [HttpGet("trackapplogs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<TrackAppLogDto>>> GetTrackAppLogs(int id)
        {
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();
            var trackApp = await _repoTrackApp.GetByIdAsync(id);
            if (trackApp == null) return NotFound(new ApiResponse(404));

            var _repoTrackAppLog = _unitOfWork.Repository<TrackAppLog>();
            var spec = new TrackAppLogsSpecification(id);
            var trackAppLogs = await _repoTrackAppLog.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<TrackAppLogDto>>(trackAppLogs));
        }

        [HttpPost("trackapplogs")]
        public async Task<ActionResult<IReadOnlyList<TrackAppLog>>> PostTrackAppLog(TrackAppLogDto trackAppLogDto)
        {
            var _repoTrackAppLog = _unitOfWork.Repository<TrackAppLog>();
            var trackAppLogs = await _repoTrackAppLog.ListAllAsync();
            return Ok(trackAppLogs);
        }

        [HttpDelete("trackapplogs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<TrackAppLog>>> DeleteTrackAppLogs(int id)
        {
            var _repoTrackApp = _unitOfWork.Repository<TrackApp>();
            var trackApp = await _repoTrackApp.GetByIdAsync(id);
            if (trackApp == null) return NotFound(new ApiResponse(404));
            
            var _repoTrackAppLog = _unitOfWork.Repository<TrackAppLog>();
            var spec = new TrackAppLogsSpecification(id, SpecJustForDelete:true);
            var trackAppLogs = await _repoTrackAppLog.ListAsync(spec);
            _repoTrackAppLog.DeleteRange(trackAppLogs);
            await _unitOfWork.Complete();
            return Ok(trackAppLogs);
        }
    }
}