using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventMediaDTO :BaseDTO
    {
        public string? ThumbnailUrl { get; set; }
        public string? CoverUrl { get; set; }
    }
}
