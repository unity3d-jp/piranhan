using UnityEngine;
using System.Collections;

public class HighScoreController : MonoBehaviour
{
	
	TextMesh text;
	
	// Use this for initialization
	void Start ()
	{
		text = GetComponent<TextMesh> ();
		ScoreManager.instance.HighScore = PlayerPrefs.GetInt ("highscore");
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = ScoreManager.instance.HighScore.ToString ();
	}
}
