using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings;
using MobileBlazorDemoApp.Bases;
using MobileBlazorDemoApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileBlazorDemoApp.Views
{
    public partial class UserAccount : ParentComponent
    {
        [Inject]
        private UserService Service { get; set; }
        private User registerModel = new User();
        private User loginModel = new User() { Email = "amir@imam.com", Password = "123456" };
        private enum DisplayTypes
        {
            Login,
            Register
        }
        private DisplayTypes DisplayType { get; set; }
        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender == true)
            {
                SubmitLogin();
            }
        }

        private void SwitchGander()
        {
            if(registerModel.Gander == Ganders.Male)
            {
                registerModel.Gander = Ganders.Female;
            }
            else
            {
                registerModel.Gander = Ganders.Male;
            }
            StateHasChanged();
        }


        private async void SubmitRegister()
        {
            if (String.IsNullOrEmpty(registerModel.Name) || String.IsNullOrEmpty(registerModel.Email) || String.IsNullOrEmpty(registerModel.Password))
            {
                MakeToast("Please fill all data");
                return;
            }
            Busy = true;
            var result = await Service.RegisterAsync(registerModel);
            if(result.Success == true)
            {
                loginModel = result.Model;
                DisplayType = DisplayTypes.Login;
                StateHasChanged();
            }
            else
            {
                MakeToast(result.Exception.Message);
            }
            Busy = false;
        }
        
        private async void SubmitLogin()
        {
            if (String.IsNullOrEmpty(loginModel.Email) || String.IsNullOrEmpty(loginModel.Password))
            {
                MakeToast("Fill all data");
                return;
            }
            Busy = true;
            var result = await Service.LoginAsync(loginModel);
            if(result.Success == true)
            {
                OnLogged(result.Model);
                //Navigator.NavigateTo<Index>();
                //Scope.RefreshMainLayout();
            }
            Busy = false;
        }

        private void OnLogged(User user)
        {
            Scope.Me = user;
            App.DetailPage = new ContentPage() { Title = "Home" };
            App.NavigationPage = new NavigationPage(App.DetailPage);
            App.DrawerParentPage = new ContentPage() { Title = "Drawer" };
            MasterDetailPage mainPage = new MasterDetailPage();
            mainPage.Master = App.DrawerParentPage;
            mainPage.Detail = App.NavigationPage;
            App.Current.MainPage = mainPage;
            App.Host.AddComponent<Index>(parent: App.DetailPage);
            App.Host.AddComponent<Drawer>(parent: App.DrawerParentPage);
        }
    }
}
