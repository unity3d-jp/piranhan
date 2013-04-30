using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {
	
	[SerializeField]
	Flashing flash;
	
	void Start()
	{
		MusicController[] musics = FindObjectsOfType(typeof(MusicController)) as MusicController[];
		foreach( MusicController music in musics )
		{
			Destroy ( music.gameObject );
		}

		
		GameManager[] managers = FindObjectsOfType(typeof( GameManager))  as GameManager[];
		
		
		foreach( GameManager mangager in managers )
		{
			Destroy (mangager.gameObject );
		}
	}

	// Update is called once per frame
	void Update () {
		
		if( Input.GetButtonDown("Fire1"))
		{
			StartCoroutine(GoPrologue());
			enabled = false;
		}
	}
	
	IEnumerator GoPrologue()
	{
		//iTween.CameraFadeAdd();
		//iTween.CameraFadeTo(1, 1);
		
		GetComponent<PlaySound>().PlayOneShot();
		
		flash.enabled = false;
		yield return null;
		flash.offTime = 0.1f;
		flash.onTime = 0.1f;

		flash.enabled = true;
		
		yield return new WaitForSeconds(1f);
		
		Application.LoadLevel("Prologue");
	}
}
