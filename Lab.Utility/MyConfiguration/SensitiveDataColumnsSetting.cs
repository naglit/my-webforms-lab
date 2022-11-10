using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Lab.Utility.Configuration
{
	public class SensitiveDataColumnsSetting
	{
		/// <summary>Node in xml file</summary>
		private const string ELE_SENSITIVE_DATA_COLUMNS = "SensitiveDataColumns";
		/// <summary>Element: Key</summary>
		private const string ELE_PHYSICAL_COLUMN_NAME = "PhysicalColumnName";
		/// <summary>Configuration Path</summary>
		private static readonly string m_settingFilePath = 
			Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Lab.Utility\Configuration\SensitiveDataColumns.xml");
		/// <summary>Instance</summary>
		private static SensitiveDataColumnsSetting m_SensitiveDataColumnsSetting;
		/// <summary>Lock Object</summary>
		private static readonly object m_padlock = new object();

		/// <summary>
		/// Constractor
		/// </summary>
		private SensitiveDataColumnsSetting()
		{
			ReadSettingFile();
		}

		/// <summary>
		/// Read the configuration file
		/// </summary>
		private void ReadSettingFile()
		{
			if (File.Exists(m_settingFilePath) == false)
			{
				Console.WriteLine("{0} doesn't exist", m_settingFilePath);
				return;
			}

			// Read an xml
			try
			{
				var doc = XDocument.Load(m_settingFilePath);
				this.SensitiveDataColumns = doc.Element(ELE_SENSITIVE_DATA_COLUMNS)
					.Elements().ToDictionary(
						table => table.Name.ToString().Trim(),
						table => table.Elements()
							.Where(col => (col.Name == "PhysicalColumnName"))
							.Select(col => col.Value.ToString().Trim())
							.ToArray());

				this.Ivs = doc.Element(ELE_SENSITIVE_DATA_COLUMNS)
					.Elements().ToDictionary(
						table => table.Name.ToString().Trim(),
						table => table.Elements()
							.FirstOrDefault(col => (col.Name == "IV"))
							.Value.ToString().Trim());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Can't read {0}", m_settingFilePath);
			}
		}

		/// <summary>
		/// Get the instance (Singleton)
		/// </summary>
		public static SensitiveDataColumnsSetting GetInstance
		{
			get
			{
				lock (m_padlock)
				{
					return m_SensitiveDataColumnsSetting ?? (m_SensitiveDataColumnsSetting = new SensitiveDataColumnsSetting());
				}
			}
		}
		/// <summary>SensitiveDataColumns</summary>
		public Dictionary<string, string[]> SensitiveDataColumns { get; private set; }
		/// <summary>IVs (This practice is bad, but I do this for some reasons)</summary>
		public Dictionary<string, string> Ivs { get; private set; }
	}
}
