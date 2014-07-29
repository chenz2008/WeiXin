using System.Configuration;

namespace Samples
{
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