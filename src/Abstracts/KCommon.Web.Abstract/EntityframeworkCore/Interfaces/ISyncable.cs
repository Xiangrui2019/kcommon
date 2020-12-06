namespace KCommon.Web.Abstract.EntityframeworkCore
{
    public interface ISyncable<T>
    {
        bool EqualsInDb(T obj);
        T Map();
    }
}