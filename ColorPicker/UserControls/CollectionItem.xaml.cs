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

using ColorPicker.Classes;
using ColorPicker.Enums;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker.UserControls;
/// <summary>
/// Interaction logic for CollectionItem.xaml
/// </summary>
public partial class CollectionItem : UserControl
{
	public ColorCollection ColorCollection { get; }
	public int Id { get; }

	public CollectionItem(ColorCollection collectionItem, int id)
	{
		InitializeComponent();
		ColorCollection = collectionItem;
		Id = id;
		InitUI();
	}

	public static event EventHandler<PageEventArgs> GoClick;
	private void InitUI()
	{
		CollectionNameTxt.Text = $"{ColorCollection.Name} ({ColorCollection.Colors.Count})";
		for (int i = 0; i < ColorCollection.Colors.Count; i++)
		{
			var color = ColorHelper.ColorConverter.HexToRgb(new(ColorCollection.Colors[i]));
			Border border = new()
			{
				Background = new SolidColorBrush()
				{
					Color = Color.FromRgb(color.R, color.G, color.B)
				},
				Margin = new(5),
				Height = 25,
				Width = 25,
				CornerRadius = new(5),
				Cursor = Cursors.Hand,
			};

			border.MouseLeftButtonUp += (o, e) =>
			{
				Global.ConverterPage.ColorInfo = new(color);
				Global.ConverterPage.LoadDetails(true);
				GoClick?.Invoke(this, new(AppPages.Converter));
			};

			border.MouseRightButtonUp += (o, e) =>
			{
				Global.Bookmarks.ColorCollections[Id].Colors.RemoveAt(i);
			};

			ColorPanel.Children.Add(border);
		}
	}

	private void DeleteBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.Bookmarks.ColorCollections.RemoveAt(Id);
		Global.BookmarksPage.InitUI();
	}
}
