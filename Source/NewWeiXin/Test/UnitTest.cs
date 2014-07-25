using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Core;

namespace Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            WeiXinService.ProcessMessage("<xml><ToUserName><![CDATA[gh_73b7d749b6fa]]></ToUserName><FromUserName><![CDATA[oxhAYuPP7QcvPBq33dXs9f8Kvo2Y]]></FromUserName><CreateTime>1406283992</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[CLICK]]></Event><EventKey><![CDATA[1]]></EventKey></xml>");
        }
    }
}
