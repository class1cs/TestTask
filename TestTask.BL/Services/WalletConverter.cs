using TestTask.BL.Interfaces;
using TestTask.DAL.Models;

namespace TestTask.BL.Services;

public class WalletConverter : IWalletConverter
{
    public ConvertedBalance ConvertWalletToBalances(Wallet wallet)
    {
        var totalInUsdt = (wallet.Btc * ConversionRates.BtcToUsdt) +
                          (wallet.Xrp * ConversionRates.XrpToUsdt) +
                          (wallet.Xmr * ConversionRates.XmrToUsdt) +
                          (wallet.Dash * ConversionRates.DashToUsdt);

        var convertedBalance = new ConvertedBalance
        {
            TotalInUsdt = totalInUsdt,
            TotalInBtc = totalInUsdt / ConversionRates.BtcToUsdt,
            TotalInXrp = totalInUsdt / ConversionRates.XrpToUsdt,
            TotalInXmr = totalInUsdt / ConversionRates.XmrToUsdt,
            TotalInDash = totalInUsdt / ConversionRates.DashToUsdt
        };

        return convertedBalance;
    }
}

public static class ConversionRates
{
    public const decimal BtcToUsdt = 30000; 
    
    public const decimal XrpToUsdt = 0.5m;  
    
    public const decimal XmrToUsdt = 200; 
    
    public const decimal DashToUsdt = 100; 
}