using System.Xml.Linq;

namespace Lab.Utility.JWT
{
    /// <summary>
    /// JWT Setting Class
    /// </summary>
    public class JWTSetting
    {
        private const string ELE_EXPIRE_DAYS = "ExpireDays";
        private static JWTSetting m_JWTSetting = null;
        private static readonly object m_padlock = new object();
               
        /// <summary>
        /// Constructor
        /// </summary>
        private JWTSetting()
        {
            ReadSettingFile();
        }
             
        /// <summary>
        /// Read config values from the setting file
        /// </summary>
        private void ReadSettingFile()
        {
            int defaultExpireDays = 30;
            var doc = XDocument.Load("JWTSetting.xml");
            var expireDays = doc.Element("JWTSetting").Element(ELE_EXPIRE_DAYS).Value;
            int.TryParse(expireDays, out defaultExpireDays);
            
            this.ExpireDays = defaultExpireDays;
        }

        /// <summary>
        /// Get an Instance 
        /// </summary>
        public static JWTSetting GetInstance
        {
            get
            {
                lock (m_padlock)
                {
                    if (m_JWTSetting == null)
                    {
                        m_JWTSetting = new JWTSetting();
                    }
                    return m_JWTSetting;
                }
            }
        }
       
        /// <summary>Days until the token gets invalid</summary>
        public int ExpireDays { get; set; }
    }
}
