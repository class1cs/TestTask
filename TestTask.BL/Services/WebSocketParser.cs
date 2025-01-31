﻿using TestTask.BL.Interfaces;
using TestTask.DAL;

namespace TestTask.BL.Services;

public class WebSocketParser : IWebSocketParser
{
    private readonly HttpClient _httpClient;

    public WebSocketParser(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public event Action<PairTrade>? NewBuyTrade;
    
    public event Action<PairTrade>? NewSellTrade;
    
    public void SubscribeTrades(string pair, int maxCount = 100)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeTrades(string pair)
    {
        throw new NotImplementedException();
    }

    public event Action<Candle>? CandleSeriesProcessing;

    public void SubscribeCandles(string pair, int periodInSec,
        long? count, DateTimeOffset? from = null, DateTimeOffset? to = null)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeCandles(string pair)
    {
        throw new NotImplementedException();
    }
}