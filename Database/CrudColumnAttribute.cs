using System;
using System.Collections.Generic;
using System.Text;

namespace SparksBrainUploader {
	[AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
	public class CrudColumnAttribute : Attribute {
		public CrudColumnAttribute() {
			
		}

		public int Idx {get;set; }
	}
}
