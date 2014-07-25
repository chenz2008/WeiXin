using System.Collections.Generic;

namespace WeiXin.Core.Messages
{
    /// <summary>
    /// Xml 消息，接收微信服务器的消息为 xml 格式，被动消息的格式也是 xml。
    /// </summary>
    public abstract class XmlMessage
    {
        /// <summary>
        /// xml 消息完整内容
        /// </summary>
        public string Xml { get; set; }
        /// <summary>
        /// 接收方微信号/OpenID
        /// </summary>
        [PassiveMessageProperty(true)]
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方微信号/OpenID
        /// </summary>
        [PassiveMessageProperty(true)]
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间（整形）
        /// </summary>
        [PassiveMessageProperty(true)]
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [PassiveMessageProperty(true)]
        public string MsgType { get; set; }
        /// <summary>
        /// 消息id，64位整形（用于排除重复消息）
        /// </summary>
        public string MsgId { get; set; }
    }

    /// <summary>
    /// 接收-事件消息
    /// </summary>
    public class XmlEventMessage : XmlMessage
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }
    }
    /// <summary>
    /// 接收-图片消息
    /// </summary>
    public class XmlImageMessage : XmlMessage
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        [PassiveMessageProperty(true)]
        public string MediaId { get; set; }
    }
    /// <summary>
    /// 接收-链接消息
    /// </summary>
    public class XmlLinkMessage : XmlMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
    /// <summary>
    /// 接收-地理位置消息
    /// </summary>
    public class XmlLocationMessage : XmlMessage
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }
    /// <summary>
    /// 接收-文本消息
    /// </summary>
    public class XmlTextMessage : XmlMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [PassiveMessageProperty(true)]
        public string Content { get; set; }
    }
    /// <summary>
    /// 接收-视频消息
    /// </summary>
    public class XmlVideoMessage : XmlMessage
    {
        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        [PassiveMessageProperty(true)]
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        [PassiveMessageProperty(true)]
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        [PassiveMessageProperty(true)]
        public string Description { get; set; }
    }
    /// <summary>
    /// 接收-语音消息
    /// </summary>
    public class XmlVoiceMessage : XmlMessage
    {
        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        [PassiveMessageProperty(true)]
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果
        /// </summary>
        public string Recognition { get; set; }
    }
    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsMessage : XmlMessage
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        [PassiveMessageProperty(true)]
        public int ArticleCount { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [PassiveMessageProperty(true)]
        [HaveChild(true)]
        public List<Article> Articles { get; set; }
    }
    /// <summary>
    /// 图文消息单条内容
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        [PassiveMessageProperty(true)]
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        [PassiveMessageProperty(true)]
        public string Description { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        [PassiveMessageProperty(true)]
        public string Url { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        [PassiveMessageProperty(true)]
        public string PicUrl { get; set; }
    }
}
