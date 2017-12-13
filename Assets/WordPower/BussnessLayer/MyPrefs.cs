using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;


public class MyPrefs : MonoBehaviour
{
	/// <summary>
	/// Saves the specified Object into xml/playerprefs as string with specified key 
	/// </summary>
	public static void Save<T> (T saveMe, string key, bool isXml = false)
	{
		string data = GetStringFromObj<T> (saveMe);
		if (isXml) {
			string filePath =Application.streamingAssetsPath + "/Resources/Log";
//			if (!File.Exists (filePath)) {
//				File.Create (filePath).Dispose ();
//			}
			TextWriter writer = new StreamWriter (filePath + "/" + key + ".json");
			writer.Write (data);
			writer.Close ();
		} else
			PlayerPrefs.SetString (key, data);
	}

	/// <summary>
	/// Loads the specified Object from xml/playerprefs
	/// </summary>
	public static T Load<T> (string key, bool isXml = false) where T : class
	{
		string data = null;
		if (!isXml) {
			data = PlayerPrefs.GetString (key);
			if (String.IsNullOrEmpty (data)) {
				T targetClass = default(T);
				return targetClass;
			}
		} else {
			TextReader reader = new StreamReader (Application.streamingAssetsPath + "/Resources/Log/" + key + ".json");
			data = reader.ReadToEnd ();
		
		}
		return LoadObjFromString<T> (data);
		
	}
	/// <summary>
	/// Returns the object from string.
	/// </summary>
	public static T LoadObjFromString<T> (string data) where T : class
	{
		if (string.IsNullOrEmpty (data)) {
			T targetClass = default(T);
			return targetClass;
		}
		XmlSerializer s = new XmlSerializer (typeof(T));
		TextReader reader = new StringReader (data);
		return s.Deserialize (reader) as T;
	}
	/// <summary>
	/// Returns the string from object.
	/// </summary>
	public static string GetStringFromObj<T> (T saveMe)
	{
		StringWriter outStream = new StringWriter ();
		XmlSerializer s = new XmlSerializer (typeof(T));
		s.Serialize (outStream, saveMe);
		return outStream.ToString ();
		
	}
		

}
