// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.IO;
// using Newtonsoft.Json;
// using System.Net;
//
//
//
// namespace newsApi
// {
//   public class HealthNews
//   {
//    public static List<NewsResult> GetNewsForPlayer(string playerName)
//    {
//        var result = new List<NewsResult>();
//        var webClient = new WebClient();
//        webClient.Headers.Add("Ocp-Apim-Subscription-Key", "758e3b90a9cc475dbe82429e002d6cdf");
//        var serialzer = new JsonSerializer();
//        byte[] searchResult = webClient.DownloadData(string.Format("https://api.cognitive.microsoft.com/bing/v5.0/news/search?q={0}&mkt=en-us",playerName));
//        using (var stream = new MemoryStream(searchResult))
//        using (var reader = new StreamReader(stream))
//        using (var jsonReader= new JsonTextReader(reader))
//        {
//            result = serialzer.Deserialize<NewSearch>(jsonReader).NewsResult;
//        }
//        return result;
//    }
//  }
//  }
