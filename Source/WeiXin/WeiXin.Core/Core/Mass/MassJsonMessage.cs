using System.Collections.Generic;

namespace WeiXin.Core
{
    public abstract class MassJsonMessage
    {
        /// <summary>
        /// 用户 OpenId 集合
        /// </summary>
        public List<string> Touser { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public abstract string MsgType { get; }

        protected string Json(string content)
        {
            return "{\"touser\":[" + GetTouser() + "],\"msgtype\":\"" + MsgType + "\",\"" + MsgType + "\":{" + (content ?? string.Empty) + "}}";
        }

        private string GetTouser()
        {
            var result = string.Empty;
            if (Touser != null && Touser.Count > 0)
            {
                foreach (var item in Touser)
                {
                    result += string.Format("\"{0}\",", item);
                }
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }
            return result;
        }

        public abstract string GetJson();
    }
}
