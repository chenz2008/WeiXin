using System;

namespace WeiXin.Core
{
    internal sealed class QRCode
    {
        /// <summary>
        /// 创建临时带参数二维码
        /// </summary>
        /// <param name="scene_id">参数值</param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        internal static QRCodeInfo CreateQR_SCENE(long sceneId, string accessToken)
        {
            var obj = new { expire_seconds = 1800, action_name = "QR_SCENE", action_info = new { scene = new { scene_id = sceneId } } };
            return CreateQR(obj, accessToken);
        }

        /// <summary>
        /// 创建永久带参数二维码
        /// </summary>
        /// <param name="scene_id">参数值</param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        internal static QRCodeInfo CreateQR_LIMIT_SCEN(long sceneId, string accessToken)
        {
            var obj = new { action_name = "QR_LIMIT_SCENE", action_info = new { scene = new { scene_id = sceneId } } };
            return CreateQR(obj, accessToken);
        }

        private static QRCodeInfo CreateQR(object obj, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", accessToken);
            var json = JsonSerializerHelper.Serialize(obj);
            Log.Debug("\r\n创建参数二维码 json数据：>>\r\n{0}", json);
            var resultJson = HttpRequestHelper.PostHttp_ForamtByJson(url, json);
            Log.Debug("\r\n创建参数二维码 json返回值：>>\r\n{0}", resultJson);
            var returnCode = GlobalReturnCode.GetReturnCode(resultJson);
            if (!returnCode.IsRequestSuccess)
            {
                throw new WeixinRequestApiException(string.Format("创建二维码失败\r\n全局返回值：{0}\r\n对应说明：{1}\r\nJson：{2}\r\n请求路径：{3}", returnCode.ErrCode, returnCode.Msg, returnCode.Json, url), returnCode);
            }
            var jsonObj = JsonSerializerHelper.Deserialize(resultJson);
            var qrCodeInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<QRCodeInfo>(json);
            return qrCodeInfo;
        }
    }
}
