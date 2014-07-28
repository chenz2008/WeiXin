using System;
using System.IO;
using System.Net;
using System.Text;

namespace WeiXin.Core
{
    internal class PostHttpErrorException : Exception
    {
        private string _Message;
        private int _HttpStatusCode;
        internal PostHttpErrorException(int httpStatusCode, string message)
        {
            this._HttpStatusCode = httpStatusCode;
            this._Message = message;
        }
        internal int HttpStatusCode
        {
            get
            {
                return this._HttpStatusCode;
            }
        }
        public override string Message
        {
            get
            {
                return this._Message;
            }
        }
    }

    internal class HttpRequestHelper
    {
        private static string Http_ForamtByJson(string url, string method = "GET", string json = null)
        {
            var request = HttpWebRequest.Create(url);
            request.Method = method;
            request.Timeout = 600000;
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(json))
            {
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(buffer, 0, buffer.Length);
                    writer.Flush();
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new PostHttpErrorException((int)response.StatusCode, string.Format("请求失败，HTTP 状态码{0}", (int)response.StatusCode));
                }
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        internal static string GetHttp_ForamtByJson(string url)
        {
            return Http_ForamtByJson(url);
        }

        internal static string PostHttp_ForamtByJson(string url, string json)
        {
            return Http_ForamtByJson(url, "POST", json);
        }

        internal static string HttpRequest(string url, string method = "GET", string data = null)
        {
            var request = HttpWebRequest.Create(url);
            request.Method = method;
            request.Timeout = 600000;

            if (!string.IsNullOrEmpty(data))
            {
                var buffer = Encoding.ASCII.GetBytes(data);
                request.ContentLength = data.Length;
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(buffer, 0, buffer.Length);
                    writer.Flush();
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new PostHttpErrorException((int)response.StatusCode, string.Format("请求失败，HTTP 状态码{0}", (int)response.StatusCode));
                }
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
