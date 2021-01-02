using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mh.Utils
{
    public static class Ext
    {
        #region < Is.. >
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                return false;

            return items.Contains(item);
        }
        public static bool NotIn<T>(this T item, params T[] items)
        {
            if (items == null)
                return false;

            return !items.Contains(item);
        }
        public static bool IsNull(this object value)
        {
            if (value == DBNull.Value || value == null || (value.GetType() == typeof(string) && (string.IsNullOrEmpty(value.ToString())|| string.IsNullOrWhiteSpace(value.ToString()))))
                return true;
            return false;
        }

        public static bool IsNotNull(this object value)
        {
            return !IsNull(value);
        }

        /// <summary>
        /// Returns if the value is integer.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns></returns>
        public static bool IsInteger(this object value)
        {
            if (IsNull(value)) return false;
            return Regex.IsMatch(value.ToString(), "^\\d+$");
        }
        /// <summary>
        /// Return if the value is integer or  decimal.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns></returns>
        public static bool IsIntegerOrDecimal(this object value)
        {
            if (IsNull(value)) return false;

            if (IsInteger(value))
            {
                return true;
            }

            decimal decValue;
            return decimal.TryParse(value.ToString(), out decValue);
        }

        public static bool IsList(this object value)
        {
            if (value == null) return false;
            return value is IList &&
                   value.GetType().IsGenericType &&
                   value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(this object value)
        {
            if (value == null) return false;
            return value is IDictionary &&
                   value.GetType().IsGenericType &&
                   value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        public static bool IsGuid(this object value)
        {
            if (value.IsNull()) return false;
            Guid guidValue;
            return Guid.TryParse(value.ToString(), out guidValue);
        }

        public static string ToHexWithSeparator(this byte[] param)
        {
            return string.Join("-", param.Select(x => x.ToString("X")).ToArray());
        }
        #endregion

        #region < To.. >
        public static short ToInt16(this object value)
        {
            return Convert.ToInt16(value);
        }
        public static int ToInt32(this object value)
        {
            return Convert.ToInt32(value);
        }
        public static long ToInt64(this object value)
        {
            return Convert.ToInt64(value);
        }
        public static decimal ToDecimal(this object value)
        {
            return Convert.ToDecimal(value);
        }
        public static byte ToByte(this object value)
        {
            return Convert.ToByte(value);
        }
        public static byte[] ToByteArray(this object value)
        {
            return (byte[])value;
        }
        public static DateTime ToDateTime(this object value)
        {
            return Convert.ToDateTime(value);
        }
        public static DateTime ToDateTimeFromNumber(this double value)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(value);
        }
        public static double ToNumberFromDateTime(this DateTime value)
        {
            return value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
       
        public static bool ToBool(this object value)
        {
            return Convert.ToBoolean(value);
        }
        public static Guid ToGuid(this object value)
        {
            return new Guid(value.ToString());
        }

        /// <summary>
        /// Returns null if value is null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringNull(this object value)
        {
            if (value.IsNull()) return null;
            return value.ToString();
        }

        public static short? ToInt16Null(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToInt16(value);
        }
        public static int? ToInt32Null(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToInt32(value);
        }
        public static long? ToInt64Null(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToInt64(value);
        }
        public static decimal? ToDecimalNull(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToDecimal(value);
        }
        public static byte? ToByteNull(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToByte(value);
        }
        public static byte[] ToByteArrayNull(this object value)
        {
            if (value.IsNull()) return null;
            return (byte[])value;
        }
        public static DateTime? ToDateTimeNull(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToDateTime(value);
        }
        public static DateTime? ToDateTimeFromNumberNull(this double? value)
        {
            if (value.IsNull()) return null;
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(value.Value);
        }
        public static double? ToNumberFromDateTimeNull(this DateTime? value)
        {
            if (value.IsNull()) return null;
            return value.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        public static bool? ToBoolNull(this object value)
        {
            if (value.IsNull()) return null;
            return Convert.ToBoolean(value);
        }
        public static Guid? ToGuidNull(this object value)
        {
            if (value.IsNull()) return null;
            return new Guid(value.ToString());
        }

        public static string ToStrSeparated<T>(this List<T> value)
        {
            if (value == null || value.Count == 0)
            {
                return null;
            }
            string sonuc = "";
            Type type = null;
            foreach (object item in value)
            {
                type = item.GetType();
                if(type.IsEnum)
                {
                    sonuc += ((int)item).ToString() + ",";
                }
                else
                {
                    sonuc += item.ToString() + ",";
                }
            }
            sonuc = sonuc.Substring(0, sonuc.Length - 1);
            return sonuc;
        }

        public static byte[] ToByteFromHex(this string veri)
        {
            string[] veri_arr = veri.Split('-');
            byte[] sonuc = new byte[veri_arr.Length];
            for (int i = 0; i < veri_arr.Length; i++)
            {
                sonuc[i] = byte.Parse(veri_arr[i], System.Globalization.NumberStyles.HexNumber);
            }
            return sonuc;
        }

        public static string ToTitleCase(this string _S, string _sCulture = "tr-TR")
        {
            if (!String.IsNullOrEmpty(_S))
            {
                _S = _S.Trim();
                if (_S.Length > 0)
                {
                    System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(_sCulture);
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    _S = textInfo.ToLower(_S);
                    _S = textInfo.ToTitleCase(_S);
                }
            }
            return _S;
        }

        public static string ToMd5(this string veri)
        {
            if (veri.IsNull()) return null;

            System.Security.Cryptography.MD5 MD5Pass = System.Security.Cryptography.MD5.Create();
            byte[] MD5Buff = MD5Pass.ComputeHash(System.Text.Encoding.GetEncoding(1254).GetBytes(veri));
            return BitConverter.ToString(MD5Buff).Replace("-", string.Empty);
        }

        public static string ToUpperNull(this string value)
        {
            if (value.IsNull()) return null;
            return value.ToUpper();
        }
        public static string ToLowerNull(this string value)
        {
            if (value.IsNull()) return null;
            return value.ToLower();
        }
        #endregion
    }
}