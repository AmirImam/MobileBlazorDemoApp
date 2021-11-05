using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings;
using MobileBlazorDemoApp.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileBlazorDemoApp.Services
{
    public class NavigationService
    {
        public NavigationService(ScopedManager scope)
        {
            this.Scope = scope;
        }
        private ScopedManager Scope { get; set; }
        public RenderFragment Content { get; set; }
        public List<NavigationModel> NavigationStack { get; set; } = new List<NavigationModel>();
        public async Task NavigateToAsync<T>() where T : ParentComponent
        {
            //NavigateTo(typeof(T));
            try
            {
                (App.Current.MainPage as MasterDetailPage).IsPresented = false;
                ContentPage newPage = new ContentPage() { Title = typeof(T).Name };
                await App.NavigationPage.PushAsync(newPage);
                App.Host.AddComponent<T>(parent: newPage);
            }
            catch (Exception ex)
            {

            }
        }
       

        public async Task NavigateBackAsync()
        {
            await App.NavigationPage.PopAsync();

        }

        RenderFragment RenderContent(Type t) => builder =>
        {
            builder.OpenComponent(0, t);
            builder.CloseComponent();
        };
    }

    public class NavigationModel
    {
        public NavigationModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Type Type { get; set; }
        public RenderFragment Content { get; set; }
        public string Title { get; set; }
    }
}
