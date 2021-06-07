using AutoMapper;
using JdShops.Entities;
using JdShops.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdShops.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace JdShops.Services
{
    public interface IShopsService
    {
        IEnumerable<ShopsDto> GetById(int id);
        IEnumerable<ShopsDto> GetAll(string searchPhrase);
        float Create(CreateShopDto dto);
        void ShopDelete(int id);
        void ShopUpdate(int id, UpdateShopDto dto);
    }

    public class ShopsService : IShopsService
    {
        private readonly ShopsDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;
        


        public ShopsService(ShopsDBContext dbContext, IMapper mapper, ILogger<ShopsService> logger, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;           

        }
        /** Method to update shop details record in both tables of DB */
        public void ShopUpdate(int id, UpdateShopDto dto)
        {
            _logger.LogWarning($"Shop with Shop Number: {id} Update action invoked by");

            var shop = _dbContext
                .Shops
                .FirstOrDefault(r => r.ShopNumber == id);

            var address = _dbContext
                .Address
                .FirstOrDefault(r => r.ShopNumber == id);

            if (shop is null)
            {
                _logger.LogError($"Shop with Shop Number: {id} Update action unsuccessful invoked by");
                throw  new NotFoundException("Shop Not Found!");
            }
            shop.ShopNumber = dto.ShopNumber;
            shop.Facia = dto.Facia;
            shop.Town = dto.Town;
            shop.PhoneNumber = dto.PhoneNumber;
            address.MapCoordinatesLatitude = dto.MapCoordinatesLatitude;
            address.MapCoordinatesLongitude = dto.MapCoordinatesLongitude;
            address.DeliveryInfo = dto.DeliveryInfo;
            _dbContext.SaveChanges();
            _logger.LogWarning($"Shop with Shop Number: {id} Update action successful invoked by");
            


        }
        /** Method to delete shop details record in both tables of DB */
        public void ShopDelete(int id)
        {
            _logger.LogWarning($"Shop with Shop Number: {id} Delete action invoked by");
            var shop = _dbContext
                .Shops
                .Include(r => r.Address)
                .FirstOrDefault(r => r.ShopNumber == id);

            var address = _dbContext
                .Address
                .FirstOrDefault(r => r.ShopNumber == id);

            if (shop is null)
            {
                _logger.LogError($"Shop with Shop Number: {id} Delete action unsuccessful invoked by");
                throw new NotFoundException("Shop Not Found!");
            }
            
            _dbContext.Remove(shop);
            _dbContext.Remove(address);
            _dbContext.SaveChanges();
            _logger.LogWarning($"Shop with Shop Number: {id} Delete action successful invoked by");
           
        }
        /** Method to list specified by shop number shop details records from both tables of DB */
        public IEnumerable<ShopsDto> GetById(int id) 
        {
            var shops = _dbContext
             .Shops
             .Include(r => r.Address)
             .Where(r => r.ShopNumber == id).ToList();

            if (shops is null) throw new NotFoundException("Shop Not Found!");
            var result = _mapper.Map<List<ShopsDto>>(shops);
            return result;
        }
        /** Method to list all the shops details records from both tables of DB */
        public IEnumerable<ShopsDto> GetAll(string searchPhrase)
        {
            var shops = _dbContext
                    .Shops
                    .Include(r => r.Address)
                    .Where(r=> searchPhrase == null || (r.Facia.ToLower() == searchPhrase.ToLower() 
                              || r.Town.ToLower() == searchPhrase.ToLower()))
                    .ToList();

            var shopsDtos = _mapper.Map<List<ShopsDto>>(shops);
            return shopsDtos;
        }
        /** Method to create new shop entity record in both tables of DB */
        public float Create(CreateShopDto dto)
        {
            _logger.LogWarning($"Shop with Shop Number: {dto.ShopNumber} Create action invoked by");
            var shop = _mapper.Map<Shops>(dto);
            _dbContext.Shops.Add(shop);
            _dbContext.SaveChanges();
            _logger.LogWarning($"Shop with Shop Number: {dto.ShopNumber} Create action finished with success by");
            
            return shop.ShopNumber;
        }
    }
}
