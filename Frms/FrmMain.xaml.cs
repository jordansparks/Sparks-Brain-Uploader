using System;
using System.Collections.Generic;
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
		}

		private void FrmMain_Load(object sender,EventArgs e) {
			LayoutMenu();
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
			string prefDbVersion=Prefs.GetString(PrefName.DataBaseVersion);
			string toVersion=Assembly.GetExecutingAssembly().GetName().Version.ToString();
			if(!classConvertDatabase.Convert(prefDbVersion,toVersion)) {
				//probably because they hit Cancel
				Application.Current.Shutdown();
			}
			BringToFront();
		}

		private void LayoutMenu(){//typically called in Loaded()
			//Projects-----------------------------------------------------------------------------------------------------------
			MenuItem menuItemProjects=new MenuItem("Projects");
			menuMain.Add(menuItemProjects);
			menuItemProjects.Add(new MenuItem("New",menuItemProjectNew_Click));
			menuItemProjects.Add(new MenuItem("Open",menuItemProjectOpen_Click));
			menuItemProjects.Add(new MenuItem("Close",menuItemProjectClose_Click));
			menuItemProjects.AddSeparator();
			menuItemProjects.Add(new MenuItem("Database",menuItemProjectDatabase_Click));
		}

		private void menuItemProjectClose_Click(object sender,EventArgs e) {
			MsgBox.Show("Close");
		}

		private void menuItemProjectDatabase_Click(object sender,EventArgs e) {
			FrmDatabase frmDatabase=new FrmDatabase();
			frmDatabase.ShowDialog(this);
		}

		private void menuItemProjectNew_Click(object sender,EventArgs e) {
			MsgBox.Show("New");
		}

		private void menuItemProjectOpen_Click(object sender,EventArgs e) {
			MsgBox.Show("Open");
		}
	}
}
