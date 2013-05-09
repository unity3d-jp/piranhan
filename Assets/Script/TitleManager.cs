using UnityEngine;
using System.Collections;

public class TitleManager : MonoSingleton<TitleManager>
{
	
	[SerializeField]
	Flashing flash;

	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			StartCoroutine (GoPrologue ());
			enabled = false;
		}
	}
	
	IEnumerator GoPrologue ()
	{
		GetComponent<PlaySound> ().PlayOneShot ();
		
		flash.enabled = false;
		flash.offTime = 0.1f;
		flash.onTime = 0.1f;

		flash.enabled = true;
		
		yield return new WaitForSeconds(1f);
		
		Application.LoadLevel ("Prologue");
	}
}
