using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CodeBase;
using DataConnectionBase;

namespace SparksBrainUploader {
	///<summary>Used to send queries</summary>
	public class Db {
		///<summary></summary>
		public static DataTable GetTable(string command) {
			DataConnection dataConnection=new DataConnection();
			DataTable table=dataConnection.GetTable(command);
			return table;
			//DataSet retVal=new DataSet();
			//table.TableName="table";
			//retVal.Tables.Add(table);
			//retVal.Tables[0].TableName="";
			//return retVal;
		}

		///<summary></summary>
		public static int NonQ(string command,bool getInsertID=false) {
			DataConnection dataConnection=new DataConnection();
			int rowsChanged=dataConnection.NonQ(command,getInsertID);
			if(getInsertID){
				return dataConnection.InsertID;
			}
			else{
				return rowsChanged;
			}
		}

		///<summary>This is for multiple queries all concatenated together with ;</summary>
		public static DataSet GetDataSet(string commands){
			DataConnection dataConnection=new DataConnection();
			//DataTable table=dcon.GetTable(command);
			DataSet retVal=dataConnection.GetDataSet(commands);
			//retVal.Tables.Add(table);
			return retVal;
		}

		///<summary>Get count.</summary>
		public static int GetCount(string command) {
			DataConnection dataConnection=new DataConnection();
			return SIn.Int(dataConnection.GetCount(command));
		}

		public static bool CanConnect(){
			DataConnection dataConnection=new DataConnection();
			return dataConnection.CanConnect();
		}
	}
}
