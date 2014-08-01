using System.Web;

namespace WeiXin.Core
{
    public class MassTextMessage : MassJsonMessage
    {
        public string Content { get; set; }
        public override string MsgType
        {
            get
            {
                return "text";
            }
        }
        public override string GetJson()
        {
            string content = string.Format("\"content\":\"{0}\"", (this.Content ?? string.Empty));
            return base.Json(content);
        }
    }
}
