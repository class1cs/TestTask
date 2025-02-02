using Newtonsoft.Json;

namespace TestTask.DAL.SubscriptionMessages;

public class CandleSubscriptionMessage
{
    [JsonProperty("event")]
    public string Event { get; set; }
    
    [JsonProperty("channel")]
    public string Channel { get; set; }
    
    [JsonProperty("key")]
    public string Key { get; set; }
}