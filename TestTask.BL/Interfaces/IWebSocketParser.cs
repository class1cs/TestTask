using TestTask.DAL;

namespace TestTask.BL.Interfaces;

public interface IWebSocketParser
{
    event Action<PairTrade> NewBuyTrade;
    
    event Action<PairTrade> NewSellTrade;
    
    void SubscribeTrades(string pair, int maxCount = 100);
    
    void UnsubscribeTrades(string pair);

    event Action<Candle> CandleSeriesProcessing;
    
    void SubscribeCandles(string pair, int periodInSec, long? count = 0, DateTimeOffset? from = null, DateTimeOffset? to = null);
    
    void UnsubscribeCandles(string pair);
}