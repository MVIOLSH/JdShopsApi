using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Entities
{
    public class Shops
    {
        public int Id { get; set; }
        public float ShopNumber { get; set; }

        public string Facia { get; set; }

        public string Town { get; set; }

        public string PhoneNumber { get; set; }

        //public string DeliveryInfo { get; set; }

        //public string MapCoordinates { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }







    }
}
