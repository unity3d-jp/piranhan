using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;

public class StageRecodeProcessor : AssetPostprocessor
{
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string str in importedAssets) {
			string fileName = Path.GetFileName (str);
			string dirName = Path.GetDirectoryName (str);
			
			if (! Path.GetExtension (fileName).Equals (".xml"))
				continue;
			
			string path = Path.Combine( dirName, Path.GetFileNameWithoutExtension (fileName)) + ".asset";
			
			StageRecodeObject sobj = (StageRecodeObject)AssetDatabase.LoadAssetAtPath (path, typeof(StageRecodeObject));
				
			if (sobj == null) {
				sobj = ScriptableObject.CreateInstance<StageRecodeObject> ();
				AssetDatabase.CreateAsset ((ScriptableObject)sobj, path);
			}
			
			using (FileStream stream = new FileStream(str ,FileMode.Open)) {
				sobj.ToObject (stream);
				stream.Close ();
			}

			// add label
			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (path, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
			
		}
		
		
	}
	
}
