using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdShops.Models
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; } = 2;
        public bool IsValidated { get; set; } = false;

    }
}
