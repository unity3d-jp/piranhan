using UnityEngine;
using System.Collections;

public class CatAnimationController : MonoBehaviour
{
	
	
	public SpriteAnimationController leftWalk = null;
	public SpriteAnimationController rightWalk = null;
	public SpriteAnimationController upWalk = null;
	public SpriteAnimationController failed = null;
	public Flashing flash;
	
	public void MoveRight ()
	{
		rightWalk.enabled = true;
	}
	
	public void MoveLeft ()
	{
		leftWalk.enabled = true;
	}
	
	public IEnumerator Flashing ()
	{
		flash.enabled = true;

		yield return new WaitForSeconds(1.5f);

		flash.enabled = false;
		
		transform.parent.collider.enabled = true;
	}

	

}
