using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace YN.XFCounterLabelControl.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NumberFormatInfo nfi = culture.NumberFormat;
            var decimalValue = double.Parse(value.ToString()).ToString("C2");
            var stringFormatted = decimalValue.ToString().Replace($"{nfi.CurrencySymbol}", $"{nfi.CurrencySymbol} ");
            return string.IsNullOrEmpty(stringFormatted) ? decimalValue : stringFormatted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
}
