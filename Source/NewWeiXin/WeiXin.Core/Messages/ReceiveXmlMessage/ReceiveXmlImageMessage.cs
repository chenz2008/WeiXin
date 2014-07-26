using System.Collections.Generic;

namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ReceiveXmlImageMessage : ReceiveXmlMessage
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
}
