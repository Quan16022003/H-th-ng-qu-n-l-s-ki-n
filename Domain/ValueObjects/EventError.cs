using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public static class EventError
    {
        public static readonly Error PublishFailed = new ("Event.PublishFailed", 
            "Publish sự kiện thất bại");

        public static readonly Error ValidationFailed = new("Event.PublishFailed",
            "Vui lòng nhập đầy đủ thông tin cần thiết trước khi publish");
    }
}
