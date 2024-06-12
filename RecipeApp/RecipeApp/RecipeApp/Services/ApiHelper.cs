using Newtonsoft.Json;
using RecipeApp.Helpers;
using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public static class ApiHelper
    {
        private static HttpClient httpClient;

        static ApiHelper()
        {
            httpClient = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000 * 100,
                Timeout = TimeSpan.FromSeconds(60),
            };
        }

        /// <summary>
        /// POST API Call
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="requestHeaders"></param>
        /// <returns></returns>
        public static async Task<BaseResponse<TResult>> PostAsync<TResult>(string url,
            string content,
            Dictionary<string, string> requestHeaders = null)
        {
            var result = new BaseResponse<TResult>();
            try
            {
                if (!UtilHelper.IsInternetConnected())
                {
                    result.GetNoInternetResponse();
                    return result;
                }
                Debug.WriteLine("\nPost URL: " + url);
                Debug.WriteLine("POST Request: " + content);

                if (requestHeaders != null)
                    foreach (var e in requestHeaders)
                    {
                        httpClient.DefaultRequestHeaders.Add(e.Key, e.Value);
                        Debug.WriteLine("PostAsync requestHeaders " + e.Key + "  ==  " + e.Value);
                    }

                HttpContent data = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<TResult>(responseData);
                    result.GetSuccessResponse();
                    Debug.WriteLine("\nPost URL: " + url);
                    Debug.WriteLine("POST Request: " + content);
                    Debug.WriteLine("\nPostAsync API Response " + responseData);
                }
                else
                {
                    result = await HandleResponse<TResult>(response);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //AppCenterHelper.ReportCrash(this.ToString(), ex, url);
                result.GetGenericErrorResponse();
                return result;
            }
            return result;
        }

        /// <summary>
        /// GET API Call
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestHeaders"></param>
        /// <returns></returns>
        public static async Task<BaseResponse<TResult>> GetAsync<TResult>(string url, Dictionary<string, string> requestHeaders = null)
        {
            var result = new BaseResponse<TResult>();
            try
            {
                if (!UtilHelper.IsInternetConnected())
                {
                    result.GetNoInternetResponse();
                    return result;
                }

                Debug.WriteLine("URL: " + url);

                if (requestHeaders != null)
                    foreach (var e in requestHeaders)
                        httpClient.DefaultRequestHeaders.Add(e.Key, e.Value);

                //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var jsonResponse = JsonConvert.DeserializeObject<TResult>(responseData);
                    result.Data = jsonResponse;
                    result.GetSuccessResponse();
                    Debug.WriteLine("PostAsync API Response " + JsonConvert.SerializeObject(jsonResponse));
                }
                else
                {
                    result = await HandleResponse<TResult>(response);
                }
            }
            catch (Exception ex)
            {
                //AppCenterHelper.ReportCrash(this.ToString(), ex, url);
                result.GetGenericErrorResponse();
                return result;
            }
            return result;
        }

        /// <summary>
        /// Handle Error Codes
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async Task<BaseResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var result = new BaseResponse<T>();
            string msg = Constants.Global_Error;

            //var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                msg = Constants.UnAuthorized;
            else if (response.StatusCode == HttpStatusCode.RequestTimeout)
                msg = Constants.Connection_Timeout;
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
                msg = Constants.InternalError;
            else if (response.StatusCode == HttpStatusCode.GatewayTimeout)
                msg = Constants.Connection_Timeout;
            else if (response.StatusCode == HttpStatusCode.BadRequest)
                msg = Constants.Bad_Request;

            result.ApiStatus = (int)response.StatusCode;
            result.Success = false;
            result.ErrorMessage = msg;

            //_ = Application.Current.MainPage.DisplayAlert("", msg, "OK");
            return result;
        }
    }
}
