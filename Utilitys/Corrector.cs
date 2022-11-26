using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Задание__8.Utility
{
    public static class Corrector
    {
        private static readonly HttpClient client;
        private const string URL = "https://speller.yandex.net/services/spellservice.json/checkText?text=";

        static Corrector()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new("application/json"));
            client.DefaultRequestHeaders.Add(
                "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 OPR/91.0.4516.72"
            );
        }

        public static async Task<List<IncorrectWord>> GetDataAsync(string text)
        {
            string resultString = text;
            var task = client.GetStreamAsync(URL + String.Join('+', text.Split()));
            return await JsonSerializer.DeserializeAsync<List<IncorrectWord>>(task.Result);
        }
    }
}