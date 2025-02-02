using TestTask.DAL;

namespace TestTask.BL.Interfaces;

public interface IWebSocketParser
{
    Task SubscribeTradesAndCandles(string symbol, string key);
    
    void UnsubscribeFromTradesAndCandles();
}