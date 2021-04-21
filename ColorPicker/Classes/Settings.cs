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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ColorPicker.Classes
{
	/// <summary>
	/// Settings of ColorPicker
	/// </summary>
	public class Settings
	{
		/// <summary>
		/// True if the theme of ColorPicker is set to dark.
		/// </summary>
		public bool IsDarkTheme { get; set; }

		/// <summary>
		/// The language of the app (country code). Can be _default, en-US, fr-FR...
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// True if ColorPicker should check updates on start.
		/// </summary>
		public bool CheckUpdatesOnStart { get; set; }

		/// <summary>
		/// True if ColorPicker should show a notification to the user.
		/// </summary>
		public bool NotifyUpdates { get; set; }
	}

	/// <summary>
	/// Class that contains methods that can manage ColorPicker' settings.
	/// </summary>
	public static class SettingsManager
	{
		/// <summary>
		/// Loads ColorPicker settings.
		/// </summary>
		public static void Load()
		{
			string path = Env.AppDataPath + @"\Léo Corporation\ColorPicker\Settings.xml"; // The path of the settings file

			if (File.Exists(path)) // If the file exist
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings)); // XML Serializer
				StreamReader streamReader = new StreamReader(path); // Where the file is going to be read

				Global.Settings = (Settings)xmlSerializer.Deserialize(streamReader); // Read

				streamReader.Dispose();
			}
			else
			{
				Global.Settings = new Settings { IsDarkTheme = false, Language = "_default", CheckUpdatesOnStart = true, NotifyUpdates = true }; // Create a new settings file

				Save(); // Save the changes
			}
		}

		/// <summary>
		/// Saves ColorPicker settings.
		/// </summary>
		public static void Save()
		{
			string path = Env.AppDataPath + @"\Léo Corporation\ColorPicker\Settings.xml"; // The path of the settings file

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings)); // Create XML Serializer

			if (!Directory.Exists(Env.AppDataPath + @"\Léo Corporation\ColorPicker")) // If the directory doesn't exist
			{
				Directory.CreateDirectory(Env.AppDataPath + @"\Léo Corporation\"); // Create the directory
				Directory.CreateDirectory(Env.AppDataPath + @"\Léo Corporation\ColorPicker"); // Create the directory
			}

			StreamWriter streamWriter = new StreamWriter(path); // The place where the file is going to be written
			xmlSerializer.Serialize(streamWriter, Global.Settings);

			streamWriter.Dispose();
		}
	}
}