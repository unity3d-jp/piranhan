using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{

	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel ("Title");
		}
	
	}
}
