using System.Web;

namespace WeiXin.Core.Messages
{
    public class CustomerJsonArticleMessage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string PicUrl { get; set; }

        public string GetJson()
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(Title))
            {
                result += "\"title\":\"" + (Title ?? string.Empty) + "\",";
            }
            if (!string.IsNullOrEmpty(Description))
            {
                result += "\"description\":\"" + (Description ?? string.Empty) + "\",";
            }
            if (!string.IsNullOrEmpty(Url))
            {
                result += "\"url\":\"" + (Url ?? string.Empty) + "\",";
            }
            if (!string.IsNullOrEmpty(PicUrl))
            {
                result += "\"picurl\":\"" + (PicUrl ?? string.Empty) + "\",";
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
