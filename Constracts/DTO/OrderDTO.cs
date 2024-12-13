using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public  class OrderDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
