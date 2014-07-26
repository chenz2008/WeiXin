
using System.Collections.Generic;
namespace WeiXin.Core.Messages
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class SendXmlNewsMessage : SendXmlMessage
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public List<SendXmlArticle> Articles { get; set; }

        public override string MsgType
        {
            get { return "news"; }
        }

        public override string ToXml()
        {
            var newsFormat = "<ArticleCount>{0}</ArticleCount><Articles>{1}</Articles>";
            var articles = string.Empty;
            foreach (var article in Articles)
            {
                articles += article.ToXml();
            }
            var news = string.Format(newsFormat, this.ArticleCount, articles);
            return base.ToXml(news);
        }
    }
}
