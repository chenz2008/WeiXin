
namespace WeiXin.Core
{
    /// <summary>
    /// 接收 xml 格式消息类型
    /// </summary>
    public enum XmlReceiveMessageType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// 文本消息
        /// </summary>
        Text = 1,
        /// <summary>
        /// 图片消息
        /// </summary>
        Image = 2,
        /// <summary>
        /// 语音消息
        /// </summary>
        Voice = 3,
        /// <summary>
        /// 视频消息
        /// </summary>
        Video = 4,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        Location = 5,
        /// <summary>
        /// 链接消息
        /// </summary>
        Link = 6,
        /// <summary>
        /// 用户未关注时扫描二维码事件
        /// </summary>
        Event_QRCode_Subscribe = 7,
        /// <summary>
        /// 用户已关注时扫描二维码事件
        /// </summary>
        Event_QRCode_Scan = 8,
        /// <summary>
        /// 点击菜单跳转链接时事件
        /// </summary>
        Event_View = 9,
        /// <summary>
        /// 点击菜单拉取消息时事件
        /// </summary>
        Event_Click = 10,
        /// <summary>
        /// 上报地理位置时事件
        /// </summary>
        Event_Location = 11,
        /// <summary>
        /// 关注事件
        /// </summary>
        Event_Subscribe = 12,
        /// <summary>
        /// 取消关注事件
        /// </summary>
        Event_UnSubscribe = 13
    }
}
