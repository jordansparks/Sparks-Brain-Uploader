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
	public partial class FrmDatabase:FrmODBase {
		public bool IsStartup;
		private string _filePath="DatabaseSettings.xml";

		public FrmDatabase() {
			InitializeComponent();
			Load+=FrmDatabase_Load;
		}

		private void FrmDatabase_Load(object sender,EventArgs e) {
			if(File.Exists(_filePath)){
				XmlDocument xmlDocument=new XmlDocument();
				xmlDocument.Load(_filePath);
				textServer.Text=xmlDocument.SelectSingleNode("/DatabaseConnection/Server").InnerText;
				textUser.Text= xmlDocument.SelectSingleNode("/DatabaseConnection/User").InnerText;
				textPassword.Text = xmlDocument.SelectSingleNode("/DatabaseConnection/Password").InnerText;
			}
			else{
				textServer.Text="localhost";
				textUser.Text="root";
			}
			if(!IsStartup){
				return;
			}
			//From here down is startup
			if(!IsConnectionValid()){
				//if connection is not valid, we need to stay in this window
				return;
			}
			//from here down, this is startup and this window will never even show
			if(File.Exists(_filePath)){
				//No need to save because user never got a chance to see or change it.
				//and we know it's already good
			}
			else{
				//Just once to initially create the file.
				//Typically localhost, root
				SaveToFile();
			}
			IsDialogOK=true;
		}

		///<summary>This just considers connection to the server, not to any particular database.</summary>
		private bool IsConnectionValid(){
			DataConnection.Server=textServer.Text;
			//can't include or it will error
			//+"Database=sb_main;"
			DataConnection.Database="";
			DataConnection.User=textUser.Text;
			DataConnection.Password=textPassword.Text;
			bool canConnect=Db.CanConnect();
			DataConnection.Database="sb_main";
			return canConnect;
		}

		///<summary>Must validate somehow before calling this.</summary>
		private void SaveToFile(){
			XmlDocument xmlDocument=new XmlDocument();
			XmlElement xmlElementRoot=xmlDocument.CreateElement("DatabaseConnection");
			xmlDocument.AppendChild(xmlElementRoot);
			XmlElement xmlElementServer=xmlDocument.CreateElement("Server");
			xmlElementServer.InnerText=textServer.Text;
			xmlElementRoot.AppendChild(xmlElementServer);
			XmlElement xmlElementUser=xmlDocument.CreateElement("User");
			xmlElementUser.InnerText=textUser.Text;
			xmlElementRoot.AppendChild(xmlElementUser);
			XmlElement xmlElementPassword=xmlDocument.CreateElement("Password");
			xmlElementPassword.InnerText=textPassword.Text;
			xmlElementRoot.AppendChild(xmlElementPassword);
			//overwrites
			xmlDocument.Save(_filePath);
		}

		private void buttonSave_Click(object sender,EventArgs e) {
			if(!IsConnectionValid()){
				MsgBox.Show("Invalid");
				return;
			}
			SaveToFile();
			IsDialogOK=true;
		}
	}
}
