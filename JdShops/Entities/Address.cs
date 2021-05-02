using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public float ShopNumber { get; set; }
        public string DeliveryInfo { get; set; }
        public string MapCoordinates { get; set; }

        public virtual Shops Shop { get; set; }
    }
}
