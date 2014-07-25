using System;
using System.Linq;
using WeiXin.Core.Messages;

namespace WeiXin.Core
{
    public sealed class WeiXinService
    {
        static IWeiXinService _Service;
        public static void Register(IWeiXinService service)
        {
            if (service == null)
                throw new WeiXinServiceException("service 不能为 null。");
            _Service = service;
        }

        public static string ProcessMessage(string xml)
        {
            var result = string.Empty;
            if (_Service != null)
            {
                var msg = ConvertXmlToMessage(xml);
                if (msg == null)
                {
                    Log.Error("转换消息失败，xml：\r\n{0}", xml);
                }
                else
                {
                    var msgType = GetMessageType(msg);
                    result = _Service.Process(msgType, msg);
                }
            }
            return result;
        }

        public static XmlMessage ConvertXmlToMessage(string xml)
        {
            var xmlObj = XmlHelper.Read(xml);
            var msgType = xmlObj["MsgType"];
            XmlMessage obj = null;
            if (msgType.Equals("text"))
            {
                obj = new XmlTextMessage();
            }
            else if (msgType.Equals("image"))
            {
                obj = new XmlImageMessage();
            }
            else if (msgType.Equals("voice"))
            {
                obj = new XmlVoiceMessage();
            }
            else if (msgType.Equals("video"))
            {
                obj = new XmlVideoMessage();
            }
            else if (msgType.Equals("location"))
            {
                obj = new XmlLocationMessage();
            }
            else if (msgType.Equals("link"))
            {
                obj = new XmlLinkMessage();
            }
            else if (msgType.Equals("event"))
            {
                obj = new XmlEventMessage();
            }
            if (obj != null)
            {
                var objType = obj.GetType();
                var pros = objType.GetProperties();
                foreach (var key in xmlObj.Keys)
                {
                    var tmps = pros.Where(p => p.Name.Equals(key)).ToList();
                    if (tmps.Count > 0)
                    {
                        var pInfo = tmps[0];
                        var value = xmlObj[key];
                        pInfo.SetValue(obj, value, null);
                    }
                }
                obj.Xml = xml;
            }
            return obj;
        }

        public static XmlMessageType GetMessageType(XmlMessage msg)
        {
            if (msg.MsgType.ToLower().Equals("text"))
            {
                return XmlMessageType.Text;
            }
            else if (msg.MsgType.ToLower().Equals("image"))
            {
                return XmlMessageType.Image;
            }
            else if (msg.MsgType.ToLower().Equals("voice"))
            {
                return XmlMessageType.Voice;
            }
            else if (msg.MsgType.ToLower().Equals("video"))
            {
                return XmlMessageType.Video;
            }
            else if (msg.MsgType.ToLower().Equals("location"))
            {
                return XmlMessageType.Location;
            }
            else if (msg.MsgType.ToLower().Equals("link"))
            {
                return XmlMessageType.Link;
            }
            else if (msg.MsgType.ToLower().Equals("event"))
            {
                var eventMsg = msg as XmlEventMessage;
                if (!string.IsNullOrEmpty(eventMsg.Ticket))                           // 扫描二维码事件
                {
                    if (eventMsg.Event.ToLower().Equals("subscribe"))                 // 用户未关注时扫描的事件
                    {
                        return XmlMessageType.Event_QRCode_Subscribe;
                    }
                    else if (eventMsg.Event.ToLower().Equals("scan"))                 // 用户已关注时扫描的事件
                    {
                        return XmlMessageType.Event_QRCode_Scan;
                    }
                    else
                    {
                        return XmlMessageType.Undefined;
                    }
                }
                else if (eventMsg.Event.ToLower().Equals("view"))                     // 点击菜单跳转链接时的事件
                {
                    return XmlMessageType.Event_View;
                }
                else if (eventMsg.Event.ToLower().Equals("click"))                    // 点击菜单拉取消息时的事件
                {
                    return XmlMessageType.Event_Click;
                }
                else if (eventMsg.Event.ToLower().Equals("location"))                 // 上报地理位置时的事件
                {
                    return XmlMessageType.Event_Location;
                }
                else if (eventMsg.Event.ToLower().Equals("subscribe"))                // 进行关注的事件
                {
                    return XmlMessageType.Event_Subscribe;
                }
                else if (eventMsg.Event.ToLower().Equals("unsubscribe"))              // 取消关注的事件
                {
                    return XmlMessageType.Event_UnSubscribe;
                }
                else
                {
                    return XmlMessageType.Undefined;
                }
            }
            else
            {
                return XmlMessageType.Undefined;
            }
        }
    }

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
