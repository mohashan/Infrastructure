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
        Task<string> SendAsync(Uri uri, HttpMethod method, string body, string? AuthorizationToken);
    }
    public class HttpRequester : IHttpRequester
    {
        private readonly HttpClient httpClient;

        public HttpRequester(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<string> SendAsync(Uri uri, HttpMethod method, string body, string? AuthorizationToken)
        {
            StringContent? content = null;
            if (body != string.Empty)
                content = new StringContent(body);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = content,
                Method = method,
            };

            httpClient.BaseAddress = uri;
            
            Dictionary<string,string> headers = new Dictionary<string, string>
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
            };

            if (!string.IsNullOrEmpty(AuthorizationToken))
                headers["Authorization"] = AuthorizationToken;

            AddHeaders(request, headers);

            return sendRequest(request);
        }

        private void AddHeaders(HttpRequestMessage message,Dictionary<string,string> headers)
        {
            foreach (var header in headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

        }

        private async Task<string> sendRequest(HttpRequestMessage message)
        {
            var response = await httpClient.SendAsync(message);
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
