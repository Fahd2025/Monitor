using System;

namespace Core.Entities
{
    public class CustomerApp : BaseEntity
    {
        public CustomerApp()
        {
        }

        public CustomerApp(int appInfoId, int customerId, string appVersion, string appSerial, string sysInfo)
        {
            AppInfoId = appInfoId;
            CustomerId = customerId;
            AppVersion = appVersion;
            AppSerial = appSerial;
            SysInfo = sysInfo;
        }

        public AppInfo AppInfo { get; set; }
        public int AppInfoId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTimeOffset InstallDate { get; set; } = DateTimeOffset.Now;
        public string AppVersion { get; set; }
        public string AppSerial { get; set; }
        public string SysInfo { get; set; } 
        public string RemoteId { get; set; }
        public decimal Price { get; set; }
    }
}