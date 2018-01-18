using System.Net.Http;
using System.Threading.Tasks;

namespace RxDemo.Model
{
    public class WebWorker
    {
        public virtual async Task<Response> Get(string url)
        {
            using (var client = new HttpClient())
            {
                if (!(url.StartsWith("https://www.") || url.StartsWith("http://www.")))
                    url = "https://www." + url;
                var result = await client.GetAsync(url);
                return new Response(await result.Content.ReadAsStringAsync(),result.StatusCode);//RaiseResponseReceived(new WebRequestEventArgs(result.Content.ToString(), result.StatusCode));
            }
        }
    }
}
