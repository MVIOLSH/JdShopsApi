using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JdShops.Entities;
using JdShops.Exceptions;
using JdShops.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JdShops.Services
{
    public interface IAdditionalAddressService
    {
        int Create(int shopNumber, AddAdditionalAddressDto dto);
        IEnumerable<AddAdditionalAddressDto> GetAllAdditionalForShop(int shopNumber);
        void AdditionalAddressDelete(int id, int shopNumber);
        void AdditionalAddressUpdate(int id, int shopNumber, AddAdditionalAddressDto dto);
        AddAdditionalAddressDto GetAdditionalAddressById(int shopNumber, int id);

    }

    public class AdditionalAddressService : JdShops.Services.IAdditionalAddressService
    {
        private readonly ShopsDBContext _dbContext;
        private readonly IMapper _mapper;


        public AdditionalAddressService(ShopsDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public int Create(int shopNumber, AddAdditionalAddressDto dto)
        {
            var shop = _dbContext.Shops.FirstOrDefault(r => r.ShopNumber == shopNumber);
            if (shop is null) throw new NotFoundException("Shop Not Found!");

            var addressEntity = _mapper.Map<AdditionalAddress>(dto);
            _dbContext.AdditionalAddresses.Add(addressEntity);
            _dbContext.SaveChanges();
            return addressEntity.Id;
        }

        public IEnumerable<AddAdditionalAddressDto> GetAllAdditionalForShop(int shopNumber)
        {
            var shop = _dbContext.Shops.FirstOrDefault(r => r.ShopNumber == shopNumber);
            if (shop is null) throw new NotFoundException("Shop Not Found!");

            var additionalAddresses = _dbContext
                .AdditionalAddresses
                .Where(r=>r.ShopNumber == shop.ShopNumber)
                .ToList();
          

            var additionalsDtos = _mapper.Map<List<AddAdditionalAddressDto>>(additionalAddresses);
            return additionalsDtos;
        }

        public AddAdditionalAddressDto GetAdditionalAddressById(int shopNumber, int id)
        {
            var shop = _dbContext.Shops.FirstOrDefault(r => r.ShopNumber == shopNumber);
            if (shop is null) throw new NotFoundException("Shop Not Found!");

            var additionalAddress = _dbContext
                .AdditionalAddresses
                .FirstOrDefault(r => r.Id == id);

            if (additionalAddress is null || shopNumber != additionalAddress.ShopNumber)
            {
                throw new NotFoundException("Address Not Found!");
            }

            var additionalDto = _mapper.Map<AddAdditionalAddressDto>(additionalAddress);
            return additionalDto;

        }

        public void AdditionalAddressDelete(int id, int shopNumber)
        {
            var address = _dbContext
                .AdditionalAddresses
                .FirstOrDefault(r => r.Id == id);

            if (address is null || shopNumber!=address.ShopNumber)
            {
                throw new NotFoundException("Shop Not Found!");
            }
            _dbContext.Remove(address);
            _dbContext.SaveChanges();
        }

        public void AdditionalAddressUpdate(int shopNumber, int id, AddAdditionalAddressDto dto)
        {
            var address = _dbContext
                .AdditionalAddresses
                .FirstOrDefault(r => r.Id == id);

            if (address is null||shopNumber!=address.ShopNumber)
            {
                throw new NotFoundException("Shop Not Found!");
            }
            address.ShopNumber = dto.ShopNumber;
            address.Description = dto.Description;
            address.MapCoordinatesLatitude = dto.MapCoordinatesLatitude;
            address.MapCoordinatesLongitude = dto.MapCoordinatesLongitude;
            address.DeliveryInfo = dto.DeliveryInfo;
            _dbContext.SaveChanges();
        }
    }
}
