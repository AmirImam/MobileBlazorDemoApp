using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileBlazorDemoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileBlazorDemoApp.Droid
{
    public class ToastService : IToast
    {
        public void MakeToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}