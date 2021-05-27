using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class TrackApp : BaseEntity
    {
        public CustomerApp CustomerApp { get; set; }
        public int CustomerAppId { get; set; }
        public DateTimeOffset LastCheckDate { get; set; } = DateTimeOffset.Now;
        public string AppLicense { get; set; }
        public bool DoActivation { get; set; } = false;
        public bool DoDefense { get; set; } = false;
        public AppStatus Status { get; set; } = AppStatus.NotSet;
        public IReadOnlyList<TrackAppLog> TrackAppLogs { get; set; }

    }
}