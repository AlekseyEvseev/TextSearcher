using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindText
{
   public class Settings
    {
        public string DefaultFolder { get; set; } 
        public string[] fileTypes { get; set; }
        public bool subfolders { get; set; }
        public bool fullOrder { get; set; }

        public static Settings GetSettings()
        {
            Settings settings = null;
            string fileNameSettings = Globals.SettingFile;

            if (File.Exists(fileNameSettings))
            {
                using (FileStream fs = new FileStream(fileNameSettings, FileMode.Open))
                {

                    XmlSerializer xser = new XmlSerializer(typeof(Settings));
                    settings = (Settings)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else settings = new Settings();

            return settings;
        }
        public void Save()
        {
            string fileNameSettings = Globals.SettingFile;
            if (File.Exists(fileNameSettings)) File.Delete(fileNameSettings);

            using (FileStream fs = new FileStream(fileNameSettings, FileMode.Create))
            {
                XmlSerializer xser = new XmlSerializer(typeof(Settings));
                xser.Serialize(fs, this);
                fs.Close();
            }



        }




    }
}
