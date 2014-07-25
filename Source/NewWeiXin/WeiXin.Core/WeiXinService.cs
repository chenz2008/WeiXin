using System;
using System.Linq;
using WeiXin.Core.Messages;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace WeiXin.Core
{
    /// <summary>
    /// 微信核心服务接口
    /// </summary>
    public sealed class WeiXinService
    {
        static IWeiXinService _Service;
        /// <summary>
        /// 注册接收消息服务
        /// </summary>
        /// <param name="service"></param>
        public static void Register(IWeiXinService service)
        {
            if (service == null)
                throw new WeiXinServiceException("service 不能为 null。");
            _Service = service;
        }
        /// <summary>
        /// 处理微信服务器发来的消息，请确保已注册（Register）
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string ProcessMessage(string xml)
        {
            Log.Debug("\r\nReceive>>\r\n{0}", xml);
            var result = string.Empty;
            if (_Service != null)
            {
                var msg = ConvertReceiveXmlToXmlReceiveMessage(xml);
                if (msg == null)
                {
                    Log.Error("转换消息失败，xml：\r\n{0}", xml);
                }
                else
                {
                    var msgType = GetXmlReceiveMessageType(msg);
                    result = _Service.Process(msgType, msg);
                }
            }
            return result;
        }
        /// <summary>
        /// 验证微信调用接口签名
        /// </summary>
        /// <returns></returns>
        public static bool CheckSignature(
            string token,
            string signature,
            string timestamp,
            string nonce)
        {
            return SignatureHelper.CheckSignature(token, signature, timestamp, nonce);
        }
        /// <summary>
        /// （回复）创建被动文本消息
        /// </summary>
        /// <param name="receiveMsg">接收到的消息</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string CreateToXmlTextMessageXml(XmlReceiveMessage receiveMsg, string content)
        {
            var sendMsg = new XmlSendTextMessage();
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.CreateTime = SignatureHelper.CreateTimestamp();
            sendMsg.Content = content;
            var xml = sendMsg.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", xml);
            return xml;
        }
        /// <summary>
        /// （回复）创建被动图文消息
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="articles"></param>
        /// <returns></returns>
        public static string CreateToXmlNewsMessageXml(XmlReceiveMessage receiveMsg, List<XmlSendArticle> xmlSendArticle)
        {
            if (xmlSendArticle.Count > 10)
                throw new WeiXinServiceException("图文消息不能超过10条。");
            var sendMsg = new XmlSendNewsMessage();
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.CreateTime = SignatureHelper.CreateTimestamp();
            sendMsg.Articles = xmlSendArticle;
            sendMsg.ArticleCount = sendMsg.Articles.Count;
            var xml = sendMsg.ToXml();
            Log.Debug("\r\nSend>>\r\n{0}", xml);
            return xml;
        }
        /// <summary>
        /// 将接收的 xml 消息转换为对应实体
        /// </summary>
        /// <param name="receiveXml"></param>
        /// <returns></returns>
        private static XmlReceiveMessage ConvertReceiveXmlToXmlReceiveMessage(string receiveXml)
        {
            var xmlObj = XmlHelper.Read(receiveXml);
            var msgType = xmlObj["MsgType"];
            XmlReceiveMessage obj = null;
            if (msgType.Equals("text"))
            {
                obj = new XmlReceiveTextMessage();
            }
            else if (msgType.Equals("image"))
            {
                obj = new XmlReceiveImageMessage();
            }
            else if (msgType.Equals("voice"))
            {
                obj = new XmlReceiveVoiceMessage();
            }
            else if (msgType.Equals("video"))
            {
                obj = new XmlReceiveVideoMessage();
            }
            else if (msgType.Equals("location"))
            {
                obj = new XmlReceiveLocationMessage();
            }
            else if (msgType.Equals("link"))
            {
                obj = new XmlReceiveLinkMessage();
            }
            else if (msgType.Equals("event"))
            {
                obj = new XmlReceiveEventMessage();
            }
            if (obj != null)
            {
                var objType = obj.GetType();
                var pros = objType.GetProperties();
                foreach (var p in pros)
                {
                    var messagePropertyNameAttributes = p.GetCustomAttributes(typeof(MessagePropertyNameAttribute), true);
                    if (messagePropertyNameAttributes != null && messagePropertyNameAttributes.Length > 0)
                    {
                        var messagePropertyNameAttribute = (MessagePropertyNameAttribute)messagePropertyNameAttributes[0];
                        if (xmlObj.ContainsKey(messagePropertyNameAttribute.PropertyName))
                        {
                            var value = xmlObj[messagePropertyNameAttribute.PropertyName];
                            p.SetValue(obj, value, null);
                        }
                    }
                }
                obj.Xml = receiveXml;
            }
            return obj;
        }
        /// <summary>
        /// 根据消息获取消息类型（这个消息类型将 event 细化了，详情可以参考 ReceiveMessageType 类型）
        /// </summary>
        /// <param name="xmlReceiveMessage"></param>
        /// <returns></returns>
        private static XmlReceiveMessageType GetXmlReceiveMessageType(XmlReceiveMessage xmlReceiveMessage)
        {
            if (xmlReceiveMessage.MsgType.ToLower().Equals("text"))
            {
                return XmlReceiveMessageType.Text;
            }
            else if (xmlReceiveMessage.MsgType.ToLower().Equals("image"))
            {
                return XmlReceiveMessageType.Image;
            }
            else if (xmlReceiveMessage.MsgType.ToLower().Equals("voice"))
            {
                return XmlReceiveMessageType.Voice;
            }
            else if (xmlReceiveMessage.MsgType.ToLower().Equals("video"))
            {
                return XmlReceiveMessageType.Video;
            }
            else if (xmlReceiveMessage.MsgType.ToLower().Equals("location"))
            {
                return XmlReceiveMessageType.Location;
            }
            else if (xmlReceiveMessage.MsgType.ToLower().Equals("link"))
            {
                return XmlReceiveMessageType.Link;
            }
            else if (xmlReceiveMessage.MsgType.ToLower().Equals("event"))
            {
                var eventMsg = xmlReceiveMessage as XmlReceiveEventMessage;
                if (!string.IsNullOrEmpty(eventMsg.Ticket))                           // 扫描二维码事件
                {
                    if (eventMsg.Event.ToLower().Equals("subscribe"))                 // 用户未关注时扫描的事件
                    {
                        return XmlReceiveMessageType.Event_QRCode_Subscribe;
                    }
                    else if (eventMsg.Event.ToLower().Equals("scan"))                 // 用户已关注时扫描的事件
                    {
                        return XmlReceiveMessageType.Event_QRCode_Scan;
                    }
                    else
                    {
                        return XmlReceiveMessageType.Undefined;
                    }
                }
                else if (eventMsg.Event.ToLower().Equals("view"))                     // 点击菜单跳转链接时的事件
                {
                    return XmlReceiveMessageType.Event_View;
                }
                else if (eventMsg.Event.ToLower().Equals("click"))                    // 点击菜单拉取消息时的事件
                {
                    return XmlReceiveMessageType.Event_Click;
                }
                else if (eventMsg.Event.ToLower().Equals("location"))                 // 上报地理位置时的事件
                {
                    return XmlReceiveMessageType.Event_Location;
                }
                else if (eventMsg.Event.ToLower().Equals("subscribe"))                // 进行关注的事件
                {
                    return XmlReceiveMessageType.Event_Subscribe;
                }
                else if (eventMsg.Event.ToLower().Equals("unsubscribe"))              // 取消关注的事件
                {
                    return XmlReceiveMessageType.Event_UnSubscribe;
                }
                else
                {
                    return XmlReceiveMessageType.Undefined;
                }
            }
            else
            {
                return XmlReceiveMessageType.Undefined;
            }
        }
    }

    /******************************************************************************/

    /// <summary>
    /// 微信核心服务异常
    /// </summary>
    public class WeiXinServiceException : Exception
    {
        string msg;
        public WeiXinServiceException() { }
        public WeiXinServiceException(string message)
        {
            msg = message;
        }
        public override string Message
        {
            get
            {
                return msg;
            }
        }
    }
}
