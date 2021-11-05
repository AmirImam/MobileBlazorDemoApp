using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBlazorDemoApp.Services
{
    public class ScopedManager
    {
        public User Me { get; set; }
        public Action RefreshMainLayout { get; set; }
        public Action<bool> SetBusyState { get; set; }
    }
}
