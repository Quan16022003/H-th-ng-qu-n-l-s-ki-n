using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.EventDto
{
    public class EventListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryEventName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public bool IsPublic { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
