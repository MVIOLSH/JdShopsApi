using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdShops.Entities
{
    public class AdditionalAddress
    {
        public int Id { get; set; }
        public float ShopNumber { get; set; }
        public string Description { get; set; }
        public string DeliveryInfo { get; set; }
        public string MapCoordinatesLongitude { get; set; }
        public string MapCoordinatesLatitude { get; set; }
    }
}
