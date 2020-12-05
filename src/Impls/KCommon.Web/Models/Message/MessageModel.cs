using KCommon.Web.ErrorCode;
using System;

namespace KCommon.Web.Models.Message
{
    public class MessageModel
    {
        // 错误码
        public virtual ErrorType Code { get; set; }
        // 错误消息
        public virtual string Message { get; set; }
        // 返回时间
        public virtual DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
