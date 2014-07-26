using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using WeiXin.Core;
using WeiXin.Core.Messages;

namespace Samples
{
    /*
     * 使用步骤：
     * 1、首先实现 IWeiXinService 接口，Process 方法为处理微信服务器发来的消息作出的回应。
     * 2、注册实现的接口 WeiXinService.Register(_Service);
     * 3、在接口入口处调用 WeiXinService.ProcessMessage(xml);
     * 在 Process 方法中专注的开发你的功能吧:)
     */
    // 步骤1
    public class ProcessMessage : IWeiXinService
    {
        /// <summary>
        /// 处理消息的接口，如果回复被动消息，直接回复 xml 格式的消息即可。
        /// 如果不处理或者发送客服消息等直接回复空字符串即可。
        /// </summary>
        /// <param name="receiveMsgType"></param>
        /// <param name="receiveMsg"></param>
        /// <returns></returns>
        public string Process(ReceiveXmlMessageType receiveMsgType, ReceiveXmlMessage receiveMsg)
        {
            string result = string.Empty;
            if (receiveMsg != null)
            {
                // 消息类型
                switch (receiveMsgType)
                {
                    case ReceiveXmlMessageType.Undefined:                          // 未识别出消息类型
                        break;
                    case ReceiveXmlMessageType.Text:                               // 文本消息
                        break;
                    case ReceiveXmlMessageType.Image:                              // 图片消息
                        break;
                    case ReceiveXmlMessageType.Voice:                              // 语音消息
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
                        result = EventClickAction(receiveMsg);
                        break;
                    case ReceiveXmlMessageType.Event_Location:                     // 上报地理位置时事件
                        result = EventLocationAction(receiveMsg);
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

        string EventLocationAction(ReceiveXmlMessage receiveMsg)
        {
            var result = string.Empty;
            var eventMsg = receiveMsg as ReceiveXmlEventMessage;
            var article = new List<SendXmlArticle>();
            article.Add(new SendXmlArticle { Title = "你的地理位置信息", Description = string.Format("纬度：{0}\r\n\r\n经度：{1}\r\n\r\n精度：{2}", eventMsg.Latitude, eventMsg.Longitude, eventMsg.Precision), Url = "http://www.wangwenzhuang.com/" });
            result = WeiXinService.CreateToXmlNewsMessageXml(receiveMsg, article);
            return result;
        }

        string EventClickAction(ReceiveXmlMessage receiveMsg)
        {
            var result = string.Empty;
            var eventMsg = receiveMsg as ReceiveXmlEventMessage;
            if (eventMsg.EventKey.Equals("1"))
            {
                result = WeiXinService.CreateToXmlTextMessageXml(receiveMsg, "被动文本消息");
            }
            else if (eventMsg.EventKey == "2")
            {
                var article = new List<SendXmlArticle>();
                article.Add(new SendXmlArticle { Title = "被动单图文消息", Description = "被动单图文消息，此处省略一万字。。。", PicUrl = "http://h.hiphotos.baidu.com/image/pic/item/c9fcc3cec3fdfc037d970d53d63f8794a5c2266a.jpg", Url = "http://www.wangwenzhuang.com/" });
                result = WeiXinService.CreateToXmlNewsMessageXml(receiveMsg, article);
            }
            else if (eventMsg.EventKey == "3")
            {
                var article = new List<SendXmlArticle>();
                article.Add(new SendXmlArticle { Title = "被动多图文消息1", Description = "被动多图文消息，此处省略一万字。。。", PicUrl = "http://h.hiphotos.baidu.com/image/pic/item/c9fcc3cec3fdfc037d970d53d63f8794a5c2266a.jpg", Url = "http://www.wangwenzhuang.com/" });
                article.Add(new SendXmlArticle { Title = "被动多图文消息2", Description = "被动多图文消息，此处省略一万字。。。", PicUrl = "http://g.hiphotos.baidu.com/image/pic/item/55e736d12f2eb93895023c7fd7628535e4dd6fcb.jpg", Url = "http://www.wangwenzhuang.com/" });
                article.Add(new SendXmlArticle { Title = "被动多图文消息3", Description = "被动多图文消息，此处省略一万字。。。", PicUrl = "http://e.hiphotos.baidu.com/image/pic/item/63d0f703918fa0ec8426f0f7249759ee3c6ddb63.jpg", Url = "http://www.wangwenzhuang.com/" });
                result = WeiXinService.CreateToXmlNewsMessageXml(receiveMsg, article);
            }
            return result;
        }
    }

    public class Logger : ILogger
    {
        readonly string debugFileName = @"debug.txt";
        readonly string infoFileName = @"info.txt";
        readonly string warningFileName = @"warning.txt";
        readonly string errorFileName = @"error.txt";

        public Logger()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var logPath = Path.Combine(baseDir, "Log");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            debugFileName = Path.Combine(logPath, debugFileName);
            infoFileName = Path.Combine(logPath, infoFileName);
            warningFileName = Path.Combine(logPath, warningFileName);
            errorFileName = Path.Combine(logPath, errorFileName);
        }

        public void Debug(string format, params object[] objs)
        {
            LogWrite("Debug", debugFileName, format, objs);
        }

        public void Info(string format, params object[] objs)
        {
            LogWrite("Info", infoFileName, format, objs);
        }

        public void Warning(string format, params object[] objs)
        {
            LogWrite("Warning", warningFileName, format, objs);
        }

        public void Error(string format, params object[] objs)
        {
            LogWrite("Error", errorFileName, format, objs);
        }

        private void LogWrite(string type, string fileName, string format, params object[] objs)
        {
            string msg = string.Format(format, objs);
            File.AppendAllText(fileName, string.Format("时间：{0}\r\n类型：{1}\r\n消息：{2}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), type, msg));
        }
    }

    /// <summary>
    /// 微信公共平台接口
    /// </summary>
    public class WeiXin : IHttpHandler
    {
        static IWeiXinService _Service;
        static WeiXin()
        {
            _Service = new ProcessMessage();
            // 步骤2
            WeiXinService.Register(_Service, WeiXinConfig.AppId, WeiXinConfig.AppSecret);

            Log.Logger = new Logger();
            // 设置日记级别
            Log.Level = LogLevel.Debug;
        }
        public void ProcessRequest(HttpContext context)
        {
            string echoStr = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
            // 验证签名
            if (WeiXinService.CheckSignature(WeiXinConfig.Token, signature, timestamp, nonce))
            {
                var writeMsg = string.Empty;
                if ("post".Equals(context.Request.HttpMethod.ToLower()))
                {
                    string xml = null;
                    using (var reader = new StreamReader(context.Request.InputStream))
                    {
                        xml = HttpUtility.UrlDecode(reader.ReadToEnd());
                    }
                    if (!string.IsNullOrEmpty(xml))
                    {
                        // 步骤3
                        writeMsg = WeiXinService.ProcessMessage(xml);
                    }
                }
                else
                {
                    // get 请求是微信服务器验证，原样返回 echoStr 才能通过验证
                    writeMsg = echoStr;
                }
                HttpContext.Current.Response.Write(writeMsg);
                HttpContext.Current.Response.End();
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }

    public sealed class WeiXinConfig
    {
        public readonly static string AppId;
        public readonly static string AppSecret;
        public readonly static string Token;
        static WeiXinConfig()
        {
            AppId = ConfigurationManager.AppSettings["appid"];
            AppSecret = ConfigurationManager.AppSettings["appsecret"];
            Token = ConfigurationManager.AppSettings["token"];
        }
    }
}