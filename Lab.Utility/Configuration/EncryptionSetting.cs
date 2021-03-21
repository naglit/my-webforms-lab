using System;
using System.IO;
using System.Xml.Linq;


namespace Lab.Utility.Configuration
{
	public class EncryptionSetting
	{
		/// <summary>Node in xml file</summary>
		private const string ELE_ENCRYPTION_SETTING = "EncryptionSetting";
		/// <summary>Element: Key</summary>
		private const string ELE_KEY = "Key";
		/// <summary>Configuration Path</summary>
		private static readonly string m_settingFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Lab.Utility\Configuration\EncryptionSetting.xml");
		/// <summary>Instance</summary>
		private static EncryptionSetting m_EncryptionSetting;
		/// <summary>Lock Object</summary>
		private static readonly object m_padlock = new object();

		/// <summary>
		/// Constractor
		/// </summary>
		private EncryptionSetting()
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
				this.Key = Convert.FromBase64String(
					doc.Element(ELE_ENCRYPTION_SETTING).Element(ELE_KEY).Value);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Can't read {0}", m_settingFilePath);
			}
		}

		/// <summary>
		/// Get the instance (Singleton)
		/// </summary>
		public static EncryptionSetting GetInstance
		{
			get
			{
				lock (m_padlock)
				{
					return m_EncryptionSetting ?? (m_EncryptionSetting = new EncryptionSetting());
				}
			}
		}
		/// <summary>Key</summary>
		internal byte[] Key { get; private set; }
	}
}