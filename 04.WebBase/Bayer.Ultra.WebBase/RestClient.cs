using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.WebBase
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RestClient : IDisposable
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }

        public RestClient(string endpoint) : this(endpoint, HttpVerb.GET, "application/json")
        {
        }
        public RestClient(string endpoint, HttpVerb method) : this(endpoint, method, "application/json")
        {
        }

        public RestClient(string endpoint, HttpVerb method, string contentType)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
        }

        public string MakeRequest(string parameters)
        {
            string responseData = string.Empty;
            System.Net.WebRequest request = System.Net.WebRequest.Create(EndPoint);

            try
            {
                switch (Method)
                {
                    case HttpVerb.GET:
                        break;
                    case HttpVerb.POST:
                        request.Method = Method.ToString();
                        request.ContentLength = parameters.Length;
                        request.ContentType = ContentType;
                        using (StreamWriter requestWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            requestWriter.Write(parameters);
                        }

                        using (StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream()))
                        {
                            responseData = responseReader.ReadToEnd();

                        }
                        break;
                }
                return responseData;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
