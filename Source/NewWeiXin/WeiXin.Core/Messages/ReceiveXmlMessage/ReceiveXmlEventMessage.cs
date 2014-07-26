using System.Collections.Generic;

namespace WeiXin.Core.Messages
{
    /// <summary>
    /// Xml 消息，接收微信服务器的消息为 xml 格式，被动消息的格式也是 xml。
    /// </summary>
    public abstract class ReceiveXmlMessage
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
}
