using System;
using System.Globalization;
using Xamarin.Forms;

namespace Notes.Converters
{
	public class SelectedItemChangedEventArgsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var args = value as SelectedItemChangedEventArgs;
			if (args == null)
			{
				throw new ArgumentException("Expected value to be of type SelectedItemChangedEventArgs");
			}
			return args.SelectedItem;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
