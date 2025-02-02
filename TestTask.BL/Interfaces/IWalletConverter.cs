using TestTask.DAL.Models;

namespace TestTask.BL.Interfaces;

public interface IWalletConverter
{
    ConvertedBalance ConvertWalletToBalances(Wallet wallet);
    
    
}