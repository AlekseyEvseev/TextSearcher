using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindText
{
    class FilesSearch
    {
        
        public string Folder { get; set; }
        public List<FileSrc> FindFilesSrc { get; set; }
        public List<FileSrc> AllFilesSrc { get; set; }
        public Settings settings;

        private string[] files;
        public FilesSearch()
        {
            settings = Settings.GetSettings();
            Folder = settings.DefaultFolder;
            GetAllFiles();
        }

        public FilesSearch(string folder)
        {           
            settings = Settings.GetSettings();
            Folder = folder;
            GetAllFiles();
        }

        internal void Search(string text)
        {
            FindFilesSrc = new List<FileSrc>();
            GetFilesContent(files);
            Regex regex = new Regex(@"("+ text +")");
            foreach (FileSrc fileSrc in AllFilesSrc)
            {
                if (regex.IsMatch(fileSrc.Content))
                {
                    FindFilesSrc.Add(fileSrc);
                }
            }
    }

        public void GetAllFiles()
        {
            
            if (settings.subfolders)
            {
               files = settings.fileTypes.SelectMany(filter => Directory.GetFiles(Folder,"*"+ filter,
                                    SearchOption.AllDirectories)).ToArray();
            }
            else
            {
               files = settings.fileTypes.SelectMany(filter => Directory.GetFiles(Folder, "*" + filter,
                                     SearchOption.TopDirectoryOnly)).ToArray();
            }
        }
       

        private List<string> getFileList(string path)
        {
            List<string> fileList=null;

            fileList.AddRange( Directory.GetFiles(path));
           


            return fileList;
        }

        public void GetFilesContent(string[] files)
        {
            int i=0;
            AllFilesSrc = new List<FileSrc>();
           foreach(string file in files)
            {

                StreamReader reader = new StreamReader(file);
                FileSrc fileSrc = new FileSrc();
                fileSrc.Index = i;
                fileSrc.Content = reader.ReadToEnd();
                fileSrc.FullFileName = file;
                fileSrc.FileName = Path.GetFileName(file);
                AllFilesSrc.Add(fileSrc);
                i++;
            }
        }

    }
    struct FileSrc
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public string FullFileName { get; set; }
        public bool isContan { get; set; }
        public int FindIndex { get; set; }
        public string Content { get; set; }
    }

}
