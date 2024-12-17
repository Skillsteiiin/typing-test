using HelloApp1.Common;
using HelloApp1.ViewModels;
using HelloApp1.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HelloApp1
{
    public static class AppHost
    {
        public static IServiceProvider Build()
        {
            var services = new ServiceCollection();

            services.AddTransient<GreetingPageViewModel>();

            services.AddTransient<GreetingPage>();
            services.AddTransient<MainWindow>();


            return services.BuildServiceProvider();
        }
    }
}
