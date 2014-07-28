using System;
using System.Collections.Generic;
namespace WeiXin.Core.Core
{
    internal sealed class AccessToken
    {
        private static string _AccessToken;
        private static int _ExpiresIn;
        private static DateTime _LastGetDatetime;
        private static object obj = new object();

        private static bool CheckAccessToken()
        {
            var result = default(bool);
            if (!string.IsNullOrEmpty(_AccessToken))
            {
                var seconds = (int)DateTime.Now.Subtract(_LastGetDatetime).TotalSeconds;
                result = _ExpiresIn - seconds < 600;
            }
            else
            {
                result = true;
            }
            return result;
        }
        public static string GetAccessToken(string appId, string appSecret)
        {
            if (CheckAccessToken())
            {
                lock (obj)
                {
                    if (CheckAccessToken())
                    {
                        _AccessToken = null;
                        var result = GetAccessTokenDic(appId, appSecret);
                        if (result != null)
                        {
                            _AccessToken = (string)result["access_token"];
                            _ExpiresIn = int.Parse(result["expires_in"].ToString());
                            _LastGetDatetime = DateTime.Now;
                        }
                    }
                }
            }
            return _AccessToken;
        }
        private static Dictionary<string, object> GetAccessTokenDic(string appId, string _AppSecret)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, _AppSecret);
            var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
            var returnCode = GlobalReturnCode.GetReturnCode(json);
            if (returnCode.IsRequestSuccess)
            {
                var jsonObj = JsonSerializerHelper.Deserialize(json);
                return jsonObj;
            }
            else
            {
                throw new Exception(string.Format("获取 access_token 失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url));
            }
        }
    }
}
