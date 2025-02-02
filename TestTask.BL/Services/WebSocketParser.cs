using System.Diagnostics;
using System.Reactive.Linq;
using TestTask.BL.Interfaces;
using TestTask.DAL;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestTask.BL.Helpers;
using TestTask.DAL.SubscriptionMessages;
using Websocket.Client;


namespace TestTask.BL.Services;

public class WebSocketParser : IWebSocketParser
{
    private string? _tradeChannelId;
    
    private string? _candleChannelId;

    private readonly WebsocketClient _websocketClient;

    public WebSocketParser()
    {
        _websocketClient = new WebsocketClient(new Uri("wss://api-pub.bitfinex.com/ws/2"));
        _websocketClient.MessageReceived.Where(x => !x.Text.Contains("info") && !x.Text.Contains("hb")).Subscribe(msg =>
        {
            var response = msg.Text;
            if (msg.Text.Contains("subscribed"))
            {
                HandleSubscription(response);
            }
            // Обработка запроса подписки на свечи.
            Console.WriteLine($"Пришло новое уведомление о свечах/торгах. {response}"); // Для тестов.
            // Ответ от сервера может быть как уведомлением о подписке, так и об обновлении, так и об получении первых торгов, так и об свечах.
            // Отличить их невозможно (API не даёт к этому никаких заголовков в JSON вида "type" или что-то наподобие). Вынес ответ от сервера в переменную.
            
        });
        _websocketClient.Start();
    }
    
    private void HandleSubscription(string response)
    {

        var jsonResponse = JObject.Parse(response);
        if (jsonResponse["event"]?.ToString() == "subscribed" && jsonResponse["channel"]?.ToString() == "candles" && _candleChannelId == null)
        {
            _candleChannelId = jsonResponse["chanId"]?.ToString();
            return;
        }

        _tradeChannelId ??= jsonResponse["chanId"]?.ToString();
        
    }
    
    public async Task SubscribeTradesAndCandles(string symbol, string key)
    {
        var tradePayload = new TradeSubscriptionMessage()
        {
            Event = "subscribe",
            Channel = "trades",
            Symbol = symbol
        };
        var jsonTradeMessage = JsonConvert.SerializeObject(tradePayload);
        _websocketClient.Send(jsonTradeMessage);
        
        var candlePayload = new CandleSubscriptionMessage()
        {
            Event = "subscribe",
            Channel = "candles",
            Key = key
        };
        var jsonCandleMessage = JsonConvert.SerializeObject(candlePayload);
        _websocketClient.Send(jsonCandleMessage);
    }
    

    public void UnsubscribeFromTradesAndCandles()
    {
        if (_tradeChannelId == null)
        {
            Console.WriteLine("Нельзя отписаться, так как клиент не подписан на торги.");
            return;
        }
        
        var unsubscribeTradePayload = new UnsubscriptionMessage()
        {
            Event = "unsubscribe",
            ChanId = _tradeChannelId
        };
        
        var jsonTradeMessage = JsonConvert.SerializeObject(unsubscribeTradePayload);
        _websocketClient.Send(jsonTradeMessage);

        Console.WriteLine("Отписался от торгов.");
        _tradeChannelId = null; 
        
        if (_candleChannelId == null)
        {
            Console.WriteLine("Нельзя отписаться, так как клиент не подписан на свечи.");
            return;
        }
        
        var unsubscribeCandlePayload = new UnsubscriptionMessage()
        {
            Event = "unsubscribe",
            ChanId = _candleChannelId
        };
        
        var jsonCandleMessage = JsonConvert.SerializeObject(unsubscribeCandlePayload);
        _websocketClient.Send(jsonCandleMessage);
        Console.WriteLine("Отписался от свеч.");
        _tradeChannelId = null; 
    }
}