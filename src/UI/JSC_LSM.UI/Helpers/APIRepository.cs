using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Helpers
{
    public class APIRepository
    {

        //private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public IOptions<ApiBaseUrl> _apiBaseUrl { get; private set; }


        //public APIRepository(IOptions<ApiBaseUrl> apiBaseUrl, IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _apiBaseUrl = apiBaseUrl; //_configuration.GetSection("ApiBaseUrl").GetSection("KibuApiBaseUrl").Value;
        //}

        public APIRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            //_apiBaseUrl = apiBaseUrl; //_configuration.GetSection("ApiBaseUrl").GetSection("KibuApiBaseUrl").Value;
        }

        #region APICommunication - Common Method for API calling

        public async Task<APICommunicationResponseModel<string>> APICommunication(string URL, HttpMethod invokeType, ByteArrayContent body, string token)
        {
            APICommunicationResponseModel<string> response = new APICommunicationResponseModel<string>();
            response.statusCode = HttpStatusCode.BadRequest;
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(_apiBaseUrl + URL);
                    client.BaseAddress = new Uri("https://localhost:44330" + URL);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    HttpResponseMessage oHttpResponseMessage = new HttpResponseMessage();

                    if (invokeType.Method == HttpMethod.Get.ToString())
                    {
                        var responseTask = await client.GetAsync(client.BaseAddress);
                        responseTask.EnsureSuccessStatusCode();
                        var response1 = await responseTask.Content.ReadAsStringAsync();

                        oHttpResponseMessage = responseTask;
                    }
                    else if (invokeType.Method == HttpMethod.Post.ToString())
                    {
                        if (body != null)
                            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var responseTask = await client.PostAsync(client.BaseAddress, body);
                        responseTask.EnsureSuccessStatusCode();
                        oHttpResponseMessage = responseTask;

                    }
                    else if (invokeType.Method == HttpMethod.Put.ToString())
                    {
                        if (body != null)
                            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var responseTask = await client.PostAsync(URL, body);
                        responseTask.EnsureSuccessStatusCode();

                        oHttpResponseMessage = responseTask;
                    }
                    else if (invokeType.Method == HttpMethod.Delete.ToString())
                    {
                        var responseTask = await client.DeleteAsync(URL);
                        responseTask.EnsureSuccessStatusCode();

                        oHttpResponseMessage = responseTask;
                    }

                    response.statusCode = oHttpResponseMessage.StatusCode;
                    if (oHttpResponseMessage.IsSuccessStatusCode)
                    {
                        response.data = oHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        response.Success = true;

                    }
                    else
                        response.data = string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                //throw ex;
            }

            return response;
        }


        //public async Task<APICommunicationResponseModel<string>> APICommunicationImageupload(string URL, HttpMethod invokeType, ByteArrayContent body, string token, HttpPostedFileBase file)
        //{
        //    APICommunicationResponseModel<string> response = new APICommunicationResponseModel<string>();
        //    response.statusCode = HttpStatusCode.BadRequest;
        //    try
        //    {

        //        using (var client = new HttpClient())
        //        {
        //            using (var content = new MultipartFormDataContent())
        //            {
        //                client.BaseAddress = new Uri(_apiBaseUrl + URL);
        //                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //                HttpResponseMessage oHttpResponseMessage = new HttpResponseMessage();

        //                if (invokeType.Method == HttpMethod.Get.ToString())
        //                {
        //                    var responseTask = await client.GetAsync(client.BaseAddress);
        //                    responseTask.EnsureSuccessStatusCode();
        //                    var response1 = await responseTask.Content.ReadAsStringAsync();

        //                    oHttpResponseMessage = responseTask;
        //                }
        //                else if (invokeType.Method == HttpMethod.Post.ToString())
        //                {
        //                    //byte[] Bytes = body;//new byte[file.InputStream.Length + 1];
        //                    //file.InputStream.Read(Bytes, 0, Bytes.Length);
        //                    //var fileContent = new ByteArrayContent(Bytes);
        //                    //fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };

        //                    content.Add(body, "file", file.FileName);

        //                    //content.Add(fileContent);

        //                    //client.DefaultRequestHeaders.Clear();
        //                    //client.DefaultRequestHeaders.Accept.Clear();
        //                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        //                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

        //                    var responseTask = client.PostAsync(client.BaseAddress, content).Result;
        //                    responseTask.EnsureSuccessStatusCode();

        //                    oHttpResponseMessage = responseTask;
        //                }
        //                else if (invokeType.Method == HttpMethod.Put.ToString())
        //                {
        //                    if (body != null)
        //                        body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //                    var responseTask = await client.PostAsync(URL, body);
        //                    responseTask.EnsureSuccessStatusCode();

        //                    oHttpResponseMessage = responseTask;
        //                }
        //                else if (invokeType.Method == HttpMethod.Delete.ToString())
        //                {
        //                    var responseTask = await client.DeleteAsync(URL);
        //                    responseTask.EnsureSuccessStatusCode();

        //                    oHttpResponseMessage = responseTask;
        //                }

        //                response.statusCode = oHttpResponseMessage.StatusCode;
        //                if (oHttpResponseMessage.IsSuccessStatusCode)
        //                {
        //                    response.data = oHttpResponseMessage.Content.ReadAsStringAsync().Result;
        //                }
        //                else
        //                    response.data = string.Empty;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        //throw ex;
        //    }
        //    return response;
        //}

        private static StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            try
            {
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                {
                    Name = "UploadedFile",
                    FileName = "\"" + fileName + "\""
                };
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                return fileContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion APICommunication - Common Method for API calling
    }

    public class APICommunicationResponseModel<T>
    {
        public HttpStatusCode statusCode { get; set; }
        public T data { get; set; }
        public String Message { get; set; }
        public Boolean Success { get; set; }
        public string NotificationType { get; set; }
        public string ReturnToUrl { get; set; }
        private List<ErrorMessage> ErrorMessages { get; set; }
        private string Token { get; set; }
    }

    public class ErrorMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; }

        public string Message { get; }

        public ErrorMessage(string code, string message)
        {
            Code = code != string.Empty ? code : null;
            Message = message;
        }
    }
}
