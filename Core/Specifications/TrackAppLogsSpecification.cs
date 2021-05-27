using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class TrackAppLogsSpecification : BaseSpecification<TrackAppLog>
    {
        public TrackAppLogsSpecification(TrackAppLogSpecParams trackAppSpecParams, bool SpecJustForCount = false ) : base(x =>
        (string.IsNullOrEmpty(trackAppSpecParams.Search) || x.TrackApp.AppLicense.ToLower().Contains(trackAppSpecParams.Search)|| x.TrackApp.CustomerApp.AppSerial.ToLower().Contains(trackAppSpecParams.Search)|| x.TrackApp.CustomerApp.RemoteId.ToLower().Contains(trackAppSpecParams.Search)) &&
        (!trackAppSpecParams.AppInfoId.HasValue || x.TrackApp.CustomerApp.AppInfoId == trackAppSpecParams.AppInfoId) &&
        (!trackAppSpecParams.CustomerId.HasValue || x.TrackApp.CustomerApp.CustomerId == trackAppSpecParams.CustomerId)
        )
        {
            if(SpecJustForCount) return;

            AddInclude(p => p.TrackApp);
            AddInclude(p => p.TrackApp.CustomerApp);
            AddInclude(p => p.TrackApp.CustomerApp.AppInfo);
            AddInclude(p => p.TrackApp.CustomerApp.Customer);
            AddOrderByDescending(p => p.Id);

            ApplyPaging(trackAppSpecParams.PageSize * (trackAppSpecParams.PageIndex - 1), trackAppSpecParams.PageSize);

            if (!string.IsNullOrEmpty(trackAppSpecParams.Sort))
            {
                switch (trackAppSpecParams.Sort)
                {
                    case "customer":
                        AddOrderBy(p => p.TrackApp.CustomerApp.CustomerId);
                        break;
                    case "customerDesc":
                        AddOrderByDescending(p => p.TrackApp.CustomerApp.CustomerId);
                        break;    
                    case "app":
                        AddOrderBy(p => p.TrackApp.CustomerApp.AppInfoId);
                        break;
                    case "appDesc":
                        AddOrderByDescending(p => p.TrackApp.CustomerApp.AppInfoId);
                        break;
                    default:
                        AddOrderByDescending(p => p.TrackApp.CustomerApp.InstallDate);
                        break;
                }
            }
        }

        public TrackAppLogsSpecification(int id, bool SpecJustForDelete = false) : base(p => p.TrackAppId == id)
        {
            AddOrderByDescending(p => p.Id);

            if(SpecJustForDelete) return;

            AddInclude(p => p.TrackApp);
            AddInclude(p => p.TrackApp.CustomerApp);
            AddInclude(p => p.TrackApp.CustomerApp.AppInfo);
            AddInclude(p => p.TrackApp.CustomerApp.Customer);            
        }
    }
}