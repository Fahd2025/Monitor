using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class TrackAppsSpecification : BaseSpecification<TrackApp>
    {
        public TrackAppsSpecification(TrackAppSpecParams trackAppSpecParams, bool SpecJustForCount = false ) : base(x =>
        (string.IsNullOrEmpty(trackAppSpecParams.Search) || x.AppLicense.ToLower().Contains(trackAppSpecParams.Search)|| x.CustomerApp.AppSerial.ToLower().Contains(trackAppSpecParams.Search)|| x.CustomerApp.RemoteId.ToLower().Contains(trackAppSpecParams.Search)) &&
        (!trackAppSpecParams.AppInfoId.HasValue || x.CustomerApp.AppInfoId == trackAppSpecParams.AppInfoId) &&
        (!trackAppSpecParams.CustomerId.HasValue || x.CustomerApp.CustomerId == trackAppSpecParams.CustomerId)
        )
        {
            if(SpecJustForCount) return;

            AddInclude(p => p.CustomerApp);
            AddInclude(p => p.CustomerApp.AppInfo);
            AddInclude(p => p.CustomerApp.Customer);
            AddOrderByDescending(p => p.Id);

            ApplyPaging(trackAppSpecParams.PageSize * (trackAppSpecParams.PageIndex - 1), trackAppSpecParams.PageSize);

            if (!string.IsNullOrEmpty(trackAppSpecParams.Sort))
            {
                switch (trackAppSpecParams.Sort)
                {
                    case "customer":
                        AddOrderBy(p => p.CustomerApp.CustomerId);
                        break;
                    case "customerDesc":
                        AddOrderByDescending(p => p.CustomerApp.CustomerId);
                        break;    
                    case "app":
                        AddOrderBy(p => p.CustomerApp.AppInfoId);
                        break;
                    case "appDesc":
                        AddOrderByDescending(p => p.CustomerApp.AppInfoId);
                        break;
                    default:
                        AddOrderByDescending(p => p.CustomerApp.InstallDate);
                        break;
                }
            }
        }

        public TrackAppsSpecification(int id, bool SpecJustForDelete = false) : base(p => p.CustomerAppId == id)
        {
            AddOrderByDescending(p => p.Id);

            if(SpecJustForDelete) return;

            AddInclude(p => p.CustomerApp);
            AddInclude(p => p.CustomerApp.AppInfo);
            AddInclude(p => p.CustomerApp.Customer);
           
        }
    }
}