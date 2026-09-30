using CodeBase;
using DataConnectionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SparksBrainUploader {
	public partial class ConvertDatabases {
		private static Version _latestVersion;
		private static List<ConvertDbMethodInfo> _listConvertDbMethodInfos;

		///<summary>Gets a list of convert databases method infos and their corresponding version information based on their method name.</summary>
		private static List<ConvertDbMethodInfo> ListConvertDbMethodInfos {
			get {
				if(_listConvertDbMethodInfos==null) {
					_listConvertDbMethodInfos=GetAllVersions();
				}
				return _listConvertDbMethodInfos;
			}
		}
		
		///<summary>Returns a version object that correlates to the last convert databases method on file.</summary>
		public static Version LatestVersion {
			get {
				if(_latestVersion==null) {
					if(ListConvertDbMethodInfos.Count==0){
						_latestVersion=new Version(26,1);
					}
					_latestVersion=ListConvertDbMethodInfos[ListConvertDbMethodInfos.Count-1].VersionCur;
				}
				return _latestVersion;
			}
		}

		///<summary>Uses reflection to get all "version" methods from the ConvertDatabasesX classes that match the "ToX_X_X" pattern.
		///Also sorts the methods in the correct order of which they should be invoked.</summary>
		private static List<ConvertDbMethodInfo> GetAllVersions() {
			//Get all the private methods from the ConvertDatabases class via reflection.
			MethodInfo[] methodInfoArray=(typeof(ConvertDatabases)).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
			//Sort the methods so that they are numerically in the order that we require they be invoked in.
			List<ConvertDbMethodInfo> listConvertMethods=new List<ConvertDbMethodInfo>();
			foreach(MethodInfo methodInfo in methodInfoArray) {
				//Make sure that the only methods we add to our list match our ToX_X_X pattern.
				if(!Regex.Match(methodInfo.Name,ConvertDbMethodInfo.PATTERN_METHOD_INFO,RegexOptions.IgnoreCase).Success) {
					continue;//This method does not follow our pattern and is most likely a helper method.
				}
				listConvertMethods.Add(new ConvertDbMethodInfo(methodInfo));
			}
			//Make sure that the list of methods are sorted in ascending order (least to greatest).
			listConvertMethods.Sort((ConvertDbMethodInfo x,ConvertDbMethodInfo y) => { return x.VersionCur.CompareTo(y.VersionCur); });
			return listConvertMethods;
		}

		///<summary>Uses reflection to invoke private methods of the ConvertDatabase class in order from least to greatest if needed.
		///The old way of converting the database was to manually daisy chain methods together.
		///The new way is to just add a method that follows a strict naming pattern which this method will invoke when needed.</summary>
		public static void InvokeConvertMethods() {
			//begins going through the chain of conversion steps via reflection.
			//Loop through the list of convert databases methods from front to back because it has already been sorted (least to greatest).
			for(int i=0;i<ListConvertDbMethodInfos.Count;i++) {
				//Skip all methods that are below or equal to our "from" version.
				if(ListConvertDbMethodInfos[i].VersionCur<=FromVersion) {
					continue;
				}
				//This convert method needs to be invoked.
				ODEvent.Fire(ODEventType.ConvertDatabases,"Upgrading database to version: " 
					+ListConvertDbMethodInfos[i].VersionCur.ToString(2));//Only show the major, minor.
				try {
					//Use reflection to invoke the private static method.
					ListConvertDbMethodInfos[i].MethodInfoCur.Invoke(null,new object[] { });
				}
				catch(Exception ex) {
					string message="Convert Database failed ";
					string methodName=ListConvertDbMethodInfos[i].MethodInfoCur.Name;
					if(!string.IsNullOrEmpty(methodName)) {
						message+="during: "+methodName+"() ";
					}
					throw new Exception(message+"  "+ex.Message+"  "+ex.InnerException.Message,ex.InnerException);
				}
				//Update the preference that keeps track of what version Open Dental has successfully upgraded to.
				//Always require major, minor, build, revision.  Will throw an exception if the revision was not explicitly set (which we always set).
				Prefs.UpdateString(PrefName.VersionDatabase,ListConvertDbMethodInfos[i].VersionCur.ToString(4));
			}
		}
	}

	///<summary>A helper class to quickly manage convert databases methods.  Provides access to the corresponding MethodInfo and Version.</summary>
	public class ConvertDbMethodInfo {
		///<summary>This is the regular expression pattern used to match our convert databases method version pattern of "ToX_X".</summary>
		public const string PATTERN_METHOD_INFO=@"^To([0-9]+)_([0-9]+)$";
		private MethodInfo _methodInfo;
		private Version _version;

		public MethodInfo MethodInfoCur {
			get {
				return _methodInfo;
			}
		}

		public Version VersionCur {
			get {
				return _version;
			}
		}

		///<summary>The method info passed in should have a name that follows the ToX_X_X pattern.
		///Throws an exception if pattern not followed.</summary>
		public ConvertDbMethodInfo(MethodInfo methodInfo) {
			_methodInfo=methodInfo;
			_version=GetVersionFromConvertMethod(methodInfo);
		}

		///<summary>Uses a regular expression to extract a version from the name of the method passed in. The method info passed in should have a name that follows the ToX_X_X pattern. Throws an exception if the method name pattern was not followed.</summary>
		private Version GetVersionFromConvertMethod(MethodInfo methodInfo) {
			Match match=Regex.Match(methodInfo.Name,ConvertDbMethodInfo.PATTERN_METHOD_INFO,RegexOptions.IgnoreCase);
			if(!match.Success) {
				throw new ApplicationException("Invalid convert databases method passed into GetVersionFromConvertMethod.");
			}
			int major=SIn.Int(match.Result("$1"));
			int minor=SIn.Int(match.Result("$2"));
			int build=SIn.Int(match.Result("$3"));
			return new Version(major,minor,build,0);
		}
	}
}
