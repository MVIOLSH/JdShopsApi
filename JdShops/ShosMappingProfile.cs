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
        }

    }
}
