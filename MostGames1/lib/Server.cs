using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MostGames1.lib
{
    static class Server
    {
        /// <summary>
        /// Gets string from server by id
        /// </summary>
        /// 
        /// <param name="id">
        /// String id
        /// </param>
        /// 
        /// <returns>
        /// String from server
        /// </returns>
        public static async Task<string> GetStringsByIdAsync(Int32 id)
        {
            return await Task.Run(async () =>
            {
                using HttpClient client = new HttpClient();
                string uri = "http://tmgwebtest.azurewebsites.net/api/textstrings/";
                client.DefaultRequestHeaders.Add("TMG-Api-Key", APIKeys.Key);
                string result = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    using var response = await client.GetAsync(uri + id.ToString());
                    if (!response.IsSuccessStatusCode)
                    {
                        if ((int)response.StatusCode == 418)
                        {
                            continue;
                        }
                        throw new Exception("Ошибка сервера: " + response.ReasonPhrase);
                    }
                    else
                    {
                        Root a = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
                        result = a.text;
                        break;
                    }
                }
                return result;
            });          
        }

        class Root
        {
            public string text { get; set; }
        }
    }
}
