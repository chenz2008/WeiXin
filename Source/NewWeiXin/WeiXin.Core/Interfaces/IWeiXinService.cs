
using WeiXin.Core.Messages;
namespace WeiXin.Core
{
    public interface IWeiXinService
    {
        string Process(ReceiveXmlMessageType receiveXmlMsgType, ReceiveXmlMessage receiveXmlMessage);
    }
}
