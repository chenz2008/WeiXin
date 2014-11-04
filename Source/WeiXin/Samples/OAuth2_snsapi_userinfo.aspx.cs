using System;
using WeiXin.Core;

namespace Samples
{
    public partial class OAuth2_snsapi_userinfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var code = Request.QueryString["code"];
            var state = Request.QueryString["state"];
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(code))
            {
                var token = WeiXinService.GetOAuthAccessToken(code, WeiXinConfig.AppId, WeiXinConfig.AppSecret);
                var userInfo = WeiXinService.GetOAuthUserInfo(token.OpenId, token.AccessToken);
                this.openId.Text = token.OpenId;
                this.nickname.Text = userInfo.NickName;
                this.sex.Text = userInfo.Sex.ToString();
                this.city.Text = userInfo.City;
                this.province.Text = userInfo.Province;
                this.country.Text = userInfo.Country;
                this.headimgurl.ImageUrl = userInfo.HeadImgUrl;
            }
        }
    }
}