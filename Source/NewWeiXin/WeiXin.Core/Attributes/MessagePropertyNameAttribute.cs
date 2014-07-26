using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXin.Core
{
    /// <summary>
    /// 消息属性名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class MessagePropertyNameAttribute : Attribute
    {
        internal MessagePropertyNameAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        internal string PropertyName { get; set; }
    }
}
