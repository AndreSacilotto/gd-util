using System;

namespace Util.MathC
{
    public struct Power2 : IComparable, IFormattable, IConvertible, IComparable<Power2>, IEquatable<Power2>
    {
        public static Power2 NewPower2(int exponent) => new Power2((uint)(1 << exponent));

        private uint value;

        private Power2(uint v)
        {
            value = v;
        }
        private Power2(int v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;
            value = (uint)v;
        }

        public uint Value
        {
            get => value;
            set
            {
                value--;
                value |= value >> 1;
                value |= value >> 2;
                value |= value >> 4;
                value |= value >> 8;
                value |= value >> 16;
                value++;
                this.value = value;
            }
        }

        public int IntValue
        {
            get => (int)value;
            set => Value = value <= 0 ? 0 : (uint)value;
        }

        public int GetExponent() => MathOptimization.IntLog2(IntValue);

        public bool IsZero => value == 0;

        public int CompareTo(object obj) => value.CompareTo(obj);
        //public int CompareTo(uint other) => value.CompareTo(other);
        //public int CompareTo(int other) => IntValue.CompareTo(other);
        public int CompareTo(Power2 other) => value.CompareTo(other.value);

        //public bool Equals(uint other) => value.Equals(other);
        //public bool Equals(int other) => IntValue.Equals(other);
        public bool Equals(Power2 other) => value.Equals(other.value);

        public override string ToString() => value.ToString();
        public string ToString(IFormatProvider provider) => value.ToString(provider);
        public string ToString(string format, IFormatProvider formatProvider) => value.ToString(format, formatProvider);

        public TypeCode GetTypeCode() => value.GetTypeCode();

        #region IConvertible

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)value).ToBoolean(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)value).ToByte(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)value).ToChar(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)value).ToDateTime(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)value).ToDecimal(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)value).ToDouble(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)value).ToInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)value).ToInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)value).ToInt64(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible)value).ToSByte(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)value).ToSingle(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)value).ToType(conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible)value).ToUInt16(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible)value).ToUInt32(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible)value).ToUInt64(provider);
        }

        #endregion

        public static explicit operator Power2(int v) => new Power2(v);
        public static explicit operator Power2(uint v) => new Power2(v);

        public static implicit operator int(Power2 p) => p.IntValue;
        public static implicit operator uint(Power2 p) => p.value;

        public static bool operator <(Power2 left, Power2 right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Power2 left, Power2 right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Power2 left, Power2 right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Power2 left, Power2 right)
        {
            return left.CompareTo(right) >= 0;
        }

    }

}
