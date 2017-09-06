using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WirelessTagConnector
{
    public class HttpManager
    {
        string BASE_ADDRESS = ConfigurationSetting.BaseAddress;//"https://my.wirelesstag.net";// "https://www.mytaglist.com";
        string AUTH_API_SUB_ADDRESS = ConfigurationSetting.AuthApiSubAddess;
        string UFL_API_ADDRESS = ConfigurationSetting.UFL_Api_Address;
        public string CLIENT_SUB_ADDRESS = "/ethClient.asmx/";
        public string GET_TAG_LIST = "GetTagList";
        string CLIENT_ID = ConfigurationSetting.Client_Id;
        string CLIENT_SECRET = ConfigurationSetting.Client_Secret;
        string CLIENT_CODE = ConfigurationSetting.Client_Code;
        public string Token { get; set; }
        private static HttpManager _instance;
        private HttpManager()
        {
            
        }
        public static HttpManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HttpManager();
            }
            return _instance;
        }


        public async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                if(this.Token!=null)
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var config = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(config);
                }
                else
                {
                    throw new System.InvalidOperationException("Failed to get data from API");
                }
            }
        }

        public string Post(string subAddress,string apiName, string requestData="")
        {
            string response = null;
            try
            {               
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_ADDRESS+subAddress);
                    
                    
                                          
                    
                    if (Token != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    client.DefaultRequestHeaders
                         .Accept
                         .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiName);
                    request.Content = new StringContent(requestData, Encoding.UTF8, "application/json");

                    var responseMsg =  client.SendAsync(request).Result;
                    responseMsg.EnsureSuccessStatusCode();
                    response =  responseMsg.Content.ReadAsStringAsync().Result;
                }

                }
            catch (Exception e)
            {
                
            }
            return response;
        }

        public void GenerateToken()
        {
            using (HttpClient client = new HttpClient())
            {
                var request1 = new HttpRequestMessage(HttpMethod.Post, BASE_ADDRESS+ AUTH_API_SUB_ADDRESS);
                request1.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                        { "client_id", CLIENT_ID },
                        { "client_secret", CLIENT_SECRET },
                        { "code", CLIENT_CODE }
                     });
                var response =  client.SendAsync(request1).Result;
                response.EnsureSuccessStatusCode();

                var payload = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                this.Token = payload.Value<string>("access_token");
            }               
        }

        public string PutDataToUFLConnector(string requestData = "")
        {
            string response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    Uri reqUri = new Uri(UFL_API_ADDRESS);

                    String username = ConfigurationSetting.UFL_Api_Username;
                    String password = ConfigurationSetting.UFL_Api_Password;

                    Console.WriteLine("username is ::: " + username + " and Password is :::" + password);
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    Console.WriteLine("Base64 string of credentials ::: " + encoded);
                    if (Token != null)
                       
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",encoded);
                   
                    Console.WriteLine("Request uri is ::" + reqUri);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, reqUri);
                    request.Content = new StringContent(requestData, Encoding.UTF8);

                    var responseMsg = client.SendAsync(request).Result;
                    responseMsg.EnsureSuccessStatusCode();
                    response = responseMsg.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured :: " + e.Message);
            }
            return response;
        }

    }
}
