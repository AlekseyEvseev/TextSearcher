using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindText
{
    class Globals
    {
        public static string SettingFile = "settings.xml";

        public static string GetFolderBrowser()
        {
            string folderFind = "";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    folderFind = fbd.SelectedPath;

                }
            }
            return folderFind;
        }
    }
}
