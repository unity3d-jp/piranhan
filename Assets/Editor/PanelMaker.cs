using UnityEditor;
using UnityEngine;
using System.Collections;

public class PanelMaker : ScriptableObject
{
	public int x = 4, y = 3;
	
	public void  CreatePanel (string path)
	{
		string assetName = string.Format ("Assets/{2}({0},{1}).asset", x, y, path);
		
		if (System.IO.File.Exists (assetName))
			return;
		
		GameObject newGameobject = new GameObject ("CustomPanel");
		
		MeshRenderer meshRenderer = newGameobject.AddComponent<MeshRenderer> ();
		meshRenderer.material = new Material (Shader.Find ("Diffuse"));
			
		MeshFilter meshFilter = newGameobject.AddComponent<MeshFilter> ();
		
		meshFilter.mesh = new Mesh ();
		Mesh mesh = meshFilter.sharedMesh;
		mesh.name = "CustomPanel";
		
		mesh.vertices = new Vector3[]{
			new Vector3 (-0.5f * x, 0.5f * y, 0.0f),
			new Vector3 (0.5f * x, 0.5f * y, 0.0f),
			new Vector3 (0.5f * x, -0.5f * y, 0.0f),
			new Vector3 (-0.5f * x, -0.5f * y, 0.0f)
		};
		mesh.triangles = new int[]{
			0, 1, 2,
			2, 3, 0
		};
		
		mesh.uv = new Vector2[]{
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 0.0f)
		};
		
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
		mesh.Optimize ();
		
		AssetDatabase.CreateAsset (mesh, assetName);
		AssetDatabase.SaveAssets ();
		
		DestroyImmediate (newGameobject);
	}
	
	[ContextMenu("Create Object")]
	public void CreateObject ()
	{
		CreatePanel ("polygon/Panel");
	}

	[ContextMenu("Reset score")]
	public void ResetParam ()
	{
		PlayerPrefs.DeleteAll ();
	}
}

