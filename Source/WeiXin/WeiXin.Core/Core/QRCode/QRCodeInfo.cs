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
        [JsonProperty("expire_seconds")]
        public int ExpireSeconds { get; set; }
    }
}
