using System.IO;
using System.Web;
using WeiXin.Core;

namespace Samples
{
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
                        xml = reader.ReadToEnd();
                    }
                    if (!string.IsNullOrEmpty(xml))
                    {
                        // 步骤3
                        try
                        {
                            writeMsg = WeiXinService.ProcessMessage(xml);
                        }
                        catch (System.Exception e)
                        {
                            Log.Error("异常信息：{1}\r\n源：{2}\r\n堆栈：{3}\r\n引发异常的方法：{4}\r\n\r\n", e.Message, e.Source, e.StackTrace, e.TargetSite.Name);
                        }
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
}