namespace KCommon.Core.Abstract.Serializing
{
    public interface IProtobufSerializer
    {
        byte[] Serialize(object obj);
        T Deserialize<T>(byte[] data) where T : class;
    }
}