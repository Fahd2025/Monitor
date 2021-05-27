using System;

namespace API.Dtos
{
    public class CustomerAppDto
    {        
        public int Id { get; set; }
        public int AppInfoId { get; set; }
        public string AppInfo_Name { get; set; }
        public string AppInfo_Description { get; set; }       
        public int CustomerId { get; set; }
        public string Customer_Name { get; set; }        
        public string Customer_Phone { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_TaxNumber { get; set; }        
        public string Customer_LogoUrl { get; set; }       
        public string Customer_Description { get; set; }
        public DateTimeOffset InstallDate { get; set; }
        public string AppVersion { get; set; }
        public string AppSerial { get; set; }
        public string SysInfo { get; set; } 
        public string RemoteId { get; set; }
        public decimal Price { get; set; }
    }
}