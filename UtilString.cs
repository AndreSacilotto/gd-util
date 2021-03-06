using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Util
{
    public static class UtilString
    {
        public const string FLOAT_FIXED_POINT = "0.############################################################################################################";

        public const string numbers = "0123456789";
        public const string charLower = "abcdefghijklmnopqrstuvwxyz";
        public const string charUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        #region Contains

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string source, char value, int startIdx = 0) =>
                source.IndexOf(value, startIdx) >= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string source, string value, int startIdx = 0, StringComparison strCmp = StringComparison.InvariantCultureIgnoreCase) =>
            source.IndexOf(value, startIdx, strCmp) >= 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string source, string value, int startIdx, int length, StringComparison strCmp = StringComparison.InvariantCultureIgnoreCase) =>
            source.IndexOf(value, startIdx, length, strCmp) >= 0;

        #endregion

        #region Percent

        public static NumberFormatInfo PercentFormat => new NumberFormatInfo
        {
            PercentGroupSeparator = string.Empty,
            PercentPositivePattern = 1,
            PercentNegativePattern = 1,
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NumberWithSign(int value) => value.ToString("+#;-#;0");
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NumberWithSign(float value) => value.ToString("+#;-#;0");


        public static string LowPrecisePercentage(float value) =>
            value.ToString("0.##\\%", PercentFormat);

        public static string PrecisePercentage(float value, out decimal decimalValue)
        {
            decimalValue = Math.Floor((decimal)value * 100m) / 100m;
            if (Math.Floor(decimalValue) % 1 == 0)
                return decimalValue.ToString("P0", PercentFormat);
            return decimalValue.ToString("P1", PercentFormat);
        }

        #endregion

        public static string EnumToName(Enum enumValue)
        {
            var str = enumValue.ToString();
            var sb = new StringBuilder(str.Length);

            if (char.IsLetterOrDigit(str[0]))
                sb.Append(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                var c = str[i];
                if (c == '_')
                    sb.Append(' ');
                else if (char.IsUpper(c) || char.IsNumber(c))
                    sb.Append(' ' + c);
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }

        public static string NicifyVariableName(string str)
        {
            if (str == null || str.Length == 0)
                return string.Empty;

            str = str.Trim();
            var sb = new StringBuilder();

            int i = 1;

            if (char.IsLetter(str[0]))
                sb.Append(char.ToUpper(str[0]));
            else if (str[0] == '_' && str.Length > 1)
                sb.Append(char.ToUpper(str[i++]));

            for (; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    var i1 = i + 1;
                    if (i1 < str.Length && char.IsLetterOrDigit(str[i1]) && !char.IsUpper(str[i1]))
                        sb.Append(' ');
                }
                sb.Append(str[i]);
            }

            return sb.ToString();
        }

        public static string[] SplitAndReplace(string str, char separator = ' ')
        {
            if (str == null || str.Length == 0)
                return null;

            var len = str.Length;

            var isSplit = new bool[len];
            int foundSplit = 1;

            bool isSeparator, wasSeparator = false;
            for (int i = 1; i < len; i++)
            {
                isSeparator = str[i] == separator;
                if (isSeparator && !wasSeparator)
                {
                    isSplit[i] = true;
                    foundSplit++;
                }
                wasSeparator = isSeparator;
            }

            if (foundSplit == 1)
                return new string[1] { str.Substring(0, str.Length) };

            string strSeparator = "" + separator;
            var split = new string[foundSplit];
            int size = 1, index = 0, start = 0;
            for (int i = 1; i < len; i++, size++)
                if (isSplit[i])
                {
                    split[index++] = str.Substring(start, size).Replace(strSeparator, string.Empty);
                    start = i + 1;
                    size = 0;
                }
            split[index] = str.Substring(start, --size).Replace(strSeparator, string.Empty);

            return split;
        }

        public static string GenerateID(int lenght, bool allowNumbers, bool allowLowerCase, bool allowUpperCase)
        {
            const string numbers = "0123456789";
            const string charLower = "abcdefghijklmnopqrstuvwxyz";
            const string charUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var allowChars = string.Empty;
            if (allowNumbers) allowChars += numbers;
            if (allowLowerCase) allowChars += charLower;
            if (allowUpperCase) allowChars += charUpper;

            var rng = new Random();
            var sb = new StringBuilder(lenght);
            var len = allowChars.Length;
            for (int i = 0; i < lenght; i++)
                sb.Append(allowChars[rng.Next(len)]);
            return sb.ToString();
        }

    }

}