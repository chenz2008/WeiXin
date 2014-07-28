
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public class ReceiveXmlEventMessage : ReceiveXmlMessage
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
}
