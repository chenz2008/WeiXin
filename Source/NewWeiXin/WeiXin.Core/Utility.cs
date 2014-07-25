using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace WeiXin.Core
{
    internal sealed class XmlHelper
    {
        internal static Dictionary<string, string> Read(string xml)
        {
            var result = new Dictionary<string, string>();
            XElement xmlElement = XElement.Parse(xml);
            var elements = xmlElement.Elements().ToList();
            foreach (var elemet in elements)
            {
                result.Add(elemet.Name.LocalName, elemet.Value);
            }
            return result;
        }

        /// <summary>
        /// 返回数组索引对应 params
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="elementNames"></param>
        /// <returns></returns>
        internal static string[] GetElementValue(string xml, params string[] elementNames)
        {
            var result = new string[elementNames.Length];
            XElement xmlElement = XElement.Parse(xml);
            var elements = xmlElement.Elements().ToList();
            for (int i = 0; i < elementNames.Length; i++)
            {
                var tmps = elements.Where(e => e.Name.LocalName.Equals(elementNames[i])).ToList();
                if (tmps.Count > 0)
                {
                    result[i] = tmps[0].Value;
                }
            }
            return result;
        }
    }

    /******************************************************************************/

    /// <summary>
    /// 签名帮助
    /// </summary>
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
            var tmpStr = SignatureHelper.CreateSignature(tempArr);
            return tmpStr.Equals(signature);
        }
    }
}
