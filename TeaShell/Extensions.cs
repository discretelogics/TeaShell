using System;
using System.Collections;
using System.Collections.Generic;
using TeaTime;

namespace TeaTime.TeaShell
{
    static class Extensions
    {
        public static bool IsDecimalType(this FieldType value)
        {
            switch (value)
            {
                case FieldType.Float:
                case FieldType.Double:
                case FieldType.NetDecimal:                
                    return true;

                case FieldType.None:
                case FieldType.Int8:
                case FieldType.Int16:
                case FieldType.Int32:
                case FieldType.Int64:
                case FieldType.UInt8:
                case FieldType.UInt16:
                case FieldType.UInt32:
                case FieldType.UInt64:
                case FieldType.Custom:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        public static string Limit(this string s, int n)
        {
            return s.Substring(0, Math.Min(n, s.Length));
        }
    }
}
