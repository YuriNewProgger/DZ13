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

    public async Task<HttpResponseMessage> Authorization(AccountRequestModel _account) =>
        await _client.PostAsJsonAsync($"{_host}/Accounts/Authorization", _account);
    
    public Task<List<Account>> GetAccounts() =>
        _client.GetFromJsonAsync<List<Account>>($"{_host}/Accounts/GetAccounts");
    
        
}