using Newtonsoft.Json;
using System.Net.Http.Headers;
using WorkTimeRegistrationShared.Infrastructure;

namespace WorkTimeRegistrationApp.Infrastructure;

public class RequestProvider
{
    private readonly Lazy<HttpClient> _httpClient = new Lazy<HttpClient>(() =>
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
        httpClientHandler.UseDefaultCredentials = true;
        var httpClient = new HttpClient(httpClientHandler);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    },
       LazyThreadSafetyMode.ExecutionAndPublication);

    public async Task<Result<TResult>> GetAsync<TResult>(string uri) where TResult : class
    {
        HttpClient httpClient = _httpClient.Value;
        HttpResponseMessage response = await httpClient.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            string serialized = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(serialized);
            return new Success<TResult>(result!);
        }
        return new Failure<TResult>(response.StatusCode.ToString());
    }

    public async Task<HttpResponseMessage> PostAsync<TIn>(string uri, TIn data)
    {
        HttpClient httpClient = _httpClient.Value;
        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = (await httpClient.PostAsync(uri, content)).EnsureSuccessStatusCode();
        return response;

    }
}
