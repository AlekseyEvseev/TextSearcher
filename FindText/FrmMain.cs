using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FindText
{

    public partial class FrmMain : Form
    {
        private string folderFind;
        private FilesSearch filesSearch = null;
        private List<FileSrc> filesSrc = null;
        public FrmMain()
        {
            InitializeComponent();
            Settings settings = Settings.GetSettings();
            folderFind = settings.DefaultFolder;
            lbFolder.Text = folderFind;
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (Directory.Exists(lbFolder.Text))
            {
                filesSearch = new FilesSearch(folderFind);
                filesSearch.Search(tb_search.Text);
                filesSrc = filesSearch.FindFilesSrc;

                lvFiles.Items.Clear();
                foreach (FileSrc file in filesSrc)
                {
                    ListViewItem listViewItem = new ListViewItem(file.Index.ToString());
                    listViewItem.SubItems.Add(file.FileName);
                    listViewItem.SubItems.Add(file.FullFileName);
                    lvFiles.Items.Add(listViewItem);
                }

            }
            else
            {
                MessageBox.Show("Путь не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void findFiles(string searchOrder)
        {

        }

        private string[] GetAllFilesFromFolder(string folder)
        {
            string[] files = new string[1];

            return files;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            folderFind = Globals.GetFolderBrowser();
            lbFolder.Text = folderFind;

        }


        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (lvFiles.SelectedItems.Count > 0)
            {
                foreach (FileSrc file in filesSrc)
                {
                    if (file.Index.ToString() == lvFiles.SelectedItems[0].Text)
                    {
                        richTextBox1.Text = file.Content;
                        ColourRrbText();

                    }
                }
            }
        }
        private void ColourRrbText()
        {
            Regex regExp = new Regex("(" + tb_search.Text +")");

            foreach (Match match in regExp.Matches(richTextBox1.Text))
            {
                richTextBox1.Select(match.Index, match.Length);
                richTextBox1.SelectionColor = Color.Blue;
            }
        }
        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count > 0)
            {
                try
                {
                Process.Start(lvFiles.SelectedItems[0].SubItems[2].Text) ;

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void открытьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count > 0)
            {
                try
                {
                    Process.Start( Path.GetDirectoryName (lvFiles.SelectedItems[0].SubItems[2].Text));
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
