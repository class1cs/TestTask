using Microsoft.Extensions.DependencyInjection;
using TestTask.BL.Interfaces;
using TestTask.BL.Services;
using TestTask.UI.ViewModels;

namespace TestTask.UI;

public static class IoC
{
    private static readonly IServiceProvider _provider;

    static IoC()
    {
        var services = new ServiceCollection();

        services.AddTransient<MainViewModel>();
        services.AddScoped<IRestParser, RestParser>();
        services.AddScoped<IWebSocketParser, WebSocketParser>();
        _provider = services.BuildServiceProvider();
    }

    public static T Resolve<T>() => _provider.GetRequiredService<T>();
}

public class ViewModelLocator
{
    public MainViewModel MainViewModel => IoC.Resolve<MainViewModel>();
}
