﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXin.Core
{
    internal sealed class SendCustomerMessage
    {
        //public static void SendTextMessage(CustomerServiceTextMessage msg)
        //{
        //    SendMessage(msg.GetJson());
        //}
        //public static void SendNewsMessage(CustomerServiceNewsMessage msg)
        //{
        //    SendMessage(msg.GetJson());
        //}
        //private static void SendMessage(string json)
        //{
        //    Task t = new Task(() =>
        //    {
        //        string accessToken = null;
        //        try
        //        {
        //            accessToken = WeiXinAccessTokenManager.GetToken();
        //        }
        //        catch (Exception e)
        //        {
        //            LogHelper.Log(string.Format("发送客服消息失败。\r\n错误消息：\r\n{1}", e.Message));
        //            LogHelper.LogError(e);
        //            return;
        //        }
        //        string api = string.Format("{0}?access_token={1}", ConfigProperty.WeiXin_CustomerServiceApi, accessToken);
        //        LogHelper.LogWeiXinMessage(json);
        //        try
        //        {
        //            var result = HttpRequestHelper.PostHttp_ForamtByJson(api, json);
        //        }
        //        catch (PostHttpErrorException e)
        //        {
        //            LogHelper.Log(string.Format("发送客服消息失败，HTTP 状态码{0}，JSON 数据：\r\n{1}", e.HttpStatusCode, json));
        //        }
        //        catch (Exception e)
        //        {
        //            LogHelper.LogError(e);
        //            LogHelper.Log(string.Format("发送客服消息失败，JSON 数据：\r\n{0}\r\n错误消息：\r\n{1}", json, e.Message));
        //        }
        //    });
        //    t.Start();
        //}
    }
}
