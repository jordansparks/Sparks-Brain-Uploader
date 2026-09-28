using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnectionBase;

namespace SparksBrainUploader{
	public class Mains {
		///<summary></summary>
		public static bool ExistsMainDb(){
			string dbName="sb_main";
			string command="SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '"+SOut.String(dbName)+"'";
			DataConnection.Database="";
			DataTable table=Db.GetTable(command);
			DataConnection.Database="sb_main";
			if(table.Rows.Count==0){
				return false;
			}
			return true;
		}

		///<summary>Only call this if it does not already exist.</summary>
		public static void CreateMainDb(){
			string dbName="sb_main";
			DataConnection.Database="";
			string command="CREATE DATABASE "+SOut.String(dbName);
			Db.NonQ(command);
			DataConnection.Database=dbName;
			//This is just basic schema.
			//Most of the schema will be handled with conversion scripts.
			command=@"CREATE TABLE pref (
				PrefNum bigint NOT NULL auto_increment PRIMARY KEY,
				PrefName varchar(255) NOT NULL,
				ValueString text NOT NULL
				) DEFAULT CHARSET=utf8 ENGINE=MyISAM";
			Db.NonQ(command);
			command="INSERT INTO pref(PrefName,ValueString) VALUES('DataBaseVersion','26.1.0.0')";
			Db.NonQ(command);
			command="INSERT INTO pref(PrefName,ValueString) VALUES('ProgramVersion','26.1.0.0')";
			Db.NonQ(command);
			command="INSERT INTO pref(PrefName,ValueString) VALUES('CorruptedDatabase','0')";
			Db.NonQ(command);
			return;
		}
	}
}
