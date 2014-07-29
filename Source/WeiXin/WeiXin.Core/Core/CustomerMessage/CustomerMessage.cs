using System.Threading.Tasks;
using WeiXin.Core.Messages;

namespace WeiXin.Core
{
    internal class CustomerMessage
    {
        internal static void SendCustomerMessage(string accessToken, CustomerJsonMessage msg)
        {
            var json = msg.GetJson();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", accessToken);
            Log.Debug("\r\n发送客服消息 json 数据：>>\r\n{0}", json);
            var result = HttpRequestHelper.PostHttp_ForamtByJson(url, json);
            Log.Debug("\r\n发送客服消息 json 返回值：>>\r\n{0}", result);
            var returnCode = GlobalReturnCode.GetReturnCode(json);
            if (!returnCode.IsRequestSuccess)
            {
                throw new WeixinRequestApiException(string.Format("获取 access_token 失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
            }
        }
    }
}
