using UnityEngine;
using System.Collections;

public class CatAnimationController : MonoBehaviour
{
	
	
	public SpriteAnimationController leftWalk = null;
	public SpriteAnimationController rightWalk = null;
	public SpriteAnimationController upWalk = null;
	public SpriteAnimationController failed = null;
	public SpriteAnimationController clearAction = null;
	public Flashing flash;
	[HideInInspector]
	public Vector3 direction;
	
	public void MoveRight ()
	{
		rightWalk.enabled = true;
		direction = Vector3.right;
	}
	
	public void MoveLeft ()
	{
		leftWalk.enabled = true;
		direction = Vector3.left;
	}
	
	public void LookUp ()
	{
		upWalk.enabled = true;
		direction = Vector3.up;
	}
	
	public IEnumerator Flashing ()
	{
		flash.enabled = true;

		yield return new WaitForSeconds(1.5f);

		flash.enabled = false;
		
		transform.parent.collider.enabled = true;
	}

	

}
