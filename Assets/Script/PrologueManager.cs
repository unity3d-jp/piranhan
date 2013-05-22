using UnityEngine;
using System.Collections;

public class PrologueManager : MonoSingleton<PrologueManager>
{
	IEnumerator Start ()
	{
		yield return new WaitForSeconds(4.5f);
		Application.LoadLevel ("Game");
	}
	
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel ("Game");
		}
	}
}
