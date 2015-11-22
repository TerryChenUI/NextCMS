using System;
using System.Runtime.Serialization;

namespace NextCMS.Core
{
    /// <summary>
    /// 应用程序异常类
    /// </summary>
    [Serializable]
    public class NextCMSException : Exception
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public NextCMSException()
        {
        }

        /// <summary>
        /// 带有错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public NextCMSException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 带有错误格式
        /// </summary>
        /// <param name="messageFormat">异常信息格式</param>
        /// <param name="args">错误参数</param>
        public NextCMSException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// 带有序列化数据
        /// </summary>
        /// <param name="info">序列化异常错误</param>
        /// <param name="context">有关上下文</param>
        protected NextCMSException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 初始化与指定的错误消息，并引用内部异常，它是此异常原因的异常类的新实例。
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">这是当前异常的原因，或者一个空引用，如果没有指定内部异常的异常</param>
        public NextCMSException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
