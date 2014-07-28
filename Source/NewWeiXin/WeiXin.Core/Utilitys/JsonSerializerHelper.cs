using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace WeiXin.Core
{
    internal sealed class JsonSerializerHelper
    {
        internal static Dictionary<string, object> Deserialize(string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, object>>(json);
        }

        internal static IList<Dictionary<string, object>> Deserializes(string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<IList<Dictionary<string, object>>>(json);
        }

        internal static string Serialize(object obj)
        {
            var jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        internal static T ConvertJsonStringToObject<T>(string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        internal static IList<T> ConvertJsonStringToObjects<T>(string jsonStr)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<IList<T>>(jsonStr);
        }
    }
}
