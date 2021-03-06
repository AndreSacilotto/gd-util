using Godot;
using System.Text.RegularExpressions;

namespace Util
{
    public static class UtilColor
    {
        public enum ColorIndex : int
        {
            R,
            G,
            B,
            A
        }

        private static readonly RandomNumberGenerator colorRng = new RandomNumberGenerator();
        private static readonly Regex colorExpression = new Regex(@"#?[0-9A-Fa-f]{6}");

        public static bool HexStringIsValidColor(string value) => colorExpression.IsMatch(value);

        public static Color RandomColor(float alpha = 1) =>
            new Color(colorRng.Randf(), colorRng.Randf(), colorRng.Randf(), alpha);

        public static Color PseudoRandomColor(string colorStr, float alpha = 1)
        {
            long r = 0, g = 0, b = 0;
            for (int i = 0; i < colorStr.Length; i++)
            {
                int charValue = colorStr[i];
                r += charValue;
                g += charValue + charValue;
                b += charValue + charValue + charValue;
            }
            return new Color((r % 255) / 255f, (g % 255) / 255f, (b % 255) / 255f, alpha);
        }



        #region Text Color

        //https://stackoverflow.com/questions/3942878/how-to-decide-font-color-in-white-or-black-depending-on-background-color
        public static Color TextColorFromColor(Color color, Color lightColor, Color darkColor) =>
            (color.r * 0.299f + color.g * 0.587f + color.b * 0.114f) > 0.729f ? darkColor : lightColor;

        public static Color TextColorFromColorLerp(Color color, Color lightColor, Color darkColor) =>
           lightColor.LinearInterpolate(darkColor, color.r * 0.299f + color.g * 0.587f + color.b * 0.114f);

        public static Color TextColorFromColorAdvanced(Color color, Color lightColor, Color darkColor)
        {
            float[] mult = { 0.2126f, 0.7152f, 0.0722f };
            float L = 0f;
            for (int i = 0; i < 3; i++)
            {
                var c = color[i] < 0.03928f ? color[i] / 12.92f : Mathf.Pow((color[i] + 0.055f) / 1.055f, 2.4f);
                L += c * mult[i];
            }
            return L > 0.179f ? darkColor : lightColor;
        }

        #endregion

        #region Basic Colors

        /// <summary>255 255 255</summary>
        public static Color White => new Color(1, 1, 1, 1);
        /// <summary>0 0 0</summary>
        public static Color Black => new Color(0, 0, 0, 1);
        /// <summary>255 255 255 a:0</summary>
        public static Color Transparent => new Color(1, 1, 1, 0);

        /// <summary>255 0 0</summary>
        public static Color Red => new Color(1, 0, 0, 1);
        /// <summary>0 255 0</summary>
        public static Color Green => new Color(0, 1, 0, 1);
        /// <summary>0 0 255</summary>
        public static Color Blue => new Color(0, 0, 1, 1);

        /// <summary>255 255 0</summary>
        public static Color Yellow => new Color(1, 1, 0, 1);
        /// <summary>255 0 255 - Same as Fuchsia</summary>
        public static Color Magenta => new Color(1, 0, 1, 1);
        /// <summary>0 255 255 - Same as Aqua</summary>
        public static Color Cyan => new Color(0, 1, 1, 1);

        #endregion

        #region Half Colors

        /// <summary>255 128 0</summary>
        public static Color Orange => new Color(1, 0.5f, 0, 1);
        /// <summary>255 0 128 - Same as Rose</summary>
        public static Color DeepPink => new Color(1, 0, 0.5f, 1);

        /// <summary>128 255 0 - Can also be called GreenRed</summary>
        public static Color GreenYellow => new Color(0.5f, 1, 0, 1);
        /// <summary>0 255 128 - Same as SpringGreen</summary>
        public static Color GreenBlue => new Color(0, 1, 0.5f, 1);

        /// <summary>128 0 255 - Usually called ElectricViolet</summary>
        public static Color Violet => new Color(0.5f, 0, 1, 1);
        /// <summary>0 128 255</summary>
        public static Color Azure => new Color(0, 0.5f, 1, 1);

        //Single Half
        /// <summary>128 0 0</summary>
        public static Color Maroon => new Color(0.5f, 0, 0, 1);
        /// <summary>0 128 0</summary>
        public static Color HalfGreen => new Color(0, 0.5f, 0, 1);
        /// <summary>0 0 128</summary>
        public static Color Navy => new Color(0, 0, 0.5f, 1);

        //Double Half
        /// <summary>128 128 0</summary>
        public static Color Olive => new Color(0.5f, 0.5f, 0, 1);
        /// <summary>128 0 128</summary>
        public static Color Purple => new Color(0.5f, 0, 0.5f, 1);
        /// <summary>0 128 128</summary>
        public static Color Teal => new Color(0, 0.5f, 0.5f, 1);

        /// <summary>128 128 128 - Also known as Grey</summary>
        public static Color Gray => new Color(0.5f, 0.5f, 0.5f, 1);

        #endregion

        #region More Colors

        public static Color Copper => new Color(0.72f, 0.45f, 0.2f, 1);
        public static Color Silver => new Color(.75f, .75f, .75f, 1);
        public static Color Gold => new Color(1, 0.84f, 0, 1);

        #endregion

    }
}
