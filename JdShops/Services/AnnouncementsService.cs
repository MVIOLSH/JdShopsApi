using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JdShops.Entities;
using JdShops.Exceptions;
using JdShops.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JdShops.Services
{
    public interface IAnnouncementsService
        {
            AnnouncementsDto GetById(int id);
            IEnumerable<AnnouncementsDto> GetAll();
            float Create(AnnouncementsDto dto);
            void AnnouncementDelete(int id);
            void AnnouncementUpdate(int id, AnnouncementsDto dto);
        }

        public class AnnouncementsService : IAnnouncementsService
        {
            private readonly ShopsDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly IWebHostEnvironment _environment;
            private readonly IImageUploadService _image;


            public AnnouncementsService(ShopsDBContext dbContext, IMapper mapper, ILogger<Services.ShopsService> logger, IWebHostEnvironment environment, IImageUploadService image)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _logger = logger;
                _environment = environment;
                _image = image;

            }
            /** Method to update shop details record in both tables of DB */
            public void AnnouncementUpdate(int id, AnnouncementsDto dto)
            {
                _logger.LogWarning($"Announcement Number: {id} Update action invoked by");

                var announcements = _dbContext
                    .Announcements
                    .FirstOrDefault(r => r.Id == id);

             

                if (announcements is null)
                {
                    _logger.LogError($"Shop with Shop Number: {id} Update action unsuccessful invoked by");
                    throw new NotFoundException("Shop Not Found!");
                }
                announcements.Id = dto.Id;
                announcements.Content = dto.Content;
                announcements.Title = dto.Title;
                announcements.Type = dto.Type;
                announcements.IsPublished = dto.IsPublished;
                announcements.IsDeleted = dto.IsDeleted;
                _dbContext.SaveChanges();
                _logger.LogWarning($"Shop with Shop Number: {id} Update action successful invoked by");



            }
            /** Method to delete shop details record in both tables of DB */
            public void AnnouncementDelete(int id)
            {
                _logger.LogWarning($"Announcement Number: {id} Delete action invoked by");
                var announcement = _dbContext
                    .Announcements
                    .FirstOrDefault(r => r.Id == id);

                if (announcement is null)
                {
                    _logger.LogError($"Announcement Number: {id} Delete action unsuccessful invoked by");
                    throw new NotFoundException("Announcement Not Found!");
                }

                _dbContext.Remove(announcement);
                _dbContext.SaveChanges();
                _logger.LogWarning($"Announcement Number: {id} Delete action successful invoked by");

            }
            /** Method to list specified by shop number shop details records from both tables of DB */
            public AnnouncementsDto GetById(int id)
            {
                var announcement = _dbContext
                 .Announcements
                 .FirstOrDefault(r => r.Id == id);

                if (announcement is null) throw new NotFoundException("Shop Not Found!");
                var result = _mapper.Map<AnnouncementsDto>(announcement);
                return result;
            }
            /** Method to list all the shops details records from both tables of DB */
            public IEnumerable<AnnouncementsDto> GetAll()
            {
                var announcement = _dbContext
                    .Announcements
                    .ToList();
                        
                var announcementDtos = _mapper.Map<List<AnnouncementsDto>>(announcement);
                return announcementDtos;
            }
            /** Method to create new shop entity record in both tables of DB */
            public float Create(AnnouncementsDto dto)
            {
                _logger.LogWarning($"Announcement Number: {dto.Id} Create action invoked by");
                var announcement = _mapper.Map<Announcements>(dto);
                _dbContext.Announcements.Add(announcement);
                _dbContext.SaveChanges();
                _logger.LogWarning($"Announcement Number: {dto.Id} Create action finished with success by");

                return announcement.Id;
            }
        }
    }

