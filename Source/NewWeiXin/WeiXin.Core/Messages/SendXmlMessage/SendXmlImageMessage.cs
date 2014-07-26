
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class SendXmlImageMessage : SendXmlMessage
    {
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        public override string MsgType
        {
            get
            {
                return "image";
            }
        }

        public override string ToXml()
        {
            string content = string.Format(
"<Image>" +
"<MediaId><![CDATA[{0}]]></MediaId>" +
"</Image>", MediaId ?? string.Empty);
            return base.ToXml(content);
        }
    }
}
