using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using CodeBase;
using DataConnectionBase;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using OpenDental;
using OpenDental.UI;

namespace SparksBrainUploader{

	///<summary></summary>
	public partial class ClassConvertDatabase {
		private System.Version FromVersion;
		private System.Version ToVersion;

		///<summary>Return false to indicate exit app.  Only called when program first starts in Load.</summary>
		public bool Convert(string fromVersion,string toVersion) {
			FromVersion=new Version(fromVersion);
			ToVersion=new Version(toVersion);
			if(Prefs.GetBool(PrefName.CorruptedDatabase)) {
				MsgBox.Show("Your database is corrupted because an update failed.");
				return false;//shuts program down.
			}
			if(FromVersion==ToVersion) {
				return true;//no conversion necessary
			}
			if(FromVersion >= ConvertDatabases.LatestVersion) {
				return true;//no conversion necessary
			}
			if(!MsgBox.Show(MsgBoxButtons.OKCancel,"Your database will now be converted\r\n"
				+"from version "+FromVersion.ToString()+"\r\n"
				+"to version "+ToVersion.ToString()))
			{
				//If user clicks cancel, then close the program
				return false;
			}
			Prefs.UpdateBool(PrefName.CorruptedDatabase,true);
			ConvertDatabases.FromVersion=FromVersion;
			ProgressWin progressWin=new ProgressWin();
			progressWin.ActionMain=() => ConvertDatabases.InvokeConvertMethods();
			progressWin.ShowCancelButton=false;
			progressWin.ShowDialog();
			Prefs.UpdateBool(PrefName.CorruptedDatabase,false);
			return true;
		}

	}

}