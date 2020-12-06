namespace KCommon.Web.Models.Message
{
    public class MessageValue<T> : MessageModel
    {
        public MessageValue(T value)
        {
            Value = value;
        }

        // 消息值
        public T Value { get; set; }
    }
}