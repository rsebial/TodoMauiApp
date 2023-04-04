using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoMauiApp.Models;

namespace TodoMauiApp.DataServices
{
    public class RestDataServices : IRestDataServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataServices() 
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5160" : "https://localhost:7175";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task AddTodoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet A  gvccess");
                return;
            }

            try
            {
                string jsonTodo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Created Todo: {response.StatusCode}");
                }
                else
                {
                    Debug.WriteLine($"---->Non Http 2xx response {response.StatusCode}");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception Error: {ex.Message}");
            }

            return;
        }

        public async Task DeleteTodoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Created Todo: {response.StatusCode}");
                }
                else
                {
                    Debug.WriteLine($"---->Non Http 2xx response {response.StatusCode}");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception Error: {ex.Message}");

            }
        }

        public async Task<List<ToDo>> GetAllTodosAsync()
        {
            List<ToDo> list = new List<ToDo>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access");
                return list;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSerializerOptions);

                }
                else{
                    Debug.WriteLine($"---->Non Http 2xx response {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception Error: {e.Message}");
            }

            return list;
        }

        public async Task UpdateTodoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access");
                return;
            }

            try
            {
                string jsonTodo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{toDo.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Created Todo: {response.StatusCode}");
                }
                else
                {
                    Debug.WriteLine($"---->Non Http 2xx response {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Error: {ex.Message}");
            }

            return;
        }
    }
}
