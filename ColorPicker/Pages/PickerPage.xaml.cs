﻿/*
MIT License

Copyright (c) Léo Corporation

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/
using LeoCorpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker.Pages
{
	/// <summary>
	/// Interaction logic for PickerPage.xaml
	/// </summary>
	public partial class PickerPage : Page
	{
		public PickerPage()
		{
			InitializeComponent();
			InitUI(); // Init UI
		}

		private void InitUI()
		{
			// Generate random color
			Random random = new();
			int r = random.Next(0, 255); int g = random.Next(0, 255); int b = random.Next(0, 255); // Generate random values
			
			// Display
			ColorDisplayer.Background = new SolidColorBrush { Color = Color.FromRgb((byte)r, (byte)g, (byte)b) }; // Display color
			RedSlider.Value = r; // Set value
			GreenSlider.Value = g; // Set value
			BlueSlider.Value = b; // Set value

			// Convert to HEX
			var hex = ColorsConverter.RGBtoHEX(r, g, b); // Convert
			HEXTxt.Text = $"{Properties.Resources.HEXP} #{hex.Value}";
		}

		private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			ColorDisplayer.Background = new SolidColorBrush { Color = Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value) };
			RedValueTxt.Text = RedSlider.Value.ToString(); // Set text

			var h = ColorsConverter.RGBtoHEX((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value); // Convert
			HEXTxt.Text = $"{Properties.Resources.HEXP} #{h.Value}";
		}

		private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			ColorDisplayer.Background = new SolidColorBrush { Color = Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value) };
			GreenValueTxt.Text = GreenSlider.Value.ToString(); // Set text

			var h = ColorsConverter.RGBtoHEX((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value); // Convert
			HEXTxt.Text = $"{Properties.Resources.HEXP} #{h.Value}";
		}

		private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			ColorDisplayer.Background = new SolidColorBrush { Color = Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value) };
			BlueValueTxt.Text = BlueSlider.Value.ToString(); // Set text

			var h = ColorsConverter.RGBtoHEX((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value); // Convert
			HEXTxt.Text = $"{Properties.Resources.HEXP} #{h.Value}";
		}

		private void SelectColorBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void CopyBtn_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText($"{RedSlider.Value};{GreenSlider.Value};{BlueSlider.Value}"); // Copy
		}

		private void CopyHEXBtn_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText("#" + ColorsConverter.RGBtoHEX((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value).Value); // Copy
		}
	}
}