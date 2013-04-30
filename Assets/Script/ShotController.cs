using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {
	
	[SerializeField]
	GameObject bulletPrefab;
	
	public void Shoot()
	{
		if( GameObject.FindGameObjectWithTag("Bullet") != null) 
			return;
		
		GameObject bullet = GameObject.Instantiate( bulletPrefab ) as GameObject;
		bullet.transform.position = transform.position + Vector3.down * 0.1f + Vector3.forward * 0.2f;
		
		CatController cat = GetComponent<CatController>();
		Vector3 direct = cat.Direct;
		string bulletFileName = string.Empty;

		if( direct == Vector3.up)
			bulletFileName = "Texture/bullet-u1";
		else if( direct == Vector3.left )
			bulletFileName = "Texture/bullet-l1";
		else if( direct == Vector3.right )
			bulletFileName = "Texture/bullet-r1";
		
		
		

		bullet.renderer.material.mainTexture = Resources.Load(bulletFileName) as Texture;
		bullet.GetComponent<BulletController>().direct = direct;
	}
}
