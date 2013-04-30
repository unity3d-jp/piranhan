using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	GameManager manager;
	TextMesh tmesh;
	
	void Start()
	{
		tmesh = GetComponent<TextMesh>();
		manager = FindObjectOfType(typeof(GameManager))as GameManager;
		
		if( manager == null)
			enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		tmesh.text = string.Format("{0}", manager.score);
		
	}
}
