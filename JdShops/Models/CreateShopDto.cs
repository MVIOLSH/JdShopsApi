using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Models
{
    public class CreateShopDto
    {
        public int Id { get; set; }
        public float ShopNumber { get; set; }

        public string Facia { get; set; }

        public string Town { get; set; }

        public string PhoneNumber { get; set; }

        public string DeliveryInfo { get; set; }

        public string MapCoordinates { get; set; }

    }
}
