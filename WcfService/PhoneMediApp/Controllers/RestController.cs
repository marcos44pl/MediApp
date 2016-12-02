using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ComunicationControllers;
using System.Net.Http;
using System.Text;
using Windows.UI.Popups;

namespace PhoneMediApp.Controllers
{
    public class RestController<T>
    {
        internal class ODataResponse<Y>
        {
            [JsonProperty("odata.metadata")]
            public string Metadata { get; set; }
            public List<Y> Value { get; set; }
        }

        internal class ODataRequest<Y>
        {
            [JsonProperty("odata.metadata")]
            public string Metadata { get; set; }
            public Y Value { get; set; }
        }
        public async Task<IEnumerable<T>> getObjectsApi(string uri)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.Accept = "application/json";
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));

                    var resStream = response.GetResponseStream();
                    var stri = new StreamReader(resStream);
                    var json = stri.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
                await msg.ShowAsync();
            }
            return new List<T>();
        }

        public async Task<List<T>> getObjects(string uri)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.Accept = "application/json";
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));

                    var resStream = response.GetResponseStream();
                    var stri = new StreamReader(resStream);
                    var json = stri.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<ODataResponse<T>>(json);
                    return result.Value;
                }
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
                await msg.ShowAsync();
            }
            return new List<T>();
        }

        public async Task<bool> insertObject(T obj, string tableName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                var result = await httpClient.PostAsync(WcfConfig.WcfAdress + tableName, content);
                if (result.IsSuccessStatusCode)
                    return true;
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
                await msg.ShowAsync();
            }
            return false;
        }
    }
}
