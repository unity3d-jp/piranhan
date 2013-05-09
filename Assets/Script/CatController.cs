using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour
{
	
	CatAnimationController catAnimation;
	[SerializeField]
	float speed = 60;
	private Vector3 firstPosition;
	
	void Start ()
	{
		catAnimation = GetCatAnimationController ();
		firstPosition = transform.position;
		catAnimation.LookUp ();
	}
	
	public CatAnimationController GetCatAnimationController ()
	{
		return transform.GetComponentInChildren<CatAnimationController> ();
	}
	
	public void Move (float direction)
	{
		if (direction < 0) 
			catAnimation.MoveLeft ();
		else 
			catAnimation.MoveRight ();
		
		transform.Translate (catAnimation.direction * speed * Time.deltaTime);
	}

	public void LookUp ()
	{
		catAnimation.LookUp ();
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
		if (collision.CompareTag ("Enemy")) {
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
