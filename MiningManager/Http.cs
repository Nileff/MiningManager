using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MiningManager
{
    class Http
    {
        public static JObject GetResponce(string URL)
        {
            var request = WebRequest.Create(URL);
            string responseText;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    responseText = sr.ReadToEnd();
                }
                try
                {
                    return JObject.Parse(responseText);
                }
                catch { }
                try
                {
                    JArray arr = JArray.Parse(responseText);
                    return (JObject)arr.First;
                }
                catch { }
            }
            catch { }
            return new JObject();
        }
    }
}
