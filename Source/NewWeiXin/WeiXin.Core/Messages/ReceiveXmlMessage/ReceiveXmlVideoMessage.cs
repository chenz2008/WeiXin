
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class ReceiveXmlVideoMessage : ReceiveXmlMessage
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
}
