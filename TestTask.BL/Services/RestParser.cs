using Newtonsoft.Json;
using TestTask.BL.Interfaces;
using TestTask.DAL;

namespace TestTask.BL.Services;

public class RestParser : IRestParser
{
    private readonly HttpClient _httpClient;

    public RestParser()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<IEnumerable<Trade>> GetNewTradesAsync(string symbol, int maxCount)
    {
       var request = await _httpClient.GetAsync($"https://api-pub.bitfinex.com/v2/trades/{symbol}/hist?limit={maxCount}");
       var json = await request.Content.ReadAsStringAsync();
       var deserializedResponse = JsonConvert.DeserializeObject<List<List<object>>>(json);
       var trade = deserializedResponse.FirstOrDefault();
       if (trade.Count == 5)
       {
           var fundingTrades = deserializedResponse.Select(x => new FundingTrade()
           {
               Amount = Convert.ToDecimal(x[2]),
               Id = Convert.ToInt32(x[0]),
               Mts = Convert.ToInt32(x[1]),
               Period = Convert.ToInt32(x[4]),
               Rate = Convert.ToInt32(x[3]),
               Side = Convert.ToDecimal(x[2]) > 0 ? "Buy" : "Sell"
               // API не предоставляет данных о времени торгов валютами.
           });
           return fundingTrades;
       }
       
       var pairTrades = deserializedResponse.Select(x => new PairTrade()
       {
           Amount = Convert.ToDecimal(x[2]),
           Pair = symbol,
           Id = Convert.ToInt32(x[0]),
           Price = Convert.ToDecimal(x[3]),
           Side = Convert.ToDecimal(x[2]) > 0 ? "Buy" : "Sell"
          // API не предоставляет данных о времени торгов парами.
       });
       return pairTrades;
    }

    public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, long? count, DateTimeOffset? to = null)
    {
        throw new NotImplementedException();
    }
}