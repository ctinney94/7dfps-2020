using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFiring : MonoBehaviour
{
	public WeaponRecoil recoilClass;
	public Transform gunBarrel;
	public Transform playerCamera;

	public float maxLineTraceRange = 5000.0f;

	// Update is called once per frame
	void Update()
	{
		if(GameStateManager.instance.GetState() != GameStateManager.GameStates.STATE_GAMEPLAY)
			return;

		gunBarrel.rotation = playerCamera.rotation;
		if(Input.GetMouseButton(0) && WeaponManager.instance.CanFireCurrentWeapon())
		{
			recoilClass.FireWeapon();

			WeaponManager.WeaponProfile currentGun = WeaponManager.instance.GetCurrentWeapon();
			CameraShake.instance.Shake(currentGun.shakeAmount);

			if(currentGun.muzzleFlash)
			{
				currentGun.muzzleFlash.sprite = WeaponManager.instance.muzzleFlashSprites[Random.Range(0, WeaponManager.instance.muzzleFlashSprites.Count)];
				currentGun.muzzleFlash.enabled = true;
			}

			if(currentGun.isProjectile)
			{
				ProjectileManager.instance.FireProjectile(gunBarrel.position, Camera.main.transform.rotation, true);
			}
			else
			{
				for(int i = 0; i < currentGun.shots; i++)
				{
					Vector3 forwardVec = playerCamera.rotation * GetRandomInsideCone(currentGun.angleDeviation) * Vector3.forward;
					RaycastHit hit;
					Vector3 start = gunBarrel.position;
					Vector3 end = start + forwardVec * maxLineTraceRange;
					bool isHit = Physics.Linecast(start, end, out hit);
					if(isHit)
					{
						LineManager.instance.ShowLine(start, hit.point);
						if(hit.rigidbody)
						{
							Enemy e = hit.rigidbody.GetComponent<Enemy>();
							if(e)
								e.OnDeath();
							else
								ImpactManager.instance.ShowImpact(hit.point);
						}
						else
						{
							ImpactManager.instance.ShowImpact(hit.point);
						}
					}
					else
					{
						LineManager.instance.ShowLine(start, end);
					}
				}
			}
		}
	}

	Quaternion GetRandomInsideCone(float conicAngle)
	{
		// random tilt right (which is a random angle around the up axis)
		Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(0f, conicAngle), Vector3.up);

		// random spin around the forward axis
		Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);

		// tilt then spin
		return (randomSpin * randomTilt);
	}
}
