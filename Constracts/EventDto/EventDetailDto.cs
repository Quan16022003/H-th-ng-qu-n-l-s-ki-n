using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.EventDto
{
    public class EventDetailDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
