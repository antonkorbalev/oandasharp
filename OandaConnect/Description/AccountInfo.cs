using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace OandaConnect.Description
{
    public class Account
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
        public float PL { get; set; }
    }

    public class AccountInfo
    {
        [DeserializeAs(Name = "account")]
        public Account Account { get; set; }
    }
}
