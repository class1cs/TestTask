using Newtonsoft.Json;

namespace TestTask.DAL.SubscriptionMessages;

public class TradeSubscriptionMessage
{
    [JsonProperty("event")]
    public string Event { get; set; }
    
    [JsonProperty("channel")]
    public string Channel { get; set; }
    
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}