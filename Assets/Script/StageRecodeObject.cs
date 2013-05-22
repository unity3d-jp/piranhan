using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public class StageRecodeObject : ScriptableObject
{
	public StageRecord[] stageRecodes = null;
	
	
	[System.Serializable]
	public class StageRecord
	{
		public float secWait;
		public int character;
		public float x;
		public float y;
	}
#if UNITY_EDITOR	
	[ContextMenu("to xml")]
	public void ToXML ()
	{
		XmlSerializer serializer = new XmlSerializer (typeof(StageRecord[]));
		
		string path = Path.GetDirectoryName( UnityEditor.AssetDatabase.GetAssetPath(this)) + "/" + name + ".xml";
		FileStream fstream = new FileStream(path , FileMode.Create );
		
		serializer.Serialize(fstream, stageRecodes);
		fstream.Close();
	}
	
	public void ToObject (Stream stream)
	{
		XmlSerializer serializer = new XmlSerializer(typeof( StageRecord[] ));
		
		stageRecodes = (StageRecord[]) serializer.Deserialize(stream);
		
	}
#endif
}
