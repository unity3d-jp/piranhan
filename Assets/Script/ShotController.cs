using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CatController))]
public class ShotController : MonoBehaviour
{
	
	[SerializeField]
	GameObject bulletPrefab;
	private CatAnimationController catAnimation;

	void Start ()
	{
		catAnimation = GetComponent<CatController> ().GetCatAnimationController ();
	}
	
	public void Shoot ()
	{
		if (GameObject.FindGameObjectWithTag ("Bullet") != null) 
			return;
		
		GameObject bullet = GameObject.Instantiate (bulletPrefab) as GameObject;
		bullet.transform.position = transform.position + Vector3.down * 0.1f + Vector3.forward * 0.2f;
		
		Vector3 direction = catAnimation.direction;

		if (direction == Vector3.up)
			bullet.transform.Rotate (Vector3.forward * 90);
		else if (direction == Vector3.left)
			bullet.transform.Rotate (Vector3.forward * 180);

		bullet.GetComponent<BulletController> ().direction = direction;
	}
}
