using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : MonoBehaviour
{
	public static CameraShake instance { get; private set; } = null;

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public List<Transform> camTransforms;
	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	void Awake()
	{
		instance = this;
	}

	public void Shake(float amount)
	{
		shakeDuration = .33f;
		shakeAmount = amount;
	}

	void LateUpdate()
	{
		if(GameStateManager.instance.GetState() == GameStateManager.GameStates.STATE_GAMEPLAY)
		{
			if(shakeDuration > 0)
			{
				foreach(Transform t in camTransforms)
				{
					t.localPosition = Random.insideUnitSphere * shakeAmount;
				}
				shakeAmount *= decreaseFactor;
				shakeDuration -= Time.deltaTime;
			}
			else
			{
				foreach(Transform t in camTransforms)
				{
					t.localPosition = new Vector3();
				}
				shakeDuration = 0f;
				//shakeAmount = 0;
			}
		}
	}
}
