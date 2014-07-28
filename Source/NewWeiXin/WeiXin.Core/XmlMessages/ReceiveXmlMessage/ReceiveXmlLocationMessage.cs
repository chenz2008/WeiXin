
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class ReceiveXmlLocationMessage : ReceiveXmlMessage
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
}
