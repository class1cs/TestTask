using TestTask.DAL;

namespace TestTask.BL.Interfaces;

public interface IRestParser
{
    Task<IEnumerable<Trade>> GetNewTradesAsync(string symbol, int maxCount);
    
    Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int periodInSec, DateTimeOffset? from,  long? count = 0, DateTimeOffset? to = null);
}