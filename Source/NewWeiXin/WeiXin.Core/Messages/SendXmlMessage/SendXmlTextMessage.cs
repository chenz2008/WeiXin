
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class SendXmlTextMessage : SendXmlMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        public override string MsgType
        {
            get { return "text"; }
        }

        public override string ToXml()
        {
            string content = string.Format(
"<Content>" +
"<![CDATA[{0}]]>" +
"</Content>", Content ?? string.Empty);
            return base.ToXml(content);
        }
    }
}
