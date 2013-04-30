using UnityEngine;
using System.Collections;

public class RandomSpone : MonoBehaviour
{
	[SerializeField] GameManager manager;
	[SerializeField] GameObject fishPrefab;
	
	public float sponeParcent = 2;
	
	public int sponeCount = 0;

	[SerializeField] Transform left = null, right = null;
	
	private GameObject dustBox = null;

	void OnDrawGizmosSelected ()
	{
		if( left == null || right == null)
			return;
		
		Gizmos.DrawLine (left.position, right.position);
	}

	void OnEnable()
	{
		dustBox = GameObject.Find("dustbox") as GameObject;
		if( dustBox == null)
			dustBox = new GameObject("dustbox");

		StartCoroutine(SponeLoop());
		animation.enabled = true;
	}
	
	void OnDisable()
	{
		StopAllCoroutines();
		animation.enabled = false;
		dustBox = null;
	}
	
	IEnumerator SponeLoop ()
	{
		while (true) {
			Spone ();
			yield return new WaitForSeconds(sponeParcent);
		}
	}
	
	
	public void Spone ()
	{
		// spone limit
		if( manager.clearCount <= sponeCount  )
			return;
		
		sponeCount ++;
		
		if( dustBox == null)
			dustBox = new GameObject("dustbox");
		
		float position = Random.Range (left.transform.position.x, right.transform.position.x);
		
		GameObject fish = GameObject.Instantiate (fishPrefab) as GameObject;
		fish.transform.position = transform.position + Vector3.left * position + Vector3.forward * 20;
		fish.transform.parent = dustBox.transform;
		
	}
	
}
