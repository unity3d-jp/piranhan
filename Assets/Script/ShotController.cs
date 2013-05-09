using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CatController))]
public class ShotController : MonoBehaviour
{
	
	[SerializeField]
	GameObject bulletPrefab;
	private CatController cat;
	
	void Start ()
	{
		cat = GetComponent<CatController> ();
	}
	
	public void Shoot ()
	{
		if (GameObject.FindGameObjectWithTag ("Bullet") != null) 
			return;
		
		GameObject bullet = GameObject.Instantiate (bulletPrefab) as GameObject;
		bullet.transform.position = transform.position + Vector3.down * 0.1f + Vector3.forward * 0.2f;
		
		Vector3 direction = cat.direction;
		string bulletFileName = string.Empty;

		if (direction == Vector3.up)
			bulletFileName = "Texture/bullet-u1";
		else if (direction == Vector3.left)
			bulletFileName = "Texture/bullet-l1";
		else if (direction == Vector3.right)
			bulletFileName = "Texture/bullet-r1";
		

		bullet.renderer.material.mainTexture = Resources.Load (bulletFileName) as Texture;
		bullet.GetComponent<BulletController> ().direction = direction;
	}
}
