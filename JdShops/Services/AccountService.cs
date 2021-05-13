using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JdShops.Entities;
using JdShops.Exceptions;
using JdShops.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JdShops.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        void UpdateUser(RegisterUserDto dto, int id);
        IEnumerable<User> GetAllUsers(string searchPhrase);
        void DeleteUser(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly ShopsDBContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSetting;
        private readonly IMapper _mapper;

        public AccountService(ShopsDBContext dbContext, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSetting, IMapper mapper)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _authenticationSetting = authenticationSetting;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var NewUser = new User()
            {
                Email = dto.Email,
                Fname = dto.Fname,
                Lname = dto.Lname,
                RoleId = dto.RoleId, 
                IsValidated = dto.IsValidated

            };

            var hashedPassword = _passwordHasher.HashPassword(NewUser, dto.PasswordHash);
            NewUser.PasswordHash = hashedPassword;

            _dbContext.Users.Add(NewUser);
            _dbContext.SaveChanges();
        }


        public string GenerateJwt(LoginDto dto)
            {
                var user = _dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email == dto.Email);

                if (user is null)
                {
                    throw new BadRequestException("Invalid username or password");
                }

                if (!user.IsValidated == true)
                {
                    throw new BadRequestException("Invalid username or password");
                }

                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.PasswordHash);
                if (result == PasswordVerificationResult.Failed)
                {
                    throw new BadRequestException("Invalid username or password");
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.Fname} {user.Lname}"),
                    new Claim(ClaimTypes.Role, $"{user.Role.Name}")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSetting.JwtKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(_authenticationSetting.JwtExpireDays);

                var token = new JwtSecurityToken(_authenticationSetting.JwtIssuer, _authenticationSetting.JwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: cred);

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }

        public void UpdateUser(RegisterUserDto dto, int id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(r => r.Id == id);

       
            if (user is null)
            {
              throw new NotFoundException("User Not Found!");
            }
           
            user.Email = dto.Email;
            user.Fname = dto.Fname;
            user.Lname = dto.Lname;
            user.RoleId = dto.RoleId;
            user.IsValidated = dto.IsValidated;
            if (dto.PasswordHash == null)
            {
                user.PasswordHash = user.PasswordHash;
            }
            else
            {
                var hashedPassword = _passwordHasher.HashPassword(user, dto.PasswordHash);
                user.PasswordHash = hashedPassword;
            }
            

            _dbContext.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers(string searchPhrase)
        {
            var Users = _dbContext.Users
                .Include(u => u.Role)
                .Where(r=> searchPhrase == null 
                           ||(r.Email.ToLower() ==searchPhrase.ToLower()
                           || r.Fname.ToLower() == searchPhrase.ToLower()
                           || r.Lname.ToLower() == searchPhrase.ToLower()))
                .ToList();
            
            return Users;
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(r => r.Id == id);
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }
    }
    }

