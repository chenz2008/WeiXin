using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXin.Core;

namespace Samples
{
    public partial class OAuth2_snsapi_base : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var code = Request.QueryString["code"];
            var state = Request.QueryString["state"];
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(code))
            {
                var token = WeiXinService.GetOAuthAccessToken(code);
                this.openId.Text = token.OpenId;
            }
        }
    }
}