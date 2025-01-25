using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorFrontend.Client.Models;
using Microsoft.JSInterop;

namespace BlazorFrontend.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ApiService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        // 新規追加: ログイン機能
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var loginRequest = new LoginRequest { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("https://<USER_MANAGEMENT_SERVICE_URL>/api/user/login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    // トークンをセッションストレージに保存
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", loginResponse.Token);
                }
                return loginResponse;
            }
            return null;
        }

        // 認証ヘッダーの追加
        private async Task AddAuthorizationHeaderAsync()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        // トークンの取得
        private async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
        }

        // API呼び出しに認証ヘッダーを追加
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<IEnumerable<Company>>("https://<COMPANY_MANAGEMENT_SERVICE_URL>/api/company");
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("https://<USER_MANAGEMENT_SERVICE_URL>/api/user");
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<IEnumerable<Item>>("https://<ITEM_MANAGEMENT_SERVICE_URL>/api/item");
        }

        // 必要に応じて、CRUD操作のメソッドを追加
    }
}