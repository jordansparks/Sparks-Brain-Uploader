using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace SparksBrainUploader {
	public class DataConnection {
		///<summary>This data adapter is used for all queries to the database.</summary>
		private MySqlDataAdapter mySqlDataAdapter;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private MySqlConnection mySqlConnection;
		///<summary>Used to get very small bits of data from the db when the data adapter would be overkill.  For instance retrieving the response after a command is sent.</summary>
		private MySqlDataReader mySqlDataReader;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		private MySqlCommand mySqlCommand;
		///<summary>After inserting a row, this variable will contain the primary key for the newly inserted row.  This can frequently save an additional query to the database.</summary>
		public int InsertID;
		//public static string ConnectionString;
		public static string User;
		public static string Server;
		public static string Password;
		public static string Database;

		///<summary></summary>
		public DataConnection(){
			string connectionString=
				"Server="+Server+";";
				if(Database!=""){
					connectionString+="Database="+Database+";";
				}
				connectionString+="User ID="+User+";"
				+"Password="+Password+";"
				+"SslMode=none;CharSet=utf8;Treat Tiny As Boolean=false";
			mySqlConnection=new MySqlConnection(connectionString);
			//dr = null;
			mySqlCommand = new MySqlCommand();
			mySqlCommand.Connection=mySqlConnection;
			//table=new DataTable();
		}

		///<summary>Fills table with data from the database.</summary>
		public DataTable GetTable(string command){
			DataTable table=new DataTable();
			mySqlCommand.CommandText=command;
			mySqlDataAdapter=new MySqlDataAdapter(mySqlCommand);
			mySqlDataAdapter.Fill(table);
			mySqlConnection.Close();
 			return table;
		}
		
		///<summary>Fills dataset with data from the database.</summary>
		public DataSet GetDataSet(string commands) {
			DataSet dataSet=new DataSet();
			mySqlCommand.CommandText=commands;
			mySqlDataAdapter=new MySqlDataAdapter(mySqlCommand);
			mySqlDataAdapter.Fill(dataSet);
			mySqlConnection.Close();
			return dataSet;
		}

		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public int NonQ(string commands,bool getInsertID){
			int rowsChanged=0;
			mySqlCommand.CommandText=commands;
			mySqlConnection.Open();
			rowsChanged=mySqlCommand.ExecuteNonQuery();
			if(getInsertID) {
				mySqlCommand.CommandText="SELECT LAST_INSERT_ID()";
				mySqlDataReader=(MySqlDataReader)mySqlCommand.ExecuteReader();
				if(mySqlDataReader.Read())
					InsertID=Convert.ToInt32(mySqlDataReader[0].ToString());
			}
			mySqlConnection.Close();
			return rowsChanged;
		}
		
		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public int NonQ(string command){
			return NonQ(command,false);
		}

		///<summary>Use this for count(*) queries.  They are always guaranteed to return one and only one value.  Uses datareader instead of datatable, so faster.  Can also be used when retrieving prefs manually, since they will also return exactly one value</summary>
		public string GetCount(string command){
			string retVal="";
			mySqlCommand.CommandText=command;
			mySqlConnection.Open();
			mySqlDataReader=(MySqlDataReader)mySqlCommand.ExecuteReader();
			mySqlDataReader.Read();
			retVal=mySqlDataReader[0].ToString();
			mySqlConnection.Close();
			return retVal;
		}

		public bool CanConnect(){
			try{
				mySqlConnection.Open();
			}
			catch{
				return false;
			}
			mySqlConnection.Close();
			return true;
		}
	}
}