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
    public class FxConnect
    {
        private string _token;
        private RestClient _client;

        public FxConnect(string token, OandaEnv environment)
        {
            _token = token;
            var clientAddr = string.Format("https://api-fx{0}.oanda.com/v3", environment);
            _client = new RestClient(clientAddr);
        }    
        
        internal T GetResponce<T>(string path, Method method) where T : class, new()
        {
            var req = new RestRequest(path, method);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            req.RequestFormat = DataFormat.Json;
            var resp = _client.Execute<T>(req);
            Debug.WriteLine(string.Format("Path:{0}", path));
            Debug.WriteLine(string.Format("Answer:{0}", resp.Content));
            if (resp.StatusCode!=System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Invalid responce.");     
            }
            return resp.Data;
        }

        public Account GetAccountSummary(string accId)
        {
            return GetResponce<AccountInfo>(string.Format("/accounts/{0}/summary", accId), Method.GET).Account;                
        }          
    }
}
