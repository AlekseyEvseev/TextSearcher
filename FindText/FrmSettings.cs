using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FindText
{
    public partial class FrmSettings : Form
    {
        private Settings settings = new Settings();

        public FrmSettings()
        {
            InitializeComponent();
            settings = Settings.GetSettings();
            initControls();

        }

        private void initControls()
        {
            tbPath.Text = settings.DefaultFolder;
            cbx_fullOrder.Checked = settings.fullOrder;
            cbx_subfolders.Checked = settings.subfolders;
            if (settings.fileTypes != null)
            {
                for (int i = 0; i < settings.fileTypes.Length; i++)
                {
                    lbxType.Items.Add(settings.fileTypes[i]);
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            settings.DefaultFolder = tbPath.Text;
            settings.fullOrder = cbx_fullOrder.Checked;
            settings.subfolders = cbx_subfolders.Checked;
            settings.fileTypes = GetTypes();
            settings.Save();
            this.Close();
        }

        private string[] GetTypes()
        {
            int countTypes = lbxType.Items.Count;
            string[] types = new string[countTypes];
            for (int i = 0; i < countTypes; i++)
            {
                types[i] = lbxType.Items[i].ToString();
            }
                return types;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string extention = tbType.Text;
            extention = extention.ToLower();
            Regex regex = new Regex(@"^.[a-z]");
            if (regex.IsMatch(extention))
             {
            lbxType.Items.Add(extention);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (lbxType.SelectedIndex >=0)
            {
            lbxType.Items.RemoveAt (lbxType.SelectedIndex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            tbPath.Text = Globals.GetFolderBrowser();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
