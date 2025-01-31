using DevExpress.Mvvm;
using PropertyChanged;
using TestTask.BL.Interfaces;

namespace TestTask.UI.ViewModels;

public class MainViewModel : BindableBase
{
    public MainViewModel(IRestParser restParser)
    {
        restParser.GetNewTradesAsync("tBTCUSD", 125);
    }
}