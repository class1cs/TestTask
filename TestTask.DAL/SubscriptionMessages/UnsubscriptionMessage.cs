using Newtonsoft.Json;

namespace TestTask.DAL.SubscriptionMessages;

public class UnsubscriptionMessage
{
    [JsonProperty("event")]
    public string Event { get; set; }
    
    [JsonProperty("chanId")]
    public string ChanId { get; set; }
}