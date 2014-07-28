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
                result += "\"title\":\"" + HttpUtility.UrlDecode(Title) + "\",";
            }
            if (!string.IsNullOrEmpty(Description))
            {
                result += "\"description\":\"" + HttpUtility.UrlDecode(Description) + "\",";
            }
            if (!string.IsNullOrEmpty(Url))
            {
                result += "\"url\":\"" + HttpUtility.UrlDecode(Url) + "\",";
            }
            if (!string.IsNullOrEmpty(PicUrl))
            {
                result += "\"picurl\":\"" + HttpUtility.UrlDecode(PicUrl) + "\",";
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
