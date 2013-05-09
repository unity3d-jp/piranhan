using UnityEngine;
using System.Collections;

public class QuickSpeed : MonoBehaviour
{
	
	[SerializeField]
	SpriteAnimationController spriteAnimation;
	
	void SeedSet (float speed)
	{
		spriteAnimation.enabled = false;
		spriteAnimation.time = speed;
		spriteAnimation.enabled = true;
	}
}
