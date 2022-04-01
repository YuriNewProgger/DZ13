using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ShopAPI;

public class ClientAPI
{
    private string _host { get; set; }
    private HttpClient _client { get; set; }

    public ClientAPI(string? host)
    {
        _host = host ?? "https://localhost:7045";
        _client = new HttpClient();
    }
    
    public Task<List<Product>> GetProducts() =>
        _client.GetFromJsonAsync<List<Product>>($"{_host}/Products/GetProducts");

    public Task AddProduct(Product product) =>
        _client.PostAsJsonAsync($"{_host}/AddProducts", product);
    
    public void AddAccount(AccountRequestModel _account) =>
        _client.PostAsJsonAsync($"{_host}/Accounts/AddAccount", _account);

    public async Task<string> Login(AccountRequestModel _account)
    {
        var response = await _client.PostAsJsonAsync($"{_host}/Accounts/Login", _account);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return token;
    }

    public Task<List<Account>> GetAccounts() =>
        _client.GetFromJsonAsync<List<Account>>($"{_host}/Accounts/GetAccounts");
    
}