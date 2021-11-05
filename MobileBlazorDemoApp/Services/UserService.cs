using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileBlazorDemoApp.Services
{
    public class UserService
    {
        public UserService(ApiService service)
        {
            Service = service;
        }

        public ApiService Service { get; }

        public async ValueTask<ResponseResult<User>> RegisterAsync(User user)
        {
            return await Service.PostAsJsonAsync<User>("User", "Register", user);
        }
        public async ValueTask<ResponseResult<User>> LoginAsync(User user)
        {
            return await Service.PostAsJsonAsync<User>("User", "Login", user);
        }
        
    }
}
