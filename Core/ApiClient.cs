using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokelist.Core
{

    public static class ApiClient
    {

        public static async Task<T> Get<T>(string url, Func<string, T> callBack)
        {
            using(var client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(url))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data == null) 
                    {
                        return default(T);
                    } else 
                    {
                        return callBack(data);
                    }
                }
            }
        }

    }
}