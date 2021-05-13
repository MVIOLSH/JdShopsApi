using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using JdShops.Entities;

namespace JdShops
{
    public class Seeder
    {
        private readonly ShopsDBContext _dbContext;

        public Seeder(ShopsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    var users = SeedUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
            }

        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {

                new Role() {Name = "DummyUser"},
                new Role() {Name = "VerifiedUser"},
                new Role() {Name = "AdvancedUser"},
                new Role() {Name = "Admin"}
            };

            return roles;
        }
        private IEnumerable<User> SeedUsers()
        {
            var users = new List<User>()
            {

                new User() {Email = "mviolsh@gmail.com", Fname = "Michal", Lname = "Szymanski", PasswordHash = "Lokolo@12", RoleId = 4, IsValidated = true},
                new User() {Email = "test@test.co.uk", Fname = "Temporary", Lname = "User", PasswordHash = "p&hdrivers", RoleId = 1, IsValidated = true}
            };

            return users;
        }





    }
    
}
