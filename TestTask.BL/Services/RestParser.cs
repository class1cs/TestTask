using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using TestTask.BL.Helpers;
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
        var queryParams = new Dictionary<string, string>()
        {
            ["limit"] = maxCount.ToString()
        };
        var uri = QueryHelpers.AddQueryString($"https://api-pub.bitfinex.com/v2/trades/{symbol}/hist", queryParams!);
        
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var deserializedResponse = JsonConvert.DeserializeObject<List<List<object>>>(json);

        return ResponseRestMapper.MapTrades(deserializedResponse, symbol);
    }

    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int count, string section)
    {
        var queryParams = new Dictionary<string, string>()
        {
            ["limit"] = count.ToString()
        };
        var uri = QueryHelpers.AddQueryString($"https://api-pub.bitfinex.com/v2/candles/{symbol}/{section}", queryParams!);
        
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var deserializedResponse = JsonConvert.DeserializeObject<List<List<object>>>(json);

        return ResponseRestMapper.MapCandles(deserializedResponse, symbol);
    }

 
}