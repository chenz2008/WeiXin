
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 图文消息单条内容
    /// </summary>
    public class SendXmlArticle
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl { get; set; }
        public string ToXml()
        {
            return string.Format(
"<item>" +
"<Title><![CDATA[{0}]]></Title>" +
"<Description><![CDATA[{1}]]></Description>" +
"<PicUrl><![CDATA[{2}]]></PicUrl>" +
"<Url><![CDATA[{3}]]></Url>" +
"</item>", this.Title ?? string.Empty, this.Description ?? string.Empty, this.PicUrl ?? string.Empty, this.Url ?? string.Empty);
        }
    }
}
