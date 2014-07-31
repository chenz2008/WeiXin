using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXin.Core
{
    public class QRCodeInfo
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
        /// <summary>
        /// 过期时间（秒单位）
        /// </summary>
        [JsonProperty("expire_seconds")]
        public int ExpireSeconds { get; set; }
        /// <summary>
        /// 二维码图片 Url
        /// </summary>
        public string ImgUrl { get; set; }
    }
}
