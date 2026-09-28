using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparksBrainUploader {
	///<summary></summary>
	public class Pref {
		///<summary>PK, but we really use PrefName</summary>
		[CrudColumn(Idx=0)]
		public int PrefNum;
		///<summary></summary>
		[CrudColumn(Idx=1)]
		public string PrefName;
		///<summary></summary>
		[CrudColumn(Idx=2)]
		public string ValueString;
	}

	///<summary>Because this enum is stored in the database as strings rather than as numbers, we keep the order alphabetically.</summary>
	public enum PrefName {
		///<summary>Boolean 0 or 1. Gets set to true when an update is in progress and then will be set to false when finished.  Otherwise, true means that the database is in a corrupt state.</summary>
		CorruptedDatabase,
		///<summary>Example: "26.5.0.0" Major version is year, and minor version increments each time we need to change the db schema, and possibly more frequent than that.</summary>
		DataBaseVersion,
		///<summary>Example format: "26.5.0.0" Major version is year, and minor version increments each time we need to change the db schema, and possibly more frequent than that.</summary>
		ProgramVersion,
	}
}
