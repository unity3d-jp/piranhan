using UnityEngine;
using System.Collections;

public class HighScoreController : MonoBehaviour
{
	
	TextMesh text;
	
	// Use this for initialization
	void Start ()
	{
		text = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		text.text = PlayerPrefs.GetInt ("highscore", 0).ToString ();
	}
}
