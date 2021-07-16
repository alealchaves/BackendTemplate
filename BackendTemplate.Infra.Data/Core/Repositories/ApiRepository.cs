using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BackendTemplate.Infra.Data.Core.Repositories
{
    public abstract class ApiRepository
    {
        //forma de utilização
        //public async Task<ServiceResult<bool>> EnviarEmailRedefinirSenhaMobile(EmailBarramentoRequest email)
        //{
        //    var url = "api/email/send";
        //    var request = CreateRequest(url, "POST");

        //    var body = JsonSerializer.Serialize(email);

        //    var postBytes = Encoding.UTF8.GetBytes(body);

        //    var result = await RequestResult<ServiceResult<bool>>(request, postBytes);

        //    return result;
        //}

        protected string baseUrl;

        protected WebRequest CreateRequest(string api, string method)
        {
            return CreateRequest(api, method, null);
        }

        protected WebRequest CreateRequest(string api, string method, string accessTokenHeader,
            string contentType = "application/json; charset=UTF-8")
        {
            return CreateRequest(baseUrl, api, method, accessTokenHeader, contentType);
        }

        protected WebRequest CreateRequest(string baseUrl, string api, string method, string accessTokenHeader,
            string contentType = "application/json; charset=UTF-8")
        {
            var endpoint = $"{baseUrl}{api}";
            var request = WebRequest.Create(endpoint);

            request.Method = method;
            request.ContentType = contentType;

            if (!string.IsNullOrWhiteSpace(accessTokenHeader))
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessTokenHeader}");

            return request;
        }

        protected async Task<T> RequestResult<T>(WebRequest request, byte[] postBytes = null)
        {
            var resultJson = string.Empty;
            var result = default(T);

            if (postBytes != null)
            {
                using (var requestStream = request.GetRequestStream())
                    requestStream.Write(postBytes, 0, postBytes.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                resultJson = await reader.ReadToEndAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(resultJson);
            }

            result = JsonConvert.DeserializeObject<T>(resultJson);

            return result;
        }
    }
}