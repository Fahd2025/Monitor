using System;

namespace Core.Entities
{
    public class TrackAppLog : BaseEntity
    {        
        public TrackApp TrackApp { get; set; }
        public int TrackAppId { get; set; }
        public DateTimeOffset LogDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset SalesStartDate { get; set; } = DateTimeOffset.Now;
        public int SalesCount { get; set; } = 0;
        public decimal SalesTotal { get; set; } = 0;
    }
}