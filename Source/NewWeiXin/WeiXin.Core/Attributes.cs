using System;

namespace WeiXin.Core
{
    /// <summary>
    /// 用于描述 xml 消息中属性是否是发送被动消息的必要属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class PassiveMessagePropertyAttribute : Attribute
    {
        internal PassiveMessagePropertyAttribute(bool isRequired)
        {
            this.IsRequired = isRequired;
        }

        internal bool IsRequired { get; set; }
    }

    /// <summary>
    /// 用于描述 xml 消息中属性包含子节点
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class HaveChildAttribute : Attribute
    {
        internal HaveChildAttribute(bool isChild)
        {
            this.IsChild = isChild;
        }

        internal bool IsChild { get; set; }
    }
}
