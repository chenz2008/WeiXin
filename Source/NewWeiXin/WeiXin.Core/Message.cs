using System.Collections.Generic;

namespace WeiXin.Core.Messages
{
    #region XML 格式消息

    #region 接收的 xml 格式消息
    /// <summary>
    /// Xml 消息，接收微信服务器的消息为 xml 格式，被动消息的格式也是 xml。
    /// </summary>
    public abstract class XmlReceiveMessage
    {
        /// <summary>
        /// xml 消息完整内容
        /// </summary>
        public string Xml { get; set; }
        /// <summary>
        /// 接收方微信号/OpenID
        /// </summary>
        [MessagePropertyName("ToUserName")]
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方微信号/OpenID
        /// </summary>
        [MessagePropertyName("FromUserName")]
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间（整形）
        /// </summary>
        [MessagePropertyName("CreateTime")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [MessagePropertyName("MsgType")]
        public string MsgType { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        [MessagePropertyName("MsgId")]
        public string MsgId { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 事件消息
    /// </summary>
    public class XmlReceiveEventMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        [MessagePropertyName("Event")]
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值
        /// </summary>
        [MessagePropertyName("EventKey")]
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        [MessagePropertyName("Ticket")]
        public string Ticket { get; set; }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        [MessagePropertyName("Latitude")]
        public string Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        [MessagePropertyName("Longitude")]
        public string Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        [MessagePropertyName("Precision")]
        public string Precision { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 图片消息
    /// </summary>
    public class XmlReceiveImageMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        [MessagePropertyName("PicUrl")]
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        [MessagePropertyName("MediaId")]
        public string MediaId { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 链接消息
    /// </summary>
    public class XmlReceiveLinkMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        [MessagePropertyName("Title")]
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        [MessagePropertyName("Description")]
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        [MessagePropertyName("Url")]
        public string Url { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class XmlReceiveLocationMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        [MessagePropertyName("Location_X")]
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        [MessagePropertyName("Location_Y")]
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        [MessagePropertyName("Scale")]
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        [MessagePropertyName("Label")]
        public string Label { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 文本消息
    /// </summary>
    public class XmlReceiveTextMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [MessagePropertyName("Content")]
        public string Content { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 视频消息
    /// </summary>
    public class XmlReceiveVideoMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        [MessagePropertyName("MediaId")]
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        [MessagePropertyName("ThumbMediaId")]
        public string ThumbMediaId { get; set; }
    }

    /******************************************************************************/

    /// <summary>
    /// 语音消息
    /// </summary>
    public class XmlReceiveVoiceMessage : XmlReceiveMessage
    {
        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        [MessagePropertyName("MediaId")]
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        [MessagePropertyName("Format")]
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果
        /// </summary>
        [MessagePropertyName("Recognition")]
        public string Recognition { get; set; }
    }
    #endregion

    /******************************************************************************/

    #region 发送的 xml 格式消息
    /// <summary>
    /// Xml 消息，接收微信服务器的消息为 xml 格式，被动消息的格式也是 xml。
    /// </summary>
    public abstract class XmlSendMessage
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

    /******************************************************************************/

    /// <summary>
    /// 图片消息
    /// </summary>
    public class XmlSendImageMessage : XmlSendMessage
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

    /******************************************************************************/

    /// <summary>
    /// 文本消息
    /// </summary>
    public class XmlSendTextMessage : XmlSendMessage
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

    /******************************************************************************/

    /// <summary>
    /// 视频消息
    /// </summary>
    public class XmlSendVideoMessage : XmlSendMessage
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

    /******************************************************************************/

    /// <summary>
    /// 音乐消息
    /// </summary>
    public class XmlSendMusicMessage : XmlSendMessage
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

    /******************************************************************************/

    /// <summary>
    /// 语音消息
    /// </summary>
    public class XmlSendVoiceMessage : XmlSendMessage
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
    /// <summary>
    /// 图文消息
    /// </summary>
    public class XmlSendNewsMessage : XmlSendMessage
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public List<XmlSendArticle> Articles { get; set; }

        public override string MsgType
        {
            get { return "news"; }
        }

        public override string ToXml()
        {
            var newsFormat = "<ArticleCount>{0}</ArticleCount><Articles>{1}</Articles>";
            var articles = string.Empty;
            foreach (var article in Articles)
            {
                articles += article.ToXml();
            }
            var news = string.Format(newsFormat, this.ArticleCount, articles);
            return base.ToXml(news);
        }
    }

    /******************************************************************************/

    /// <summary>
    /// 图文消息单条内容
    /// </summary>
    public class XmlSendArticle
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
    #endregion

    #endregion
}
