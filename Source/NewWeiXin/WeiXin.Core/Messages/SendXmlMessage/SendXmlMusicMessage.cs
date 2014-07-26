
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 音乐消息
    /// </summary>
    public class SendXmlMusicMessage : SendXmlMessage
    {
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicURL { get; set; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }

        public override string MsgType
        {
            get { return "music"; }
        }

        public override string ToXml()
        {
            string content = string.Format(
 "<Music>" +
"<Title><![CDATA[{0}]]></Title>" +
"<Description><![CDATA[{1}]]></Description>" +
"<MusicUrl><![CDATA[{2}]]></MusicUrl>" +
"<HQMusicUrl><![CDATA[{3}]]></HQMusicUrl>" +
"<ThumbMediaId><![CDATA[{4}]]></ThumbMediaId>" +
"</Music>", this.Title ?? string.Empty, this.Description ?? string.Empty, this.MusicURL ?? string.Empty, this.HQMusicUrl ?? string.Empty, this.ThumbMediaId ?? string.Empty);
            return base.ToXml(content);
        }
    }
}
