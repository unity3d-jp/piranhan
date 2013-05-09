using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour
{
	
	CatAnimationController catAnimation;
	[SerializeField]
	float speed = 60;
	[HideInInspector]
	public Vector3 direction;
	private Vector3 firstPosition;
	
	void Start ()
	{
		catAnimation = transform.GetComponentInChildren<CatAnimationController> ();
		firstPosition = transform.position;
		direction = Vector3.up;
	}
	
	public void MoveLeft ()
	{
		direction = Vector3.left;
		transform.position += direction * speed * Time.deltaTime;
		catAnimation.leftWalk.enabled = true;
	}
	
	public void MoveRight ()
	{
		direction = Vector3.right;
		transform.position += direction * speed * Time.deltaTime;
		catAnimation.rightWalk.enabled = true;
	}
	
	public void MoveUp ()
	{
		catAnimation.upWalk.enabled = true;
		direction = Vector3.up;
	}

	public void Reset ()
	{
		transform.position = firstPosition;
		catAnimation.transform.localPosition = Vector3.zero;
		
		GetComponent<Controller> ().enabled = true;
		catAnimation.upWalk.enabled = true;
		
		direction = Vector3.up;
		
		StartCoroutine (catAnimation.Flashing ());
	}
	
	void OnTriggerEnter (Collider collision)
	{
		if (collision.gameObject.tag.Equals ("Enemy")) {
			GameManager.Miss ();
			catAnimation.failed.enabled = true;
			collider.enabled = false;
			animation.Play ("MissAnimation@Cat");
			GetComponent<Controller> ().enabled = false;
		}
	}
	
	void Dead ()
	{
		GameObject manager = GameObject.FindWithTag ("GameManager") as GameObject;
		StartCoroutine (manager.GetComponent<GameManager> ().GameOver ());
	}
}
