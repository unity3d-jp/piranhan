using UnityEngine;
using System.Collections;

public class GameOverManager : MonoSingleton<GameOverManager>
{
	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel ("Title");
		}
	
	}
}
