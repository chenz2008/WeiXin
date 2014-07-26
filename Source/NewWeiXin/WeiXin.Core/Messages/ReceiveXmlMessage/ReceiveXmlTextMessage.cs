
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class ReceiveXmlTextMessage : ReceiveXmlMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [MessagePropertyName("Content")]
        public string Content { get; set; }
    }
}
