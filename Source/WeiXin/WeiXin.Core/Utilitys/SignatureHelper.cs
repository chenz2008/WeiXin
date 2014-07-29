using System;
using System.Security.Cryptography;
using System.Text;

namespace WeiXin.Core
{
    internal sealed class SignatureHelper
    {
        /// <summary>
        /// 生成签名，与微信签名规则一样
        /// 必备参数：token、时间戳、随机数（9位）
        /// 生成规则：对参数进行排序生成sha1哈希（不带符号）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string CreateSignature(Array array)
        {
            Array.Sort(array);
            string tmpStr = string.Empty;
            foreach (var tmp in array)
            {
                tmpStr += tmp;
            }
            return BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(new ASCIIEncoding().GetBytes(tmpStr))).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// 生成时间戳
        /// 规则：1970年1月1日至今的间隔秒数
        /// </summary>
        /// <returns></returns>
        internal static string CreateTimestamp()
        {
            return ((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        /// <summary>
        /// 创建9位随机数
        /// </summary>
        /// <returns></returns>
        internal static string CreateRandomNumber()
        {
            return new Random().Next(100000000, 999999999).ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <returns></returns>
        internal static bool CheckSignature(
            string token,
            string signature,
            string timestamp,
            string nonce)
        {
            string[] tempArr = { token, timestamp, nonce };
            var tmpStr = CreateSignature(tempArr);
            return tmpStr.Equals(signature);
        }
    }
}
