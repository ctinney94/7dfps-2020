using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public static WeaponManager instance { get; private set; } = null;

	[System.Serializable]
	public class WeaponProfile
	{
		public string name;
		public GameObject weaponGameObject;

		public Transform weaponRecoilTransform;
		public float restAngle;
		public float firedAngled;
		public AnimationCurve animationCurve;
		public float timer;
		public float timerUnscaled;
		public bool firing;
		public float speed;

		public bool isProjectile;

		public List<AudioClip> sounds;
		public float shakeAmount;

		public int shots;
		public float angleDeviation;

		public SpriteRenderer muzzleFlash;

		public Animator animator;
		public string animationToPlay;
	}

	public List<Sprite> muzzleFlashSprites;

	public int activeWeaponIndex;

	public List<WeaponProfile> weaponProfiles;

	[Header("Filthy bullshit")]
	public GameObject barrelsA;
	public GameObject barrelsB;

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
			SetCurrentWeapon(0);
		if(Input.GetKeyDown(KeyCode.Alpha2))
			SetCurrentWeapon(1);
		if(Input.GetKeyDown(KeyCode.Alpha3))
			SetCurrentWeapon(2);
		if(Input.GetKeyDown(KeyCode.Alpha4))
			SetCurrentWeapon(3);
	}

	public void SetCurrentWeapon(int newWeaponIndex)
	{
		if(newWeaponIndex < 0 || newWeaponIndex >= weaponProfiles.Count)
			return;

		weaponProfiles[activeWeaponIndex].weaponGameObject.SetActive(false);
		activeWeaponIndex = newWeaponIndex;
		weaponProfiles[newWeaponIndex].weaponGameObject.SetActive(true);

	}

	public WeaponProfile GetCurrentWeapon()
	{
		return weaponProfiles[activeWeaponIndex];
	}

	public bool CanFireCurrentWeapon()
	{
		return weaponProfiles[activeWeaponIndex].timer == 0;
	}
}
