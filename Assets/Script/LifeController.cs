using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour
{
	
	[SerializeField]
	GameObject[] life;
	GameManager manager;
	
	void Awake ()
	{
		manager = FindObjectOfType (typeof(GameManager)) as GameManager;
	}
	
	void OnEnable ()
	{
		StartCoroutine (LifeCheck ());
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	IEnumerator LifeCheck ()
	{
		while (true) {
			
			for (int i=0; i< life.Length; i++) {
				life [i].renderer.enabled = i < manager.hp;
			}
			
			yield return new WaitForSeconds(0.5f);
		}
	}
}
