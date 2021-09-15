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
using ColorHelper;
using ColorPicker.Classes;
using ColorPicker.Enums;
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
	/// Interaction logic for PalettePage.xaml
	/// </summary>
	public partial class PalettePage : Page
	{
		public PalettePage()
		{
			InitializeComponent();
			InitUI();
		}

		private void InitUI()
		{
			// Generate random color
			int r, g, b;
			Random random = new();

			r = random.Next(0, 255); // Generate random number between 0 and 255
			g = random.Next(0, 255); // Generate random number between 0 and 255
			b = random.Next(0, 255); // Generate random number between 0 and 255

			RGBTxt.Text = $"{r}{Global.Settings.RGBSeparator}{g}{Global.Settings.RGBSeparator}{b}"; // Set text
		}

		private RGB[] GetShades(HSL hsl)
		{
			// Dark shades
			HSL darkShade = new(hsl.H, hsl.S, 30);
			RGB darkShadeRgb = ColorHelper.ColorConverter.HslToRgb(darkShade);

			// Regular shades
			var l = hsl.L - 16;
			HSL regularShade = new(hsl.H, hsl.S, (byte)l);
			RGB regularShadeRgb = ColorHelper.ColorConverter.HslToRgb(regularShade);

			// Tint shades
			var s = hsl.S - 20;
			var l2 = hsl.L + 6;

			HSL tintShade = new(hsl.H, (byte)s, (byte)l2);
			RGB tintShadeRgb = ColorHelper.ColorConverter.HslToRgb(tintShade);

			RGB[] colors = { darkShadeRgb, regularShadeRgb, tintShadeRgb };

			return colors;
		}

		private void RGBTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				// Set default color
				string[] rgb = RGBTxt.Text.Split(new string[] { Global.Settings.RGBSeparator }, StringSplitOptions.None);

				ColorDisplayer.Background = new SolidColorBrush { Color = Color.FromRgb((byte)int.Parse(rgb[0]), (byte)int.Parse(rgb[1]), (byte)int.Parse(rgb[2])) };
				BaseShade.Background = new SolidColorBrush { Color = Color.FromRgb((byte)int.Parse(rgb[0]), (byte)int.Parse(rgb[1]), (byte)int.Parse(rgb[2])) };

				// Get shades
				HSL hsl = ColorHelper.ColorConverter.RgbToHsl(new((byte)int.Parse(rgb[0]), (byte)int.Parse(rgb[1]), (byte)int.Parse(rgb[2])));

				var shades = GetShades(hsl); // Get shades
				var shades1 = GetShades(ColorHelper.ColorConverter.RgbToHsl(shades[0])); // Get shades

				DBaseShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades[0].R, shades[0].G, shades[0].B) };
				
				DarkShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades[0].R, shades[0].G, shades[0].B) };
				RegularShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades[1].R, shades[1].G, shades[1].B) };
				TintShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades[2].R, shades[2].G, shades[2].B) };

				DDarkShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades1[0].R, shades1[0].G, shades1[0].B) };
				DRegularShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades1[1].R, shades1[1].G, shades1[1].B) };
				DTintShade.Background = new SolidColorBrush { Color = Color.FromRgb(shades1[2].R, shades1[2].G, shades1[2].B) };

			}
			catch
			{
				
			}
		}
	}
}
