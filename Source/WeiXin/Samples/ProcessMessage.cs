using System.Collections.Generic;
using System.Threading.Tasks;
using WeiXin.Core;
using WeiXin.Core.Messages;

namespace Samples
{
    // 步骤1
    public class ProcessMessage : IWeiXinService
    {
        /// <summary>
        /// 处理消息的接口，如果回复被动消息，直接回复 xml 格式的消息即可。
        /// 如果不处理或者发送客服消息等直接回复空字符串即可。
        /// </summary>
        /// <param name="receiveXmlMsgType"></param>
        /// <param name="receiveXmlMessage"></param>
        /// <returns></returns>
        public string Process(ReceiveXmlMessageType receiveXmlMsgType, ReceiveXmlMessage receiveXmlMessage)
        {
            string result = string.Empty;
            if (receiveXmlMessage != null)
            {
                // 消息类型
                switch (receiveXmlMsgType)
                {
                    case ReceiveXmlMessageType.Undefined:                          // 未识别出消息类型
                        break;
                    case ReceiveXmlMessageType.Text:                               // 文本消息
                        break;
                    case ReceiveXmlMessageType.Image:                              // 图片消息
                        break;
                    case ReceiveXmlMessageType.Voice:                              // 语音消息
                        result = VoiceAction(receiveXmlMessage);
                        break;
                    case ReceiveXmlMessageType.Video:                              // 视频消息
                        break;
                    case ReceiveXmlMessageType.Location:                           // 地理位置消息
                        break;
                    case ReceiveXmlMessageType.Link:                               // 链接消息
                        break;
                    case ReceiveXmlMessageType.Event_QRCode_Subscribe:             // 用户未关注时扫描二维码事件
                        break;
                    case ReceiveXmlMessageType.Event_QRCode_Scan:                  // 用户已关注时扫描二维码事件
                        break;
                    case ReceiveXmlMessageType.Event_View:                         // 点击菜单跳转链接时事件
                        break;
                    case ReceiveXmlMessageType.Event_Click:                        // 点击菜单拉取消息时事件
                        result = EventClickAction(receiveXmlMessage);
                        break;
                    case ReceiveXmlMessageType.Event_Location:                     // 上报地理位置时事件
                        result = EventLocationAction(receiveXmlMessage);
                        break;
                    case ReceiveXmlMessageType.Event_Subscribe:                    // 关注事件
                        break;
                    case ReceiveXmlMessageType.Event_UnSubscribe:                  // 取消关注事件
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        string VoiceAction(ReceiveXmlMessage receiveMsg)
        {
            var voiceMessage = receiveMsg as ReceiveXmlVoiceMessage;
            var sendMsg = new SendXmlTextMessage();
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.Content = string.Format("语音识别结果为：{0}", voiceMessage.Recognition);
            return sendMsg.ToXml();
        }

        string EventSubscribeAction(ReceiveXmlMessage receiveMsg)
        {
            var sendMsg = new SendXmlTextMessage();
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.Content = "感谢您关注。";
            return sendMsg.ToXml();
        }

        string EventLocationAction(ReceiveXmlMessage receiveMsg)
        {
            var eventMsg = receiveMsg as ReceiveXmlEventMessage;
            var sendMsg = new SendXmlNewsMessage();
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.Articles = new List<SendXmlArticle>();
            sendMsg.Articles.Add(new SendXmlArticle { Title = "你的地理位置信息", Description = string.Format("纬度：{0}\r\n\r\n经度：{1}\r\n\r\n精度：{2}", eventMsg.Latitude, eventMsg.Longitude, eventMsg.Precision), Url = "http://www.wangwenzhuang.com/" });
            return sendMsg.ToXml();
        }

        string EventClickAction(ReceiveXmlMessage receiveMsg)
        {
            var result = string.Empty;
            var eventMsg = receiveMsg as ReceiveXmlEventMessage;
            if (eventMsg.EventKey.Equals("1"))
            {
                var sendMsg = new SendXmlTextMessage();
                sendMsg.ToUserName = receiveMsg.FromUserName;
                sendMsg.FromUserName = receiveMsg.ToUserName;
                sendMsg.Content = "被动文本消息";
                result = sendMsg.ToXml();
            }
            else if (eventMsg.EventKey.Equals("2"))
            {
                var sendMsg = new SendXmlNewsMessage();
                sendMsg.ToUserName = receiveMsg.FromUserName;
                sendMsg.FromUserName = receiveMsg.ToUserName;
                sendMsg.Articles = new List<SendXmlArticle>();
                sendMsg.Articles.Add(new SendXmlArticle { Title = "被动单图文消息", Description = "被动单图文消息，此处省略一万字。。。", PicUrl = "http://h.hiphotos.baidu.com/image/pic/item/c9fcc3cec3fdfc037d970d53d63f8794a5c2266a.jpg", Url = "http://www.wangwenzhuang.com/" });
                result = sendMsg.ToXml();
            }
            else if (eventMsg.EventKey.Equals("3"))
            {
                var sendMsg = new SendXmlNewsMessage();
                sendMsg.ToUserName = receiveMsg.FromUserName;
                sendMsg.FromUserName = receiveMsg.ToUserName;
                sendMsg.Articles = new List<SendXmlArticle>();
                sendMsg.Articles.Add(new SendXmlArticle { Title = "被动多图文消息1", Description = "被动多图文消息，此处省略一万字。。。", PicUrl = "http://h.hiphotos.baidu.com/image/pic/item/c9fcc3cec3fdfc037d970d53d63f8794a5c2266a.jpg", Url = "http://www.wangwenzhuang.com/" });
                sendMsg.Articles.Add(new SendXmlArticle { Title = "被动多图文消息2", Description = "被动多图文消息，此处省略一万字。。。", PicUrl = "http://g.hiphotos.baidu.com/image/pic/item/55e736d12f2eb93895023c7fd7628535e4dd6fcb.jpg", Url = "http://www.wangwenzhuang.com/" });
                sendMsg.Articles.Add(new SendXmlArticle { Title = "被动多图文消息3", Description = "被动多图文消息，此处省略一万字。。。", PicUrl = "http://e.hiphotos.baidu.com/image/pic/item/63d0f703918fa0ec8426f0f7249759ee3c6ddb63.jpg", Url = "http://www.wangwenzhuang.com/" });
                result = sendMsg.ToXml();
            }
            else if (eventMsg.EventKey.Equals("4"))
            {
                Task t = new Task(() =>
                {
                    var sendMsg = new CustomerJsonTextMessage();
                    sendMsg.Touser = receiveMsg.FromUserName;
                    sendMsg.Content = "客服文本消息";
                    WeiXinService.SendCustomerMessage(sendMsg);
                });
                t.Start();
            }
            else if (eventMsg.EventKey.Equals("5"))
            {
                Task t = new Task(() =>
                {
                    var sendMsg = new CustomerJsonNewsMessage();
                    sendMsg.Touser = receiveMsg.FromUserName;
                    var title = "客服单图文消息";
                    var discription = "被动单图文消息，此处省略一万字。。。";
                    var url = "http://www.wangwenzhuang.com/";
                    sendMsg.Articles = new List<CustomerJsonArticleMessage>();
                    sendMsg.Articles.Add(new CustomerJsonArticleMessage { Title = title, Description = discription, PicUrl = "http://h.hiphotos.baidu.com/image/pic/item/c9fcc3cec3fdfc037d970d53d63f8794a5c2266a.jpg", Url = url });
                    WeiXinService.SendCustomerMessage(sendMsg);
                });
                t.Start();
            }
            else if (eventMsg.EventKey.Equals("6"))
            {
                Task t = new Task(() =>
                {
                    var sendMsg = new CustomerJsonNewsMessage();
                    sendMsg.Touser = receiveMsg.FromUserName;
                    var discription = "被动单图文消息，此处省略一万字。。。";
                    var url = "http://www.wangwenzhuang.com/";
                    sendMsg.Articles = new List<CustomerJsonArticleMessage>();
                    sendMsg.Articles.Add(new CustomerJsonArticleMessage { Title = "客服多图文消息1", Description = discription, PicUrl = "http://h.hiphotos.baidu.com/image/pic/item/c9fcc3cec3fdfc037d970d53d63f8794a5c2266a.jpg", Url = url });
                    sendMsg.Articles.Add(new CustomerJsonArticleMessage { Title = "客服多图文消息2", Description = discription, PicUrl = "http://g.hiphotos.baidu.com/image/pic/item/55e736d12f2eb93895023c7fd7628535e4dd6fcb.jpg", Url = url });
                    sendMsg.Articles.Add(new CustomerJsonArticleMessage { Title = "客服多图文消息3", Description = discription, PicUrl = "http://e.hiphotos.baidu.com/image/pic/item/63d0f703918fa0ec8426f0f7249759ee3c6ddb63.jpg", Url = url });
                    WeiXinService.SendCustomerMessage(sendMsg);
                });
                t.Start();
            }
            else if (eventMsg.EventKey.Equals("7"))
            {
                var sendMsg = new SendXmlTextMessage();
                sendMsg.ToUserName = receiveMsg.FromUserName;
                sendMsg.FromUserName = receiveMsg.ToUserName;
                sendMsg.Content = "请说一段语音发来。";
                result = sendMsg.ToXml();
            }
            else if (eventMsg.EventKey.Equals("8"))
            {
                var sendMsg = new SendXmlTextMessage();
                sendMsg.ToUserName = receiveMsg.FromUserName;
                sendMsg.FromUserName = receiveMsg.ToUserName;
                sendMsg.Content = string.Format("<a href=\"http://112.126.67.94/wxtest/ViewOpenId.html?OpenId={0}\">获取OpenId</a>", receiveMsg.FromUserName);
                result = sendMsg.ToXml();
            }
            else if (eventMsg.EventKey.Equals("9"))
            {
                var sendMsg = new SendXmlTextMessage();
                sendMsg.ToUserName = receiveMsg.FromUserName;
                sendMsg.FromUserName = receiveMsg.ToUserName;
                sendMsg.Content = string.Format("OAuth2.0授权分两种，第一种获取获取 OpenId，不弹出授权界面；第二种弹出授权界面，不但能获取 OpenId，还可以获取用户的信息。\r\n<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2f112.126.67.94%2fwxtest%2fOAuth2_snsapi_base.aspx&response_type=code&scope=snsapi_base&state=0#wechat_redirect\">第一种</a>\r\n<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://112.126.67.94/wxtest/OAuth2_snsapi_userinfo.aspx&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect\">第二种</a>", WeiXinConfig.AppId);
                result = sendMsg.ToXml();
            }
            else if (eventMsg.EventKey.Equals("12"))
            {
                Task t = new Task(() =>
                {
                    // 获取已关注列表
                    var openIds = WeiXinService.GetSubscribeUserList();
                    if (openIds != null && openIds.Count > 0)
                    {
                        var discription = string.Empty;
                        // 获取已关注列表每个人的基本信息
                        for (int i = 0; i < openIds.Count; i++)
                        {
                            var userInfo = WeiXinService.GetSubscribeUserInfo(openIds[i]);
                            if (i + 1 == openIds.Count)
                            {
                                discription += string.Format("{0}、{1}", i + 1, userInfo.NickName);
                            }
                            else
                            {
                                discription += string.Format("{0}、{1}\r\n\r\n", i + 1, userInfo.NickName);
                            }
                        }
                        var sendMsg = new CustomerJsonNewsMessage();
                        sendMsg.Touser = receiveMsg.FromUserName;
                        var title = "已关注用户信息";
                        var url = "http://www.wangwenzhuang.com/";
                        sendMsg.Articles = new List<CustomerJsonArticleMessage>();
                        sendMsg.Articles.Add(new CustomerJsonArticleMessage { Title = title, Description = discription, Url = url });
                        WeiXinService.SendCustomerMessage(sendMsg);
                    }
                });
                t.Start();
            }
            return result;
        }
    }
}