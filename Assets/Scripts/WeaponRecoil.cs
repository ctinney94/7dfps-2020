using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
	public void FireWeapon()
	{
		WeaponManager.WeaponProfile gun = WeaponManager.instance.GetCurrentWeapon();
		gun.timer = 0;
		gun.firing = true;
		SoundManager.instance.playSound(gun.sounds[Random.Range(0, gun.sounds.Count - 1)]);
		if(gun.animator)
			gun.animator.Play(gun.animationToPlay);
	}

	void Update()
	{
		WeaponManager.WeaponProfile gun = WeaponManager.instance.GetCurrentWeapon();
		if(gun.firing)
		{
			gun.timer += Time.deltaTime * gun.speed;
			gun.timerUnscaled += Time.deltaTime;
			gun.timer = Mathf.Clamp01(gun.timer);


			//Dirty bullshit for idiots
			if(gun.name == "Minigun")
			{
				WeaponManager.instance.barrelsA.SetActive(gun.timer > .5);
				WeaponManager.instance.barrelsB.SetActive(gun.timer < .5);
			}

			if(gun.muzzleFlash && gun.timerUnscaled > 0.1f)
				gun.muzzleFlash.enabled = false;

			if(gun.firedAngled != 0)
			{
				Vector3 weaponRot = gun.weaponRecoilTransform.localRotation.eulerAngles;
				weaponRot.x = gun.animationCurve.Evaluate(gun.timer) * gun.firedAngled;
				gun.weaponRecoilTransform.localRotation = Quaternion.Euler(weaponRot);
			}

			if(gun.timer >= 1.0f)
			{
				gun.firing = false;
				gun.timer = 0;
				gun.timerUnscaled = 0;
			}
		}
	}
}
