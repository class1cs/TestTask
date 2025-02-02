using TestTask.DAL.Models;

namespace TestTask.BL.Helpers;

public static class ResponseRestMapper
{
    public static IEnumerable<Candle> MapCandles(List<List<object>> deserializedResponse, string symbol)
    {
        if (!deserializedResponse.Any())
        {
            return Enumerable.Empty<Candle>();
        }

        return deserializedResponse.Select(x => new Candle
        {
            ClosePrice = Convert.ToInt64(x[2]),
            HighPrice = Convert.ToInt64(x[3]),
            LowPrice = Convert.ToInt64(x[4]),
            Volume = Convert.ToDecimal(x[5]),
            Mts = Convert.ToInt64(x[0]),
            OpenPrice = Convert.ToInt64(x[1])
        });
    }    
    
    
    public static IEnumerable<Trade> MapTrades(List<List<object>> deserializedResponse, string symbol)
    {
        if (!deserializedResponse.Any())
        {
            return Enumerable.Empty<Trade>();
        }

        var isFundingTrade = deserializedResponse.First().Count == 5;

        return deserializedResponse.Select(x => 
            isFundingTrade ? MapToFundingTrade(x) : MapToPairTrade(x, symbol));
    }

    public static Trade MapToFundingTrade(List<object> tradeData)
    {
        return new FundingTrade
        {
            Id = Convert.ToInt64(tradeData[0]),
            Mts = Convert.ToInt64(tradeData[1]),
            Amount = Convert.ToDecimal(tradeData[2]),
            Rate = Convert.ToDecimal(tradeData[3]),
            Period = Convert.ToInt64(tradeData[4]),
            Side = Convert.ToDecimal(tradeData[2]) > 0 ? "Buy" : "Sell"
        };
    }

    public static Trade MapToPairTrade(List<object> tradeData, string symbol)
    {
        return new PairTrade
        {
            Id = Convert.ToInt64(tradeData[0]),
            Amount = Convert.ToDecimal(tradeData[2]),
            Price = Convert.ToDecimal(tradeData[3]),
            Pair = symbol,
            Side = Convert.ToDecimal(tradeData[2]) > 0 ? "Buy" : "Sell"
        };
    }
}