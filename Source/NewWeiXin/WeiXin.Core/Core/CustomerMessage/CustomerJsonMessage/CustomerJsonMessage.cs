
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 客服消息
    /// </summary>
    public abstract class CustomerServiceMessage
    {
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string Touser { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public abstract string MsgType { get; }

        protected string Json(string content)
        {
            return "{\"touser\":\"" + Touser + "\",\"msgtype\":\"" + MsgType + "\",\"" + MsgType + "\":{" + content + "}}";
        }

        public abstract string GetJson();
    }
}
