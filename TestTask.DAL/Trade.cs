namespace TestTask.DAL;

public abstract class Trade
{
    /// <summary>
    /// Объем трейда
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Направление (buy/sell)
    /// </summary>
    public string Side { get; set; }
    
    /// <summary>
    /// Id трейда
    /// </summary>
    public int Id { get; set; }
}