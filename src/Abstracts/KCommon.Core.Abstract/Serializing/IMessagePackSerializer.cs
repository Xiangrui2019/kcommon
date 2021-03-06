﻿using System;

namespace KCommon.Core.Abstract.Serializing
{
    public interface IMessagePackSerializer
    {
        byte[] Serialize(object obj);
        T Deserialize<T>(byte[] data) where T : class;
    }
}