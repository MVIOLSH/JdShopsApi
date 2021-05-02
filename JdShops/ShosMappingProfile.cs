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
                .ForMember(m => m.MapCoordinates, c => c.MapFrom(s => s.Address.MapCoordinates));

            CreateMap<CreateShopDto, Shops>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address()
                {DeliveryInfo = dto.DeliveryInfo, MapCoordinates = dto.MapCoordinates, ShopNumber = dto.ShopNumber}));

        }

    }
}
