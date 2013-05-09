using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class ScoreController : MonoBehaviour
{
	
	TextMesh tmesh;
	
	void Awake ()
	{
		tmesh = GetComponent<TextMesh> ();
	}
	
	void Update ()
	{
		tmesh.text = ScoreManager.instance.Score.ToString ();
	}
}
