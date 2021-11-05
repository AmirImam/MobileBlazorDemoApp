using Microsoft.AspNetCore.Components;
using MobileBlazorDemoApp.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBlazorDemoApp.Views.Partial
{
    public partial class DrawerButton<TPage> where TPage : ParentComponent
    {
        [Parameter]
        public string Icon { get; set; }
        [Parameter]
        public string Text { get; set; }

        private async void Navigate()
        {
            await Navigator.NavigateToAsync<TPage>();
        }
    }
}
