using System;
using System.Linq;
using KCommon.Core.Abstract.Components;
using KCommon.Core.Extensions;
using KCommon.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Utilities
{
    [TestClass]
    public class TypeUtilsTest
    {
        [TestMethod]
        public void TestConvert()
        {
            var t = new T();
            var v = TypeUtils.ConvertType<int>(t);
            
            Assert.AreEqual(v, 10);
        }

        [TestMethod]
        public void TestIsComponent()
        {
            var v = new S();
            
            Assert.AreEqual(TypeUtils.IsComponent(v.GetType()), true);
        }
    }

    [Component]
    internal class S
    {
    }

    internal class T : IConvertible
    {
        public int A { get; set; } = 10;
        
        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public bool ToBoolean(IFormatProvider? provider)
        {
            return false;
        }

        public byte ToByte(IFormatProvider? provider)
        {
            return A.ToString().StringToBytes().First();
        }

        public char ToChar(IFormatProvider? provider)
        {
            return A.ToString().ToCharArray().First();
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            return new DateTime();
        }

        public decimal ToDecimal(IFormatProvider? provider)
        {
            return 0;
        }

        public double ToDouble(IFormatProvider? provider)
        {
            return 0;
        }

        public short ToInt16(IFormatProvider? provider)
        {
            return 0;
        }

        public int ToInt32(IFormatProvider? provider)
        {
            return A;
        }

        public long ToInt64(IFormatProvider? provider)
        {
            return A;
        }

        public sbyte ToSByte(IFormatProvider? provider)
        {
            return SByte.MinValue;
        }

        public float ToSingle(IFormatProvider? provider)
        {
            return 0;
        }

        public string ToString(IFormatProvider? provider)
        {
            return A.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider? provider)
        {
            return 0;
        }

        public ushort ToUInt16(IFormatProvider? provider)
        {
            return 0;
        }

        public uint ToUInt32(IFormatProvider? provider)
        {
            return 0;
        }

        public ulong ToUInt64(IFormatProvider? provider)
        {
            return 0;
        }
    }
}