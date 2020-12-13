using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWobble : MonoBehaviour
{
	public CharacterController PlayerCharacterController;
	private Vector3 WeaponBobLocalPosition;
	private Vector3 LastCharacterPosition;
	private float WeaponBobFactor;

	[Header("Weapon Bob")]
	[Tooltip("Frequency at which the weapon will move around in the screen when the player is in movement")]
	public float bobFrequency = 10f;
	[Tooltip("How fast the weapon bob is applied, the bigger value the fastest")]
	public float bobSharpness = 10f;
	[Tooltip("Distance the weapon bobs when not aiming")]
	public float BobAmount = 0.05f;

	void LateUpdate()
	{
		if (Time.deltaTime > 0f)
		{
			Vector3 playerCharacterVelocity = (PlayerCharacterController.transform.position - LastCharacterPosition) / Time.deltaTime;

			// calculate a smoothed weapon bob amount based on how close to our max grounded movement velocity we are
			float characterMovementFactor = 0f;
			if (PlayerCharacterController.isGrounded)
			{
				characterMovementFactor = Mathf.Clamp01(playerCharacterVelocity.magnitude);
			}
			WeaponBobFactor = Mathf.Lerp(WeaponBobFactor, characterMovementFactor, bobSharpness * Time.deltaTime);

			// Calculate vertical and horizontal weapon bob values based on a sine function
			float frequency = bobFrequency;
			float hBobValue = Mathf.Sin(Time.time * frequency) * BobAmount * WeaponBobFactor;
			float vBobValue = ((Mathf.Sin(Time.time * frequency * 2f) * 0.5f) + 0.5f) * BobAmount * WeaponBobFactor;

			// Apply weapon bob
			WeaponBobLocalPosition.x = hBobValue;
			WeaponBobLocalPosition.y = Mathf.Abs(vBobValue);

			LastCharacterPosition = PlayerCharacterController.transform.position;
		}


		this.transform.localPosition = WeaponBobLocalPosition;
	}
}
