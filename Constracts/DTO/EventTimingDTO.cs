using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventTimingDTO : BaseDTO
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        [Display(Name = "Ngày kết thúc")]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống")]
        [Display(Name = "Thời gian bắt đầu")]
        public TimeSpan? StartTime { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Thời gian kết thúc không được để trống")]
        [Display(Name = "Thời gian kết thúc")]
        public TimeSpan? EndTime { get; set; }
    }
}
