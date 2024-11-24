using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class PlaceDetailsDTO
    {
        public string PostalCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PlaceId { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
