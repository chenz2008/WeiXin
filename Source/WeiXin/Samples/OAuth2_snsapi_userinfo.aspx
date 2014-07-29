<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OAuth2_snsapi_userinfo.aspx.cs" Inherits="Samples.OAuth2_snsapi_userinfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>OAuth2.0 授权</title>
    <link href="Content/bootstrap-3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <p>
                OpenId：<asp:Label ID="openId" runat="server" Text=""></asp:Label>
            </p>
            <p>
                nickname:<asp:Label ID="nickname" runat="server" Text=""></asp:Label>
            </p>
            <p>
                sex:<asp:Label ID="sex" runat="server" Text=""></asp:Label>
            </p>
            <p>
                city:<asp:Label ID="city" runat="server" Text=""></asp:Label>
            </p>
            <p>
                province:<asp:Label ID="province" runat="server" Text=""></asp:Label>
            </p>
            <p>
                country:<asp:Label ID="country" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Image ID="headimgurl" runat="server" class="img-rounded" Style="width: 140px; height: 140px;" />
            </p>
            <hr />
            <footer>
                <p>&copy; 2014 - WangWenzhuang</p>
            </footer>
        </div>
        <script src="Scripts/jquery-1.10.2.min.js"></script>
        <script src="Content/bootstrap-3.0.3/js/bootstrap.min.js"></script>
        <script>
        </script>
    </form>
</body>
</html>
