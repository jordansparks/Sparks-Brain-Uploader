using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnectionBase;
using SparksBrainUploader.Crud;

namespace SparksBrainUploader {
	public static class Prefs {
		public static List<Pref> ListPrefsCached;

		public static void FillCache(){
			string command="SELECT * FROM pref";
			ListPrefsCached=PrefCrud.SelectMany(command);
		}

		public static bool GetBool(PrefName prefName){
			Pref pref=ListPrefsCached.Find(x=>x.PrefName==prefName.ToString());
			if(pref.ValueString=="1"){
				return true;
			}
			return false;
		}

		public static string GetString(PrefName prefName){
			Pref pref=ListPrefsCached.Find(x=>x.PrefName==prefName.ToString());
			return pref.ValueString;
		}

		///<summary>Returns true if a change was made to db, although the return value is typically ignored.</summary>
		public static bool UpdateBool(PrefName prefName,bool boolNew){
			Pref prefCached=ListPrefsCached.Find(x=>x.PrefName==prefName.ToString());
			bool boolOld=SIn.Bool(prefCached.ValueString);
			if(boolOld==boolNew) {
				//no change needed
				return false;
			}
			string command="UPDATE pref SET "
				+"ValueString = '"+SOut.Bool(boolNew)+"' "
				+"WHERE PrefName = '"+SOut.String(prefName.ToString())+"'";
			Db.NonQ(command);
			prefCached.ValueString=SOut.Bool(boolNew);
			return true;
		}

		///<summary>Returns true if a change was made to db, although the return value is typically ignored.</summary>
		public static bool UpdateString(PrefName prefName,string strNew){
			Pref prefCached=ListPrefsCached.Find(x=>x.PrefName==prefName.ToString());
			string strOld=prefCached.ValueString;
			if(strOld==strNew) {
				//no change needed
				return false;
			}
			string command="UPDATE pref SET "
				+"ValueString = '"+SOut.String(strNew)+"' "
				+"WHERE PrefName = '"+SOut.String(prefName.ToString())+"'";
			Db.NonQ(command);
			prefCached.ValueString=strNew;
			return true;
		}
	}
}
