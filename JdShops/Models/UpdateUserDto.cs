using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Models
{
    public class UpdateUserDto
    {
       
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; } = 2;
        public bool IsValidated { get; set; } = false;
    }
}
