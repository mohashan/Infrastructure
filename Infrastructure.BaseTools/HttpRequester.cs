using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseTools
{

    public interface IHttpRequester
    {
        Task<string> SendAsync(Uri uri, HttpMethod method, string? body, string? AuthorizationToken);
    }
    public class HttpRequester : IHttpRequester
    {
        private readonly HttpClient httpClient;

        public HttpRequester(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> SendAsync(Uri uri, HttpMethod method, string? body, string? AuthorizationToken)
        {
            StringContent content = new StringContent(body);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = content,
                Method = method,
            };

            request.Headers.Add("Accept", "application/json");

            if (AuthorizationToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AuthorizationToken);

            var response = await httpClient.SendAsync(request);
            if (response == null)
            {
                throw new Exception("Response is null");
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Status code Error : {await response.Content.ReadAsStringAsync()}");
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
