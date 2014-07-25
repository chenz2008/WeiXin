using WeiXin.Core.Messages;

namespace WeiXin.Core
{
    public interface ILogger
    {
        void Debug(string format, params object[] objs);
        void Info(string format, params object[] objs);
        void Warning(string format, params object[] objs);
        void Error(string format, params object[] objs);
    }

    /******************************************************************************/

    public interface IWeiXinService
    {
        string Process(XmlReceiveMessageType receiveMsgType, XmlReceiveMessage receiveMsg);
    }
}
