using System.Text.Json;

public class ServicioWeb
{
    public async Task<T> GetData<T>(string url)
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            T data = JsonSerializer.Deserialize<T>(responseBody);
            return data;
        }
        catch (HttpRequestException err)
        {
            Console.WriteLine(err.Message);
            return default;
        }
    }
}