using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Models
{
    public class AnnouncementsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } 
        public bool IsPublished { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
