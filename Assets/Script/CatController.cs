using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {
	
	[SerializeField]
	float speed = 60;
	
	[SerializeField]
	SpriteAnimationController leftWalk, rightWalk, upWalk , failed;
	
	[SerializeField]
	Flashing flash;
	
	[HideInInspector]
	public Vector3 Direct;
	
	private Vector3 firstPosition;
	
	void Start()
	{
		firstPosition = transform.position;
		Direct = Vector3.up;
	}
	
	public void MoveLeft()
	{
		Direct = Vector3.left;
		transform.localPosition += Direct * speed * Time.deltaTime;
		leftWalk.enabled = true;
	}
	
	public void MoveRight()
	{
		Direct = Vector3.right;
		transform.localPosition += Direct * speed * Time.deltaTime;
		rightWalk.enabled = true;
	}
	
	public void MoveUp()
	{
		upWalk.enabled = true;
		Direct = Vector3.up;
	}
	
	
	public void Reset()
	{
		transform.position = firstPosition;
		transform.GetChild(0).transform.localPosition = Vector3.zero;
		
		GetComponent<Controller>().enabled = true;
		upWalk.enabled = true;
		
		Direct = Vector3.up;
		
		StartCoroutine(Flashing());
	}
	
	
	IEnumerator Flashing()
	{
		flash.enabled = true;

		yield return new WaitForSeconds(1.5f);

		flash.enabled = false;
		
		collider.enabled = true;
	}
	
	void OnTriggerEnter( Collider collision )
	{
		if( collision.gameObject.tag.Equals("Enemy"))
		{
			GameManager.Miss();
			failed.enabled = true;
			collider.enabled = false;
			animation.Play("MissAnimation@Cat");
			GetComponent<Controller>().enabled = false;
		}
	}
	
	void Dead()
	{
		GameObject manager = GameObject.FindWithTag("GameManager") as GameObject;
		StartCoroutine( manager.GetComponent<GameManager>().GameOver());
	}

}
