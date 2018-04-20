using System;
using System.Windows.Media;

namespace Algel.WpfIconFonts
{
    internal static partial class Internals
    {
        //private static readonly Uri BaseUri = new Uri("pack://application:,,,/Algel.WpfIconFonts;component/Resources/");

        public static Typeface ToTypeface(this FontIcon icon)
        {
            var typeFaceIndex = BitConverter.GetBytes((int)icon)[2];
            return Typefaces[typeFaceIndex];
        }

        public static string ToChar(this FontIcon icon)
        {
            var bytes = BitConverter.GetBytes((int)icon);
            bytes[2] = 0;
            var intCode = BitConverter.ToInt32(bytes, 0);
            return char.ConvertFromUtf32(intCode);
        }
    }
}