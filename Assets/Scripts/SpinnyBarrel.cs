using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyBarrel : MonoBehaviour
{
	float barrelVelocity;
	public float acceleration;
	public float decceleration;

	// Update is called once per frame
	void Update()
	{
		if (Input.mouseScrollDelta.y != 0)
		{
			barrelVelocity += Input.mouseScrollDelta.y * acceleration;
		}
		if (barrelVelocity > 0)
			barrelVelocity -= decceleration * Time.deltaTime;
		if (barrelVelocity < 0)
			barrelVelocity += decceleration * Time.deltaTime;
		Vector3 barrelEuler = this.transform.localRotation.eulerAngles;
		barrelEuler.z += barrelVelocity;
		this.transform.localRotation = Quaternion.Euler(barrelEuler);
	}
}
