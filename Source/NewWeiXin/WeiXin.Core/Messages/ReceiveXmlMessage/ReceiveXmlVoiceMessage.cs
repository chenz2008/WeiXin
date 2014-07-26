
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class ReceiveXmlVoiceMessage : ReceiveXmlMessage
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
}
