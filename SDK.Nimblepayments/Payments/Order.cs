using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Nimblepayments.Payments
{
    using Newtonsoft.Json;

    public class Order
    {
        [JsonProperty(PropertyName = "customerData")]
        public string CustomerTransaction { get; set; }
        [JsonProperty(PropertyName = "clientId")]
        public string User { get; set; }
    }
}
