using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	
	void Start ()
	{
		if (GameObject.FindObjectsOfType (typeof(MusicController)).Length > 1)
			Destroy (gameObject);
		else
			DontDestroyOnLoad (gameObject);
	}

}
