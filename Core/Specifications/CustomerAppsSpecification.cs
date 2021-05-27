using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class CustomerAppsSpecification : BaseSpecification<CustomerApp>
    {
        public CustomerAppsSpecification(CustomerAppSpecParams customerAppSpecParams, bool SpecJustForCount = false ) : base(x =>
        (string.IsNullOrEmpty(customerAppSpecParams.Search) || x.AppSerial.ToLower().Contains(customerAppSpecParams.Search)|| x.RemoteId.ToLower().Contains(customerAppSpecParams.Search)) &&
        (!customerAppSpecParams.AppInfoId.HasValue || x.AppInfoId == customerAppSpecParams.AppInfoId) &&
        (!customerAppSpecParams.CustomerId.HasValue || x.CustomerId == customerAppSpecParams.CustomerId)
        )
        {
            if(SpecJustForCount) return;

            AddInclude(p => p.AppInfo);
            AddInclude(p => p.Customer);
            AddOrderByDescending(p => p.Id);

            ApplyPaging(customerAppSpecParams.PageSize * (customerAppSpecParams.PageIndex - 1), customerAppSpecParams.PageSize);

            if (!string.IsNullOrEmpty(customerAppSpecParams.Sort))
            {
                switch (customerAppSpecParams.Sort)
                {
                    case "customer":
                        AddOrderBy(p => p.CustomerId);
                        break;
                    case "customerDesc":
                        AddOrderByDescending(p => p.CustomerId);
                        break;    
                    case "app":
                        AddOrderBy(p => p.AppInfoId);
                        break;
                    case "appDesc":
                        AddOrderByDescending(p => p.AppInfoId);
                        break;
                    default:
                        AddOrderByDescending(p => p.InstallDate);
                        break;
                }
            }
        }

        public CustomerAppsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.AppInfo);
            AddInclude(p => p.Customer);
        }
    }
}