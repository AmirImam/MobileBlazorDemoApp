using Microsoft.AspNetCore.Components;
using MobileBlazorDemoApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileBlazorDemoApp.Bases
{
    public class ParentComponent : ComponentBase
    {
        [Inject] protected ScopedManager Scope { get; set; }
        [Inject] protected NavigationService Navigator { get; set; }
        protected ImageSource ResourceImageUrl(string key)
        {
            ImageSource source = ImageSource.FromResource($"ResourcesManager.Resources.Images.{key}");
            return source;
        }


        private bool _busy;
        public bool Busy
        {
            get { return _busy; }
            set { _busy = value;StateHasChanged(); }
        }

        protected void MakeToast(string message)
        {
            DependencyService.Get<IToast>().MakeToast(message);
        }

    }
}
