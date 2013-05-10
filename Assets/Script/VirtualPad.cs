using UnityEngine;
using System.Collections;

public class VirtualPad : MonoBehaviour {
	[HideInInspector]
	public bool left;
	[HideInInspector]
	public bool right;
	[HideInInspector]
	public bool up;
	[HideInInspector]
	public bool trigger;

	public Vector2 dpadCenter;
	public float dpadRadius;

	public Vector2 triggerCenter;
	public float triggerRadius;

	Vector2 scDPadCenter;
	float scDPadRadius;

	Vector2 scTriggerCenter;
	float scTriggerRadius;

	void ClearInput() {
		left = right = up = trigger = false;
	}

	void UpdateParametersWithScreenSize() {
		float scaler = Screen.width;
		scDPadCenter = dpadCenter * scaler;
		scDPadRadius = dpadRadius * scaler;
		scTriggerCenter = triggerCenter * scaler;
		scTriggerRadius = triggerRadius * scaler;
	}

	void Start() {
		UpdateParametersWithScreenSize();
	}

	void Update() {
		ClearInput();
#if (UNITY_IPHONE || UNITY_IPHONE) && !UNITY_EDITOR
		foreach (var touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended) {
				TestTouch(touch.position);
			}
		}
#else
		if (Input.GetMouseButton(0)) TestTouch(Input.mousePosition);
#endif
	}

	void TestTouch(Vector2 position) {
		if ((position - scTriggerCenter).magnitude < scTriggerRadius) {
			trigger = true;
			return;
		}

		var r = position - scDPadCenter;
		if (r.magnitude < scDPadRadius) {
			if (r.x > r.y || -r.x > r.y || r.y < 0) {
				if (r.x > 0) {
					right = true;
				} else {
					left = true;
				}
			} else {
				up = true;
			}
		}
	}

	void OnDrawGizmos() {
		Vector2 ux = new Vector2(1, 0);
		Vector2 uy = new Vector2(0, 1);
		
		UpdateParametersWithScreenSize();

		DrawGizmoLine(scDPadCenter, scDPadCenter + (uy + ux) * scDPadRadius);
		DrawGizmoLine(scDPadCenter, scDPadCenter + (uy - ux) * scDPadRadius);
		DrawGizmoLine(scDPadCenter, scDPadCenter - uy * scDPadRadius);

		DrawGizmoLine(scTriggerCenter - ux * scTriggerRadius, scTriggerCenter + ux * scTriggerRadius);
		DrawGizmoLine(scTriggerCenter - uy * scTriggerRadius, scTriggerCenter + uy * scTriggerRadius);
	}

	void DrawGizmoLine(Vector2 p1, Vector2 p2) {
		Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(p1.x, p1.y, 1)),
		                Camera.main.ScreenToWorldPoint(new Vector3(p2.x, p2.y, 1)));
	}
}
