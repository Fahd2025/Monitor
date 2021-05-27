using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReturnToDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

            CreateMap<CustomerApp, CustomerAppDto>()
                .ForMember(d => d.AppInfo_Name , o => o.MapFrom(s => s.AppInfo.Name))
                .ForMember(d => d.AppInfo_Description , o => o.MapFrom(s => s.AppInfo.Description))
                .ForMember(d => d.Customer_Name , o => o.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.Customer_Phone , o => o.MapFrom(s => s.Customer.Phone))
                .ForMember(d => d.Customer_Address , o => o.MapFrom(s => s.Customer.Address))
                .ForMember(d => d.Customer_TaxNumber , o => o.MapFrom(s => s.Customer.TaxNumber))              
                .ForMember(d => d.Customer_LogoUrl , o => o.MapFrom(s => s.Customer.LogoUrl))
                .ForMember(d => d.Customer_Description , o => o.MapFrom(s => s.Customer.Description)); 

            CreateMap<TrackApp, TrackAppDto>()
                .ForMember(d => d.AppInfoId , o => o.MapFrom(s => s.CustomerApp.AppInfo.Id))
                .ForMember(d => d.AppInfo_Name , o => o.MapFrom(s => s.CustomerApp.AppInfo.Name))
                .ForMember(d => d.AppInfo_Description , o => o.MapFrom(s => s.CustomerApp.AppInfo.Description))
                .ForMember(d => d.CustomerId , o => o.MapFrom(s => s.CustomerApp.Customer.Id))
                .ForMember(d => d.Customer_Name , o => o.MapFrom(s => s.CustomerApp.Customer.Name))
                .ForMember(d => d.Customer_Phone , o => o.MapFrom(s => s.CustomerApp.Customer.Phone))
                .ForMember(d => d.Customer_Address , o => o.MapFrom(s => s.CustomerApp.Customer.Address))
                .ForMember(d => d.Customer_TaxNumber , o => o.MapFrom(s => s.CustomerApp.Customer.TaxNumber))              
                .ForMember(d => d.Customer_LogoUrl , o => o.MapFrom(s => s.CustomerApp.Customer.LogoUrl))
                .ForMember(d => d.Customer_Description , o => o.MapFrom(s => s.CustomerApp.Customer.Description))
                .ForMember(d => d.CustomerAppId , o => o.MapFrom(s => s.CustomerApp.Id))
                .ForMember(d => d.CustomerApp_InstallDate , o => o.MapFrom(s => s.CustomerApp.InstallDate))
                .ForMember(d => d.CustomerApp_AppVersion , o => o.MapFrom(s => s.CustomerApp.AppVersion))
                .ForMember(d => d.CustomerApp_AppSerial , o => o.MapFrom(s => s.CustomerApp.AppSerial))
                .ForMember(d => d.CustomerApp_SysInfo , o => o.MapFrom(s => s.CustomerApp.SysInfo))
                .ForMember(d => d.CustomerApp_RemoteId , o => o.MapFrom(s => s.CustomerApp.RemoteId))
                .ForMember(d => d.CustomerApp_Price , o => o.MapFrom(s => s.CustomerApp.Price)); 

            CreateMap<TrackAppLog, TrackAppLogDto>()
                .ForMember(d => d.AppInfoId , o => o.MapFrom(s => s.TrackApp.CustomerApp.AppInfo.Id))
                .ForMember(d => d.AppInfo_Name , o => o.MapFrom(s => s.TrackApp.CustomerApp.AppInfo.Name))
                .ForMember(d => d.AppInfo_Description , o => o.MapFrom(s => s.TrackApp.CustomerApp.AppInfo.Description))
                .ForMember(d => d.CustomerId , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.Id))
                .ForMember(d => d.Customer_Name , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.Name))
                .ForMember(d => d.Customer_Phone , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.Phone))
                .ForMember(d => d.Customer_Address , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.Address))
                .ForMember(d => d.Customer_TaxNumber , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.TaxNumber))              
                .ForMember(d => d.Customer_LogoUrl , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.LogoUrl))
                .ForMember(d => d.Customer_Description , o => o.MapFrom(s => s.TrackApp.CustomerApp.Customer.Description))
                .ForMember(d => d.CustomerApp_InstallDate , o => o.MapFrom(s => s.TrackApp.CustomerApp.InstallDate))
                .ForMember(d => d.CustomerAppId , o => o.MapFrom(s => s.TrackApp.CustomerApp.Id))
                .ForMember(d => d.CustomerApp_AppVersion , o => o.MapFrom(s => s.TrackApp.CustomerApp.AppVersion))
                .ForMember(d => d.CustomerApp_AppSerial , o => o.MapFrom(s => s.TrackApp.CustomerApp.AppSerial))
                .ForMember(d => d.CustomerApp_SysInfo , o => o.MapFrom(s => s.TrackApp.CustomerApp.SysInfo))
                .ForMember(d => d.CustomerApp_RemoteId , o => o.MapFrom(s => s.TrackApp.CustomerApp.RemoteId))
                .ForMember(d => d.CustomerApp_Price , o => o.MapFrom(s => s.TrackApp.CustomerApp.Price))
                .ForMember(d => d.TrackApp_LastCheckDate , o => o.MapFrom(s => s.TrackApp.LastCheckDate))
                .ForMember(d => d.TrackApp_AppLicense , o => o.MapFrom(s => s.TrackApp.AppLicense))
                .ForMember(d => d.TrackApp_DoActivation , o => o.MapFrom(s => s.TrackApp.DoActivation))
                .ForMember(d => d.TrackApp_DoDefense , o => o.MapFrom(s => s.TrackApp.DoDefense))
                .ForMember(d => d.TrackApp_Status , o => o.MapFrom(s => s.TrackApp.Status));      
        }
    }
}