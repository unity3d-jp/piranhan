using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		
		
		yield return new WaitForSeconds(3.5f);
		
//		iTween.CameraFadeAdd();
//		iTween.CameraFadeTo(1, 1);

		yield return new WaitForSeconds(1f);

		Application.LoadLevel("Game");
	}
	
	
}
