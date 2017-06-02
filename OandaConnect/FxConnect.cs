using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using OandaConnect.Description;
using System.Diagnostics;

namespace OandaConnect
{
    public class FxConnect : OandaBase
    {
        public FxConnect(string token, OandaEnv environment) : base(token, environment)
        {

        }

        public Account GetAccountSummary(string accId)
        {   
            return GetResponce<AccountInfo>(string.Format("/accounts/{0}/summary", accId), Method.GET).Account;                
        }          
    }
}
