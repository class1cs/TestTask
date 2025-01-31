namespace TestTask.DAL;

public class FundingTrade : Trade
{
    /// <summary>
    /// Скорость транзакции
    /// </summary>
    public decimal Rate { get; set; }
    
    /// <summary>
    /// Миллисекундная отметка
    /// </summary>
    public long Mts { get; set; }
    
    
    /// <summary>
    /// Время транзакции (сколько времени проводилась)
    /// </summary>
    public long Period { get; set; }
}