using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using PropertyChanged;
using TestTask.BL.Interfaces;

namespace TestTask.UI.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly IRestParser _restParser;
    private readonly IWebSocketParser _webSocketParser;

    public MainViewModel(IRestParser restParser, IWebSocketParser webSocketParser)
    {
        _restParser = restParser;
        _webSocketParser = webSocketParser;
    }

    public ICommand GetTradesCommand => new DelegateCommand(async () =>
    {
        var trades = await _restParser.GetNewTradesAsync("fUSD", 125);
        Console.WriteLine($"Id торгов:");
        trades.Take(20).ForEach(x => Console.WriteLine(x.Id));
    });
    
    public ICommand GetCandlesCommand => new DelegateCommand(async () =>
    {
        var candles = await _restParser.GetCandleSeriesAsync("trade%3A1m%3AtBTCUSD", 125, "hist");
        Console.WriteLine($"Информация о свечах:");
        candles.Take(20).ForEach(x => Console.WriteLine($"Цена на открытии: {x}"));
    });
    
    public ICommand SubscribeAtTradesAndCandlesCommand => new DelegateCommand(async () =>
    {
        await _webSocketParser.SubscribeTradesAndCandles("fUSD", "trade:1m:tBTCUSD");
    });
    
    public ICommand UnsubscribeFromCandlesCommand => new DelegateCommand(async () =>
    {
         _webSocketParser.UnsubscribeFromTradesAndCandles();
    });
    

   
}