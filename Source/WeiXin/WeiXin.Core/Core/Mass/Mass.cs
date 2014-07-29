
namespace WeiXin.Core
{
    /// <summary>
    /// 高级群发接口，根据 OpenId
    /// </summary>
    internal sealed class Mass
    {
        internal static void SendMessage(string accessToken, MassJsonMessage msg)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", accessToken);
            var json = msg.GetJson();
            Log.Debug("\r\n群发消息 json 数据：>>\r\n{0}", json);
            var resultJson = HttpRequestHelper.PostHttp_ForamtByJson(url, json);
            Log.Debug("\r\n群发消息 json 返回值：>>\r\n{0}", resultJson);
            var returnCode = GlobalReturnCode.GetReturnCode(resultJson);
            if (!returnCode.IsRequestSuccess)
            {
                throw new WeixinRequestApiException(string.Format("发送群发消息失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
            }
        }
    }
}
