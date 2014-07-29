/*
 * 使用步骤：
 * 1、首先实现 IWeiXinService 接口，Process 方法为处理微信服务器发来的消息作出的回应。
 * 2、注册实现的接口 WeiXinService.Register(_Service);
 * 3、在接口入口处调用 WeiXinService.ProcessMessage(xml);
 * 在 Process 方法中专注的开发你的功能吧:)
 * 
 * 所有步骤在代码中可以找到，有注释。
 * 
 * 备注：发送被动文本消息指定接收微信号时只需要将接收到的消息发送方微信号做一个转换即可，类似于下面的代码
 * sendMsg.ToUserName = receiveMsg.FromUserName;
 * sendMsg.FromUserName = receiveMsg.ToUserName;
 * 发送被动消息，只需要创建一个发送被动消息的实体，调用 ToXml 方法返回即可。
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WeiXin.Core.Messages;

namespace WeiXin.Core
{
    public sealed class WeiXinService
    {
        private static string _AppId, _AppSecret;
        private static IWeiXinService _Service;
        /// <summary>
        /// 注册微信服务
        /// </summary>
        /// <param name="service">服务接口</param>
        /// <param name="appId">appid</param>
        /// <param name="appSecret">appsecret</param>
        public static void Register(IWeiXinService service, string appId, string appSecret)
        {
            if (service == null || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appSecret))
            {
                throw new WeiXinServiceException("参数不能为空或 null。");
            }
            _Service = service;
            _AppId = appId;
            _AppSecret = appSecret;
        }
        /// <summary>
        /// 微信服务器消息处理
        /// </summary>
        /// <param name="xml">微信服务器发来的 xml 数据</param>
        /// <returns></returns>
        public static string ProcessMessage(string xml)
        {
            Log.Debug("\r\nReceive>>\r\n{0}", xml);
            string result = string.Empty;
            if (WeiXinService._Service != null)
            {
                ReceiveXmlMessage receiveXmlMessage = WeiXinService.ConvertReceiveXmlToReceiveXmlMessage(xml);
                if (receiveXmlMessage == null)
                {
                    Log.Error("转换消息失败，xml：\r\n{0}", xml);
                }
                else
                {
                    ReceiveXmlMessageType receiveXmlMessageType = WeiXinService.GetReceiveXmlMessageType(receiveXmlMessage);
                    result = WeiXinService._Service.Process(receiveXmlMessageType, receiveXmlMessage);
                }
            }
            return result;
        }
        /// <summary>
        /// 效验消息的合法性
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="signature">signature</param>
        /// <param name="timestamp">timestamp</param>
        /// <param name="nonce">nonce</param>
        /// <returns></returns>
        public static bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            return SignatureHelper.CheckSignature(token, signature, timestamp, nonce);
        }
        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="msg">消息</param>
        public static void SendCustomerMessage(CustomerJsonMessage msg)
        {
            var accessToken = AccessToken.GetAccessToken(_AppId, _AppSecret);
            CustomerMessage.SendCustomerMessage(accessToken, msg);
        }
        /// <summary>
        /// 获取 OAuth 2.0 access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static OAuth2AccessToken GetOAuthAccessToken(string code)
        {
            return OAuth2.GetOAuthAccessToken(code, _AppId, _AppSecret);
        }
        /// <summary>
        /// OAuth 2.0 验证通过获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="oAuthAccessToken">OAuth 2.0 access_token</param>
        /// <returns></returns>
        public static OAuth2UserInfo GetOAuthUserInfo(string openId, string oAuthAccessToken)
        {
            return OAuth2.GetUserInfo(openId, oAuthAccessToken);
        }
        /// <summary>
        /// 获取已关注用户列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSubscribeUserList()
        {
            var accessToken = AccessToken.GetAccessToken(_AppId, _AppSecret);
            return UserManager.GetSubscribeUserList(accessToken);
        }
        /// <summary>
        /// 根据 OpenId 获取已关注用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static UserInfo GetSubscribeUserInfo(string openId)
        {
            var accessToken = AccessToken.GetAccessToken(_AppId, _AppSecret);
            return UserManager.GetSubscribeUserInfo(accessToken, openId);
        }
        /// <summary>
        /// 创建临时带参数二维码
        /// </summary>
        /// <param name="sceneId">参数值</param>
        /// <returns></returns>
        public static QRCodeInfo CreateQR_SCENE(long sceneId)
        {
            var accessToken = AccessToken.GetAccessToken(_AppId, _AppSecret);
            return QRCode.CreateQR_SCENE(sceneId, accessToken);
        }
        /// <summary>
        /// 创建永久带参数二维码
        /// </summary>
        /// <param name="sceneId">参数值</param>
        /// <returns></returns>
        public static QRCodeInfo CreateQR_LIMIT_SCEN(long sceneId)
        {
            var accessToken = AccessToken.GetAccessToken(_AppId, _AppSecret);
            return QRCode.CreateQR_LIMIT_SCEN(sceneId, accessToken);
        }
        /// <summary>
        /// 高级群发消息（根据 OpenId）
        /// </summary>
        /// <param name="msg"></param>
        public static void SendMessage(MassJsonMessage msg)
        {
            var accessToken = AccessToken.GetAccessToken(_AppId, _AppSecret);
            Mass.SendMessage(accessToken, msg);
        }
        private static ReceiveXmlMessage ConvertReceiveXmlToReceiveXmlMessage(string receiveXml)
        {
            Dictionary<string, string> dictionary = XmlHelper.Read(receiveXml);
            string text = dictionary["MsgType"];
            ReceiveXmlMessage receiveXmlMessage = null;
            if (text.Equals("text"))
            {
                receiveXmlMessage = new ReceiveXmlTextMessage();
            }
            else if (text.Equals("image"))
            {
                receiveXmlMessage = new ReceiveXmlImageMessage();
            }
            else if (text.Equals("voice"))
            {
                receiveXmlMessage = new ReceiveXmlVoiceMessage();
            }
            else if (text.Equals("video"))
            {
                receiveXmlMessage = new ReceiveXmlVideoMessage();
            }
            else if (text.Equals("location"))
            {
                receiveXmlMessage = new ReceiveXmlLocationMessage();
            }
            else if (text.Equals("link"))
            {
                receiveXmlMessage = new ReceiveXmlLinkMessage();
            }
            else if (text.Equals("event"))
            {
                receiveXmlMessage = new ReceiveXmlEventMessage();
            }
            if (receiveXmlMessage != null)
            {
                Type type = receiveXmlMessage.GetType();
                PropertyInfo[] properties = type.GetProperties();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo propertyInfo = array[i];
                    object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(MessagePropertyNameAttribute), true);
                    if (customAttributes != null && customAttributes.Length > 0)
                    {
                        MessagePropertyNameAttribute messagePropertyNameAttribute = (MessagePropertyNameAttribute)customAttributes[0];
                        if (dictionary.ContainsKey(messagePropertyNameAttribute.PropertyName))
                        {
                            string value = dictionary[messagePropertyNameAttribute.PropertyName];
                            propertyInfo.SetValue(receiveXmlMessage, value, null);
                        }
                    }
                }
                receiveXmlMessage.Xml = receiveXml;
            }
            return receiveXmlMessage;
        }
        private static ReceiveXmlMessageType GetReceiveXmlMessageType(ReceiveXmlMessage receiveXmlMessage)
        {
            if (receiveXmlMessage.MsgType.ToLower().Equals("text"))
            {
                return ReceiveXmlMessageType.Text;
            }
            if (receiveXmlMessage.MsgType.ToLower().Equals("image"))
            {
                return ReceiveXmlMessageType.Image;
            }
            if (receiveXmlMessage.MsgType.ToLower().Equals("voice"))
            {
                return ReceiveXmlMessageType.Voice;
            }
            if (receiveXmlMessage.MsgType.ToLower().Equals("video"))
            {
                return ReceiveXmlMessageType.Video;
            }
            if (receiveXmlMessage.MsgType.ToLower().Equals("location"))
            {
                return ReceiveXmlMessageType.Location;
            }
            if (receiveXmlMessage.MsgType.ToLower().Equals("link"))
            {
                return ReceiveXmlMessageType.Link;
            }
            if (!receiveXmlMessage.MsgType.ToLower().Equals("event"))
            {
                return ReceiveXmlMessageType.Undefined;
            }
            ReceiveXmlEventMessage receiveXmlEventMessage = receiveXmlMessage as ReceiveXmlEventMessage;
            if (!string.IsNullOrEmpty(receiveXmlEventMessage.Ticket))
            {
                if (receiveXmlEventMessage.Event.ToLower().Equals("subscribe"))
                {
                    return ReceiveXmlMessageType.Event_QRCode_Subscribe;
                }
                if (receiveXmlEventMessage.Event.ToLower().Equals("scan"))
                {
                    return ReceiveXmlMessageType.Event_QRCode_Scan;
                }
                return ReceiveXmlMessageType.Undefined;
            }
            else
            {
                if (receiveXmlEventMessage.Event.ToLower().Equals("view"))
                {
                    return ReceiveXmlMessageType.Event_View;
                }
                if (receiveXmlEventMessage.Event.ToLower().Equals("click"))
                {
                    return ReceiveXmlMessageType.Event_Click;
                }
                if (receiveXmlEventMessage.Event.ToLower().Equals("location"))
                {
                    return ReceiveXmlMessageType.Event_Location;
                }
                if (receiveXmlEventMessage.Event.ToLower().Equals("subscribe"))
                {
                    return ReceiveXmlMessageType.Event_Subscribe;
                }
                if (receiveXmlEventMessage.Event.ToLower().Equals("unsubscribe"))
                {
                    return ReceiveXmlMessageType.Event_UnSubscribe;
                }
                return ReceiveXmlMessageType.Undefined;
            }
        }
    }
}
