using System;
using Microsoft.MobileBlazorBindings;
using Microsoft.Extensions.Hosting;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MobileBlazorDemoApp.Views;
using System.Net.Http;
using MobileBlazorDemoApp.Services;

namespace MobileBlazorDemoApp
{
    public class App : Application
    {
        public static IHost Host { get; set; }
        public App()
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                    services.AddScoped<ScopedManager>();
                    services.AddScoped<HttpClient>();
                    services.AddScoped<ApiService>();
                    services.AddScoped<UserService>();
                    services.AddScoped<NavigationService>();
                })
                .Build();
            
            //DetailPage = new ContentPage() { Title = "Home" };
            //NavigationPage = new NavigationPage(DetailPage);
            //DrawerParentPage = new ContentPage() { Title = "Drawer" };
            //MasterDetailPage mainPage = new MasterDetailPage();
            //mainPage.Master = DrawerParentPage;
            //mainPage.Detail = NavigationPage;
            MainPage = new ContentPage() { Title = "Login" };
                
            
            host.AddComponent<UserAccount>(parent: MainPage);
            //host.AddComponent<Drawer>(parent: DrawerParentPage);
            Host = host;
        }
        public static ContentPage DrawerParentPage { get; set; }
        public static NavigationPage NavigationPage { get; set; }
        public static ContentPage DetailPage { get; set; }
        //private StackLayout Drawer()
        //{
        //    Button button = new Button() { Text = "Home" };
        //    button.Clicked += (x, y) =>
        //    {
        //        NavigationRoot.PushAsync(new Counter())
        //    };
        //}

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
