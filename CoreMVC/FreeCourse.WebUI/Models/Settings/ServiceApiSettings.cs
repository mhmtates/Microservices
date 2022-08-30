using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Models.Settings
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string ImageStockUri { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi ImageStock { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Discount { get; set;}
        public ServiceApi FakePayment { get; set; }
        public ServiceApi Order { get; set;}
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }

}
