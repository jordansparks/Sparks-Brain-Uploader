using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenDental;
using WpfControls.UI;

namespace SparksBrainUploader {
	///<summary></summary>
	public partial class FrmPrefs:FrmODBase {

		public FrmPrefs() {
			InitializeComponent();
			Load+=FrmPrefs_Load;
		}

		private void FrmPrefs_Load(object sender,EventArgs e) {
			string folderConnectomes=Prefs.GetString(PrefName.FolderConnectomes);
			if(folderConnectomes==""){
				//first time run
				folderConnectomes=@"C:\SparksBrainConnectomes\";
			}
			textFolderConnectomes.Text=folderConnectomes;
		}

		private void buttonBrowse_Click(object sender,EventArgs e) {
			System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			folderBrowserDialog.SelectedPath=textFolderConnectomes.Text;
			if(folderBrowserDialog.ShowDialog()==System.Windows.Forms.DialogResult.Cancel) {
				return;
			}
			textFolderConnectomes.Text=folderBrowserDialog.SelectedPath;
		}

		private void buttonSave_Click(object sender,EventArgs e) {
			if(!Directory.Exists(textFolderConnectomes.Text)){
				if(!MsgBox.Show(MsgBoxButtons.YesNo,textFolderConnectomes.Text+" does not exist. Create?")){
					return;
				}
				try {
					Directory.CreateDirectory(textFolderConnectomes.Text);
				}
				catch(Exception ex){
					MsgBox.Show("Failed to created folder.\r\n"+ex.Message);
					return;
				}
			}
			//End of validation
			Prefs.UpdateString(PrefName.FolderConnectomes,textFolderConnectomes.Text);
			IsDialogOK=true;
		}
	}
}
