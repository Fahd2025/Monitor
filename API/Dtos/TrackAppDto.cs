using System;
using System.Collections.Generic;
using Core.Entities;

namespace API.Dtos
{
    public class TrackAppDto
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
        public int CustomerAppId { get; set; }
        public DateTimeOffset CustomerApp_InstallDate { get; set; } = DateTimeOffset.Now;
        public string CustomerApp_AppVersion { get; set; }
        public string CustomerApp_AppSerial { get; set; }
        public string CustomerApp_SysInfo { get; set; } 
        public string CustomerApp_RemoteId { get; set; }
        public decimal CustomerApp_Price { get; set; }
        public DateTimeOffset LastCheckDate { get; set; } = DateTimeOffset.Now;
        public string AppLicense { get; set; }
        public bool DoActivation { get; set; } = false;
        public bool DoDefense { get; set; } = false;
        public AppStatus Status { get; set; } = AppStatus.NotSet;
    }
}