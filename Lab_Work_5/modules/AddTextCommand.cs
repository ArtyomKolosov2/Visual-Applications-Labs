using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Lab_Work_5.modules
{
	public static class CustomCommands
	{
		public static readonly RoutedUICommand Save = new RoutedUICommand
			(
				"Save",
				"SaveFromComboBox",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.Return)
				}
			);
	}
}
