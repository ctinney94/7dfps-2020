using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	public bool FlipSpriteToFaceCamera;

	void Update()
	{
		transform.LookAt(Camera.main.transform);
		Vector3 angle = transform.localRotation.eulerAngles;
		angle.x = 0;
		angle.z = 0;
		transform.localRotation = Quaternion.Euler(angle);
		if (FlipSpriteToFaceCamera)
		{
			GetComponent<SpriteRenderer>().flipX = Camera.main.WorldToScreenPoint(transform.position).x > Screen.width / 2.0f;
		}
	}
}
