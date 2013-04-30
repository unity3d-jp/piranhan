using UnityEngine;
using System.Collections;

public class Flashing : MonoBehaviour {
	
	[Range(0.01f, 1f)]
	public float onTime = 0.7f;
	
	[Range(0.01f, 1f)]
	public float offTime = 0.3f;
	
	void OnEnable()
	{
		StartCoroutine(Flash ());
	}
	
	void OnDisable()
	{
		StopAllCoroutines();
		renderer.enabled = true;
	}
	
	// Use this for initialization
	IEnumerator Flash () {
	
		while(true)
		{
			renderer.enabled = true;
			yield return new WaitForSeconds(onTime);

			renderer.enabled = false;
			yield return new WaitForSeconds(offTime);
		}
	}
	
}

