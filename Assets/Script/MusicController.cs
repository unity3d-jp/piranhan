using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	
	void Awake ()
	{
		if (GameObject.FindObjectsOfType (typeof(MusicController)).Length > 1)
			Destroy (gameObject);
	}
	
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}

}
