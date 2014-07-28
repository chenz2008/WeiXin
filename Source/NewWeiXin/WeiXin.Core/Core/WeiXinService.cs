using System;
using System.Collections.Generic;
using System.Reflection;
using WeiXin.Core.Messages;

namespace WeiXin.Core
{
    public class WeiXinServiceException : Exception
    {
        private string _Message;
        public override string Message
        {
            get
            {
                return this._Message;
            }
        }
        public WeiXinServiceException()
        {
        }
        public WeiXinServiceException(string message)
        {
            this._Message = message;
        }
    }
    public sealed class WeiXinService
    {
        private static string _AppId, _AppSecret;
        private static IWeiXinService _Service;
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
        public static bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            return SignatureHelper.CheckSignature(token, signature, timestamp, nonce);
        }
        public static string CreateToXmlTextMessageXml(ReceiveXmlMessage receiveMsg, string content)
        {
            string text = new SendXmlTextMessage
            {
                ToUserName = receiveMsg.FromUserName,
                FromUserName = receiveMsg.ToUserName,
                CreateTime = SignatureHelper.CreateTimestamp(),
                Content = content
            }.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", text);
            return text;
        }
        public static string CreateToXmlVideoMessageXml(ReceiveXmlMessage receiveMsg, string mediaId, string title, string description)
        {
            string text = new SendXmlVideoMessage
            {
                ToUserName = receiveMsg.FromUserName,
                FromUserName = receiveMsg.ToUserName,
                CreateTime = SignatureHelper.CreateTimestamp(),
                MediaId = mediaId,
                Title = title,
                Description = description
            }.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", text);
            return text;
        }
        public static string CreateToXmlMusicMessageXml(ReceiveXmlMessage receiveMsg, string title, string description, string musicURL, string hQMusicUrl, string thumbMediaId)
        {
            string text = new SendXmlMusicMessage
            {
                ToUserName = receiveMsg.FromUserName,
                FromUserName = receiveMsg.ToUserName,
                CreateTime = SignatureHelper.CreateTimestamp(),
                Title = title,
                Description = description,
                MusicURL = musicURL,
                HQMusicUrl = hQMusicUrl,
                ThumbMediaId = thumbMediaId
            }.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", text);
            return text;
        }
        public static string CreateToXmlVoiceMessageXml(ReceiveXmlMessage receiveMsg, string mediaId)
        {
            string text = new SendXmlVoiceMessage
            {
                ToUserName = receiveMsg.FromUserName,
                FromUserName = receiveMsg.ToUserName,
                CreateTime = SignatureHelper.CreateTimestamp(),
                MediaId = mediaId
            }.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", text);
            return text;
        }
        public static string CreateToXmlNewsMessageXml(ReceiveXmlMessage receiveMsg, List<SendXmlArticle> sendXmlArticle)
        {
            if (sendXmlArticle.Count > 10)
            {
                throw new WeiXinServiceException("图文消息不能超过10条。");
            }
            SendXmlNewsMessage sendXmlNewsMessage = new SendXmlNewsMessage();
            sendXmlNewsMessage.ToUserName = receiveMsg.FromUserName;
            sendXmlNewsMessage.FromUserName = receiveMsg.ToUserName;
            sendXmlNewsMessage.CreateTime = SignatureHelper.CreateTimestamp();
            sendXmlNewsMessage.Articles = sendXmlArticle;
            sendXmlNewsMessage.ArticleCount = sendXmlNewsMessage.Articles.Count;
            string text = sendXmlNewsMessage.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", text);
            return text;
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
