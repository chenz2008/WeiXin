
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class SendXmlVoiceMessage : SendXmlMessage
    {
        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        public override string MsgType
        {
            get { return "voice"; }
        }

        public override string ToXml()
        {
            string content = string.Format(
"<Voice>" +
"<MediaId><![CDATA[{0}]]></MediaId>" +
"</Voice>", this.MediaId ?? string.Empty);
            return base.ToXml(content);
        }
    }
}
