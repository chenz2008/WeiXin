using System;
using System.Collections;
using System.Collections.Generic;

namespace WeiXin.Core
{
    internal sealed class UserManager
    {
        /// <summary>
        /// 获取已关注用户列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        internal static List<string> GetSubscribeUserList(string accessToken)
        {
            var result = new List<string>();
            var next_openid = string.Empty;
            var api = "https://api.weixin.qq.com/cgi-bin/user/get";
            var total = 0;
            var readCount = 0;
            do
            {
                var url = string.Empty;
                if (string.IsNullOrEmpty(next_openid))
                {
                    url = string.Format("{0}?access_token={1}", api, accessToken);
                }
                else
                {
                    url = string.Format("{0}?access_token={1}&next_openid={2}", api, accessToken, next_openid);
                }
                var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                Log.Debug("\r\n获取已关注列表 json返回值：>>\r\n{0}", json);
                var returnCode = GlobalReturnCode.GetReturnCode(json);
                if (returnCode.IsRequestSuccess)
                {
                    var jsonObj = JsonSerializerHelper.Deserialize(json);
                    total = Convert.ToInt32(jsonObj["total"]);
                    var count = Convert.ToInt32(jsonObj["count"]);
                    next_openid = Convert.ToString(jsonObj["next_openid"]);
                    readCount += count;
                    if (count > 0)
                    {
                        var openids = (jsonObj["data"] as Dictionary<string, object>)["openid"];
                        if (openids is ArrayList)
                        {
                            foreach (var item in openids as ArrayList)
                            {
                                result.Add(item.ToString());
                            }
                        }
                        else
                        {
                            Log.Warning("获取已关注列表转换 openid 类型失败，实际类型：" + openids.ToString());
                        }
                    }
                }
                else
                {
                    throw new WeixinRequestApiException(string.Format("获取已关注列表失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
                }
            } while (total > readCount);
            return result;
        }
        /// <summary>
        /// 获取已关注用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        internal static UserInfo GetSubscribeUserInfo(string accessToken, string openId)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", accessToken, openId);
            var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
            Log.Debug("\r\n获取已关注用户信息 json返回值：>>\r\n{0}", json);
            var returnCode = GlobalReturnCode.GetReturnCode(json);
            if (!returnCode.IsRequestSuccess)
            {
                throw new WeixinRequestApiException(string.Format("获取已关注用户信息失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
            }
            var userInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<UserInfo>(json);
            return userInfo;
        }
    }
}
