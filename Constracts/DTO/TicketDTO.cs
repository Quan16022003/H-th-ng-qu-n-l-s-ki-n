using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class TicketDTO: BaseDTO
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Display(Name = "Giá")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Số lượng tối đa không được để trống")]
        [Display(Name = "Số lượng tối đa 1 khách hàng có thể mua")]
        public int MaxPerPerson { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Display(Name = "Số lượng")]
        public int QuantityAvailable { get; set; }

        public int QuantitySold { get; set; }
        public int EventId { get; set; }
        public TicketStatus Status { get; set; }
    }
}
