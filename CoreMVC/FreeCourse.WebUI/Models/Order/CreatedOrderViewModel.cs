using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Models.Order
{
    public class CreatedOrderViewModel
    {
        public int OrderId { get; set; }

        public string Error { get; set; }

        public bool IsSuccessful { get; set; }
        
    }
}
