
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 客服消息
    /// </summary>
    public abstract class CustomerJsonMessage
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
            return "{\"touser\":\"" + (Touser ?? string.Empty) + "\",\"msgtype\":\"" + MsgType + "\",\"" + MsgType + "\":{" + (content ?? string.Empty) + "}}";
        }

        public abstract string GetJson();
    }
}
