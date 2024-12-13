using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AllowEmptyOrNullAttribute : Attribute
    {
        public AllowEmptyOrNullAttribute() { }
    }
}
