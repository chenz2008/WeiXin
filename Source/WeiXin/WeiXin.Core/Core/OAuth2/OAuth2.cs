using System;

namespace WeiXin.Core
{
    internal sealed class OAuth2
    {
        internal static OAuth2AccessToken GetOAuthAccessToken(string code, string appId, string appSecret)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code);
            var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
            Log.Debug("\r\n获取 OAuth2.0 access_token json返回值：>>\r\n{0}", json);
            var returnCode = GlobalReturnCode.GetReturnCode(json);
            if (!returnCode.IsRequestSuccess)
            {
                throw new WeixinRequestApiException(string.Format("获取 OAuth2.0 access_token 失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
            }
            var oAuth2AccessToken = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<OAuth2AccessToken>(json);
            return oAuth2AccessToken;
        }

        internal static OAuth2UserInfo GetUserInfo(string openId, string oAuthAccessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", oAuthAccessToken, openId);
            var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
            Log.Debug("\r\n获取用户信息 json返回值：>>\r\n{0}", json);
            var returnCode = GlobalReturnCode.GetReturnCode(json);
            if (!returnCode.IsRequestSuccess)
            {
                throw new WeixinRequestApiException(string.Format("获取用户信息失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
            }
            var userInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<OAuth2UserInfo>(json);
            return userInfo;
        }
    }
}
