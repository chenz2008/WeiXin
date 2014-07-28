
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class SendXmlVideoMessage : SendXmlMessage
    {
        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }

        public override string MsgType
        {
            get { return "video"; }
        }

        public override string ToXml()
        {
            string content = string.Format(
"<Video>" +
"<MediaId><![CDATA[{0}]]></MediaId>" +
"<Title><![CDATA[{1}]]></Title>" +
"<Description><![CDATA[{2}]]></Description>" +
"</Video>", this.MediaId ?? string.Empty, this.Title ?? string.Empty, this.Description ?? string.Empty);
            return base.ToXml(content);
        }
    }
}
