using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenDental;
using WpfControls.UI;

namespace SparksBrainUploader {
	///<summary></summary>
	public partial class FrmMain:FrmODBase {
		public FrmMain() {
			InitializeComponent();
			BitmapSource bitmapSource=BitmapSource.Create(
				pixelWidth:16,
				pixelHeight:16,
				dpiX:96,
				dpiY:96,
				pixelFormat:PixelFormats.Bgra32,
				palette:null,
				pixels: new byte[16*16*4],
				stride: 16*4);
			Icon=bitmapSource;
			Load+=FrmMain_Load;
			Shown+=FrmMain_Shown;
		}

		private void FrmMain_Load(object sender,EventArgs e) {
			LayoutMenu();
			BringToFront();
		}

		private void FrmMain_Shown(object sender,EventArgs e) {
			FrmDatabase frmDatabase=new FrmDatabase();
			frmDatabase.IsStartup=true;
			frmDatabase.ShowDialog(this);
			if(frmDatabase.IsDialogCancel){
				Application.Current.Shutdown();
				return;
			}
			if(!Mains.ExistsMainDb()){
				Mains.CreateMainDb();
			}
			Prefs.FillCache();
			ClassConvertDatabase classConvertDatabase=new ClassConvertDatabase();
			string prefDbVersion=Prefs.GetString(PrefName.VersionDatabase);
			string toVersion=Assembly.GetExecutingAssembly().GetName().Version.ToString();
			if(!classConvertDatabase.Convert(prefDbVersion,toVersion)) {
				//probably because they hit Cancel
				Application.Current.Shutdown();
				return;
			}
			string folderConnectomes=Prefs.GetString(PrefName.FolderConnectomes);
			if(!Directory.Exists(folderConnectomes)){
				FrmPrefs frmPrefs=new FrmPrefs();
				frmPrefs.ShowDialog(this);
				if(frmPrefs.IsDialogCancel){
					Application.Current.Shutdown();
					return;
				}
			}
		}

		private void LayoutMenu(){
			menuMain.Add(new MenuItem("Projects",menuItemProjects_Click));
			menuMain.Add(new MenuItem("Database",menuItemDatabase_Click));
			menuMain.Add(new MenuItem("Prefs",menuItemPrefs_Click));
		}

		private void menuItemDatabase_Click(object sender,EventArgs e) {
			FrmDatabase frmDatabase=new FrmDatabase();
			frmDatabase.ShowDialog(this);
		}

		private void menuItemPrefs_Click(object sender,EventArgs e) {
			FrmPrefs frmPrefs=new FrmPrefs();
			frmPrefs.ShowDialog(this);
		}

		private void menuItemProjects_Click(object sender,EventArgs e) {
			FrmProjects frmProjects=new FrmProjects();
			frmProjects.ShowDialog(this);
		}
	}
}
