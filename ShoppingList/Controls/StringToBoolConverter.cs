using System.Globalization;

namespace ShoppingList.Controls;

public class StringToBoolConverter : IValueConverter 
{
	public StringToBoolConverter() 
	{

	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		Console.WriteLine("Test");
		var asString = (string)value; 
		if (value is not null)
		{
			var result = Boolean.Parse(asString);
			return result;
		}
		else
		{
			return false; 
		}
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		Console.WriteLine("Test");
		return null; 
	}
}