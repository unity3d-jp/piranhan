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
	
	void OnEnable ()
	{
		StartCoroutine (ScoreUpdate ());
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	IEnumerator ScoreUpdate ()
	{
		while (true) {
			tmesh.text = string.Format ("{0}", PlayerPrefs.GetInt ("score"), 0);
			yield return new WaitForSeconds(0.3f);
		}
	}
}
