using System.Net.Http.Json;

namespace ShopAPI;

public class ClientAPI
{
    private string _host { get; set; }
    private HttpClient _client { get; set; }

    public ClientAPI(string? host)
    {
        _host = host ?? "http://localhost:5045";
        _client = new HttpClient();
    }
    
    public Task<List<Product>> GetProducts() =>
        _client.GetFromJsonAsync<List<Product>>($"{_host}/Products");

    public Task AddProduct(Product product) =>
        _client.PostAsJsonAsync($"{_host}/AddProducts", product);
}