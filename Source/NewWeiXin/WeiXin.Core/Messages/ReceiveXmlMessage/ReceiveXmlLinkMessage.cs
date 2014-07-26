
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 链接消息
    /// </summary>
    public class ReceiveXmlLinkMessage : ReceiveXmlMessage
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
}
