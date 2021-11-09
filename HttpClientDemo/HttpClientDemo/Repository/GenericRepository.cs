using Newtonsoft.Json;
using Repository.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository : IGenericRepository
    {
        private HttpClient httpClient;

        HttpClientHandler httpClientHandler = new HttpClientHandler();

        public GenericRepository()
        {
    #if DEBUG
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
    #endif
            httpClient = new HttpClient(httpClientHandler);
        }

        #region GET
        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                string jsonResult = string.Empty;

                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region POST
        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PostAsync(uri, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }
       
        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PostAsync(uri, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region PUT
        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PutAsync(uri, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                    responseMessage.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region DELETE
        public async Task DeleteAsync(string uri, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);
                await httpClient.DeleteAsync(uri);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region HELPER
        private void ConfigureHttpClient(string authToken)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
        #endregion
    }
}
