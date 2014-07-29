using System.Collections.Generic;

namespace WeiXin.Core.Messages
{
    public class CustomerJsonNewsMessage : CustomerJsonMessage
    {
        public List<CustomerJsonArticleMessage> Articles { get; set; }
        public override string MsgType
        {
            get
            {
                return "news";
            }
        }

        public override string GetJson()
        {
            var newsFormat = "\"articles\":[{0}]";
            var articles = string.Empty;
            foreach (var article in Articles)
            {
                articles += "{" + article.GetJson() + "},";
            }
            if (articles.Length > 0)
            {
                articles = articles.Substring(0, articles.Length - 1);
            }
            var news = string.Format(newsFormat, articles);
            return base.Json(news);
        }
    }
}
