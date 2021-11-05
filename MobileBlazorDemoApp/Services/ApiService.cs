using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileBlazorDemoApp.Services
{
    public class ApiService
    {
        private string BaseUrl => "http://adb1-37-243-141-242.ngrok.io/";
        private string ApiUrl => $"{BaseUrl}api/";

        public HttpClient Http { get; }

        public ApiService(HttpClient http)
        {
            Http = http;
        }
        
        public async ValueTask<ResponseResult<T>> PostAsJsonAsync<T>(string controllerName, string actionName, object model)
        {
            return await this.ExecuteAsync<T>("POST", controllerName, actionName, model);
        }

        public async ValueTask<ResponseResult<T>> GetAsJsonAsync<T>(string controllerName,string actionName)
        {
            return await this.ExecuteAsync<T>("GET", controllerName, actionName,null);
        }

        private async ValueTask<ResponseResult<T>> ExecuteAsync<T>(string httpAction, string controllerName, string actionName,object model)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                if (httpAction == "POST")
                {
                    response = await Http.PostAsync($"{ApiUrl}{controllerName}/{actionName}", EntityAsStringContent(model));
                }
                else
                {
                    response = await Http.GetAsync($"{controllerName}/{actionName}");
                }

                string msg = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode == true)
                {
                    return ResponseResult<T>.Create(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(msg));
                }
                return ResponseResult<T>.Create(new Exception(msg));
            }
            catch (Exception ex)
            {
                return ResponseResult<T>.Create(ex);
            }
        }


        private HttpContent EntityAsStringContent(object model)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        } 
    }

    public class ResponseResult<T>
    {
        public T Model { get; set; }
        public Exception Exception { get; set; }
        public bool Success => Exception == null;

        public static ResponseResult<T> Create(T model)
            => new ResponseResult<T>() { Model = model };
        public static ResponseResult<T> Create(Exception ex)
            => new ResponseResult<T>() { Exception = ex };
    }

}
