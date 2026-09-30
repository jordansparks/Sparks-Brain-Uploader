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
	public partial class FrmProjects:FrmODBase {

		public FrmProjects() {
			InitializeComponent();
			Load+=FrmProjects_Load;
		}

		private void FrmProjects_Load(object sender,EventArgs e) {
			
		}
	}
}
