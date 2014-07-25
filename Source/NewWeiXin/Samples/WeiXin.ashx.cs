using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
        public string Process(XmlMessageType rmt, XmlMessage msg)
        {
            string result = string.Empty;
            if (msg != null)
            {
                // 消息类型
                switch (rmt)
                {
                    case XmlMessageType.Undefined:                          // 未识别出消息类型
                        break;
                    case XmlMessageType.Text:                               // 文本消息
                        break;
                    case XmlMessageType.Image:                              // 图片消息
                        break;
                    case XmlMessageType.Voice:                              // 语音消息
                        break;
                    case XmlMessageType.Video:                              // 视频消息
                        break;
                    case XmlMessageType.Location:                           // 地理位置消息
                        break;
                    case XmlMessageType.Link:                               // 链接消息
                        break;
                    case XmlMessageType.Event_QRCode_Subscribe:             // 用户未关注时扫描二维码事件
                        break;
                    case XmlMessageType.Event_QRCode_Scan:                  // 用户已关注时扫描二维码事件
                        break;
                    case XmlMessageType.Event_View:                         // 点击菜单跳转链接时事件
                        break;
                    case XmlMessageType.Event_Click:                        // 点击菜单拉取消息时事件
                        break;
                    case XmlMessageType.Event_Location:                     // 上报地理位置时事件
                        //result = MessageManager.CreateTextMessageXml(receiveMsg.Xml, string.Format("您的地理位置纬度：{0}，经度：{1}，精度：{2}", eventMsg.Latitude, eventMsg.Longitude, eventMsg.Precision));
                        break;
                    case XmlMessageType.Event_Subscribe:                    // 关注事件
                        break;
                    case XmlMessageType.Event_UnSubscribe:                  // 取消关注事件
                        break;
                    default:
                        break;
                }
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
            WeiXinService.Register(_Service);

            Log.Logger = new Logger();
            // 设置日记级别
            Log.Level = LogLevel.Info;
        }
        public void ProcessRequest(HttpContext context)
        {
            string echoStr = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
            if (CheckSignature(WeiXinConfig.Token, signature, timestamp, nonce))
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
                HttpContext.Current.Response.Write(writeMsg);
                HttpContext.Current.Response.End();
            }
        }

        public bool CheckSignature(
            string token,
            string signature,
            string timestamp,
            string nonce)
        {
            string[] array = { token, timestamp, nonce };
            Array.Sort(array);
            string tmpStr = string.Empty;
            foreach (var tmp in array)
            {
                tmpStr += tmp;
            }
            tmpStr = BitConverter.ToString(
                new SHA1CryptoServiceProvider()
                .ComputeHash(new ASCIIEncoding().GetBytes(tmpStr)))
                .Replace("-", string.Empty)
                .ToLower();
            return tmpStr.Equals(signature);
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