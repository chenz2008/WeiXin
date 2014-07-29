using System;

namespace WeiXin.Core
{
    public class WeiXinServiceException : Exception
    {
        private string _Message;
        public override string Message
        {
            get
            {
                return this._Message;
            }
        }
        public WeiXinServiceException()
        {
        }
        public WeiXinServiceException(string message)
        {
            this._Message = message;
        }
    }
    public class WeixinRequestApiException : Exception
    {
        private string _Message;
        private ReturnCode _ReturnCode;
        public override string Message
        {
            get
            {
                return this._Message;
            }
        }
        public ReturnCode ReturnCode
        {
            get
            {
                return this._ReturnCode;
            }
        }
        public WeixinRequestApiException()
        {
        }
        public WeixinRequestApiException(string message, ReturnCode returnCode)
        {
            this._Message = message;
            this._ReturnCode = returnCode;
        }
    }
}
