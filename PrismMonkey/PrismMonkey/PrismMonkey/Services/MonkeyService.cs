using Newtonsoft.Json;
using PrismMonkey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PrismMonkey.Services
{
    /// <summary>
    /// 猴子服務類別
    /// </summary>
    public class MonkeyService
    {
        /// <summary>
        /// 可以讀取網路服務端點的猴子清單資訊物件
        /// </summary>
        public HttpClient Client { get; set; } = new HttpClient();
        /// <summary>
        /// 從網路讀取到的猴子集合物件
        /// </summary>
        public List<Monkey> Monkeys { get; set; }

        /// <summary>
        /// 取得遠端網路上的猴子資訊清單
        /// </summary>
        /// <returns></returns>
        public async Task<List<Monkey>> GetMonkeysAsync()
        {
            // 若已經有猴子資料，則不會聯網抓取
            if (Monkeys?.Count > 0)
                return Monkeys;

            // 透過服務端點，線上抓取猴子 JSON 清單資訊
            var result = await Client.GetStringAsync("https://www.montemagno.com/monkeys.json");
            Monkeys = JsonConvert.DeserializeObject<List<Monkey>>(result); 

            // Offline
            /*using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);
            */
            return Monkeys;
        }
    }
}
