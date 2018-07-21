using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Until.Extension
{
    public static class HttpExtension
    {
        public static async Task ProxyUrl(this HttpContext context,string Url)
        {
            try
            {
                var request = context.Request;
                var response = context.Response;
                var contentType = request.ContentType;
                using (HttpClient http = new HttpClient())
                {
                    foreach (var header in request.Headers.Where(item=>!item.Key.Equals("Content-Type") && !item.Key.Equals("Content-Length")))
                    {
                        http.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                    }

                    HttpResponseMessage message = null;
                    if (request.Method.ToLowContains("POST"))
                    {
                        Stream dataStream = request.Body;

                        using (HttpContent content = new StreamContent(dataStream))
                        {
                            content.Headers.Add("Content-Type", contentType);
                            message = await http.PostAsync(Url, content);
                        }
                    }
                    else if (request.Method.ToLowContains("GET"))
                    {
                        message = await http.GetAsync(Url);
                    }
                    if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using (message)
                        {
                            using (Stream responseStream = await message.Content.ReadAsStreamAsync())
                            {
                                if (responseStream != null)
                                {
                                    byte[] responseData = new byte[responseStream.Length];
                                    responseStream.Read(responseData, 0, responseData.Length);

                                    var responseStr = Encoding.UTF8.GetString(responseData);

                                    await response.WriteAsync(responseStr, Encoding.UTF8);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
