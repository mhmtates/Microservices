using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Models.Order
{
    public class CreateAddressInput
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; } 
        public string Line { get; set; }
    }
}
