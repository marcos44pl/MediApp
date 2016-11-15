using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PhoneMediApp.WcfRestControllers
{
    public class WcfRestController<T>
    {
        internal class ODataResponse<Y>
        {
            [JsonProperty("odata.metadata")]
            public string Metadata { get; set; }
            public List<Y> Value { get; set; }
        }

        public async Task<List<T>> getObjects(string uri)
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
    }
}
