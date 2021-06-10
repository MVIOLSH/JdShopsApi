using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JdShops.Authorization;
using JdShops.Entities;
using JdShops.Exceptions;
using JdShops.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JdShops.Services
{
    public interface ITicketService
    {
        int Create(TicketsDto dto);
        int Update(TicketsDto dto, int id);
        IEnumerable<TicketsDto> GetAll(string searchPhrase);
        IEnumerable<TicketsDto> GetById(int id);
    }

    public class TicketService : ITicketService
    {
    private readonly ShopsDBContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<TicketService> _logger;
    private readonly IUserContextService _userContextService;


    public TicketService(ShopsDBContext dbContext, IMapper mapper, ILogger<TicketService> logger, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _userContextService = userContextService;

    }

    public int Create(TicketsDto dto)
    {
        var user = _dbContext.Users.FirstOrDefault(r => r.Id == _userContextService.GetUserid);
        dto.UserId = user.Id;
        dto.UserFname = user.Fname;
        dto.UserLname = user.Lname;
        var ticket = _mapper.Map<Tickets>(dto);
        _dbContext.Tickets.Add(ticket);
        _dbContext.SaveChanges();
        _logger.LogInformation(
            $"Ticket {ticket.Id} has been created by {ticket.CreatedByUser} {ticket.UserFname} {ticket.UserLname}");
        return ticket.Id;
    }

    public int Update(TicketsDto dto, int id)
    {

        
        var ticket = _dbContext.Tickets.FirstOrDefault(c => c.Id == id);
        if (ticket is null)
        {
            throw new NotFoundException("Ticket Not Found!");
        }

        var user = _dbContext.Users.FirstOrDefault(c => c.Id == _userContextService.GetUserid);
        dto.UserId = ticket.Id;
        dto.UserFname = ticket.UserFname;
        dto.UserLname = ticket.UserLname;
        dto.Id = ticket.Id;
        var TicketDto = dto;
        ticket.TypeOfRequest = TicketDto.TypeOfRequest;
        ticket.Title = TicketDto.Title;
        ticket.Status = TicketDto.Status;
        ticket.ShopNumber = TicketDto.ShopNumber;
        ticket.Description = TicketDto.Description;
        
        _dbContext.SaveChanges();
        _logger.LogInformation($"Ticket {ticket.Id} has been updated by {user.Id} {user.Fname} {user.Lname}");

        return ticket.Id;
    }

    public IEnumerable<TicketsDto> GetAll(string searchPhrase)
    {
        var allTickets = _dbContext.Tickets
            .Include(r => r.CreatedByUser)
            .Where(r=> searchPhrase== null || 
                       (r.ShopNumber.ToLower() == searchPhrase.ToLower() 
                        || r.Status.ToLower()==searchPhrase.ToLower() 
                        || r.TypeOfRequest.ToLower() == searchPhrase.ToLower() 
                        || r.UserFname.ToLower() == searchPhrase.ToLower() 
                        ||r.UserLname.ToLower() == searchPhrase.ToLower() ))
            .ToList();

        var ticketsDtos = _mapper.Map<List<TicketsDto>>(allTickets);
        return ticketsDtos;
    }

    public IEnumerable<TicketsDto> GetById(int id)
    {
        var allTickets = _dbContext.Tickets
            .Include(r => r.CreatedByUser)
            .Where(r => r.Id == id)
            .ToList();

        var ticketsDtos = _mapper.Map<List<TicketsDto>>(allTickets);
        return ticketsDtos;

        }


    }
}
