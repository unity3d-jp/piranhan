using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private CatController controller;
	private ShotController shot;
	private VirtualPad vpad;
	
	void Start ()
	{
		controller = GetComponent<CatController> ();
		shot = GetComponent<ShotController> ();
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
		vpad = FindObjectOfType(typeof(VirtualPad)) as VirtualPad;
#endif
	}
	
	void Update ()
	{
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
		if (vpad.up)
			controller.LookUp ();
		else if (vpad.left)
			controller.Move (-1);
		else if (vpad.right)
			controller.Move (1);
		
		if (vpad.trigger)
			shot.Shoot ();
#else
		if (Input.GetAxis ("Vertical") > 0)
			controller.LookUp ();
		else if (Input.GetAxis ("Horizontal") != 0f)
			controller.Move (Input.GetAxis ("Horizontal"));

		if (Input.GetButtonDown ("Fire1"))
			shot.Shoot ();
#endif

		if (Input.GetKeyDown (KeyCode.LeftShift))
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}
}
