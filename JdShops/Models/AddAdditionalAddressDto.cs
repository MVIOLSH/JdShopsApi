using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdShops.Models
{
    public class AddAdditionalAddressDto 
    {
        public int Id { get; set; }
        public float ShopNumber { get; set; }
        public string Description { get; set; }
        public string DeliveryInfo { get; set; }
        public string MapCoordinatesLongitude { get; set; }
        public string MapCoordinatesLatitude { get; set; }
    }
}
