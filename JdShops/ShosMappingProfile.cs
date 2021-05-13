using AutoMapper;
using JdShops.Entities;
using JdShops.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops
{
    public class ShosMappingProfile : Profile
    {
        public ShosMappingProfile()
        {
            CreateMap<Shops, ShopsDto>()
                .ForMember(m => m.DeliveryInfo, c => c.MapFrom(s => s.Address.DeliveryInfo))
                .ForMember(m => m.MapCoordinatesLongitude, c => c.MapFrom(s => s.Address.MapCoordinatesLongitude))
                .ForMember(m => m.MapCoordinatesLatitude, c => c.MapFrom(s => s.Address.MapCoordinatesLatitude));

            CreateMap<CreateShopDto, Shops>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address()
                {DeliveryInfo = dto.DeliveryInfo, MapCoordinatesLatitude = dto.MapCoordinatesLatitude, ShopNumber = dto.ShopNumber}))
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address()
                    { DeliveryInfo = dto.DeliveryInfo, MapCoordinatesLongitude = dto.MapCoordinatesLongitude, ShopNumber = dto.ShopNumber }));

            CreateMap<AdditionalAddress,AddAdditionalAddressDto> ()
                .ForMember(m => m.ShopNumber, c=>c.MapFrom(s=>s.ShopNumber))
                .ForMember(m => m.DeliveryInfo, c => c.MapFrom(s => s.DeliveryInfo))
                .ForMember(m => m.MapCoordinatesLongitude, c => c.MapFrom(s => s.MapCoordinatesLongitude))
                .ForMember(m => m.MapCoordinatesLatitude, c => c.MapFrom(s => s.MapCoordinatesLatitude))
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description));
            CreateMap<AddAdditionalAddressDto, AdditionalAddress>()
                .ForMember(m => m.ShopNumber, c => c.MapFrom(s => s.ShopNumber))
                .ForMember(m => m.DeliveryInfo, c => c.MapFrom(s => s.DeliveryInfo))
                .ForMember(m => m.MapCoordinatesLongitude, c => c.MapFrom(s => s.MapCoordinatesLongitude))
                .ForMember(m => m.MapCoordinatesLatitude, c => c.MapFrom(s => s.MapCoordinatesLatitude))
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description));
            CreateMap<RegisterUserDto, User>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Email, c => c.MapFrom(s => s.Email))
                .ForMember(m => m.Fname, c => c.MapFrom(s => s.Fname))
                .ForMember(m => m.Lname, c => c.MapFrom(s => s.Lname))
                .ForMember(m => m.RoleId, c => c.MapFrom(s => s.RoleId));
            CreateMap<Tickets, TicketsDto>()
                .ForMember(m=>m.TypeOfRequest, c=>c.MapFrom(s=>s.TypeOfRequest))
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.ShopNumber, c => c.MapFrom(s => s.ShopNumber))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Status))
                .ForMember(m => m.UserLname, c => c.MapFrom(s => s.UserLname))
                .ForMember(m => m.UserFname, c => c.MapFrom(s => s.UserFname))
                .ForMember(m => m.CreatedByUser, c => c.MapFrom(s => s.CreatedByUser))
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.UserId))
                ;
            CreateMap<TicketsDto, Tickets>()
                .ForMember(m => m.TypeOfRequest, c => c.MapFrom(s => s.TypeOfRequest))
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.ShopNumber, c => c.MapFrom(s => s.ShopNumber))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Status))
                .ForMember(m => m.UserLname, c => c.MapFrom(s => s.UserLname))
                .ForMember(m => m.UserFname, c => c.MapFrom(s => s.UserFname))
                .ForMember(m => m.CreatedByUser, c => c.MapFrom(s => s.CreatedByUser))
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.UserId))
                ;

        }

    }
}
