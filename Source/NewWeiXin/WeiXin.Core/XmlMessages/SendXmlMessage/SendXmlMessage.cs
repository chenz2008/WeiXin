
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// Xml 消息，接收微信服务器的消息为 xml 格式，被动消息的格式也是 xml。
    /// </summary>
    public abstract class SendXmlMessage
    {
        /// <summary>
        /// 接收方微信号/OpenID
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方微信号/OpenID
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间（整形）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public abstract string MsgType { get; }
        protected string ToXml(string content)
        {
            return string.Format(
"<xml>" +
"<ToUserName><![CDATA[{0}]]></ToUserName>" +
"<FromUserName><![CDATA[{1}]]></FromUserName>" +
"<CreateTime>{2}</CreateTime>" +
"<MsgType><![CDATA[{3}]]></MsgType>" +
"{4}" +
"</xml>", this.ToUserName ?? string.Empty, this.FromUserName ?? string.Empty, this.CreateTime, this.MsgType, content ?? string.Empty);
        }
        public abstract string ToXml();
    }
}
