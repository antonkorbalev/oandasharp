using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics;

namespace OandaConnect
{
    public class OandaBase
    {
        private string _token;
        private RestClient _client;

        public OandaBase(string token, OandaEnv environment)
        {
            _token = token;
            var clientAddr = string.Format("https://api-fx{0}.oanda.com/v3", environment);
            _client = new RestClient(clientAddr);
        }

        private RestRequest GetRequest(string path, Method method)
        {
            var req = new RestRequest(path, method);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            req.RequestFormat = DataFormat.Json;
            return req;
        }

        internal T GetResponce<T>(string path, Method method) where T : class, new()
        {
            var req = GetRequest(path, method);
            var resp = _client.Execute<T>(req);
            Debug.WriteLine(string.Format("Path:{0}", path));
            Debug.WriteLine(string.Format("Answer:{0}", resp.Content));
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
                throw new InvalidOperationException("Invalid responce.");
            return resp.Data;
        }

        internal void GetResponceAsync<T>(string path, Method method, Action<IRestResponse<T>> callback, Parameter[] parameters = null) where T : class, new()
        {
            var req = GetRequest(path, method);
            _client.ExecuteAsync(req, (IRestResponse<T> resp) => 
            {
                Debug.WriteLine(string.Format("Path:{0}", path));
                Debug.WriteLine(string.Format("Answer:{0}", resp.Content));
                if (resp.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new InvalidOperationException("Invalid responce.");
                callback(resp);
            });
        }
    }
}
