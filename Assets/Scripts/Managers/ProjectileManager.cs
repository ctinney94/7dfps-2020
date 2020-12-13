using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
	public static ProjectileManager instance { get; private set; } = null;

	List<Projectile> ActiveProjectiles = new List<Projectile>();
	List<Projectile> InactiveProjectiles = new List<Projectile>();

	public GameObject defaultProjectilePrefab;

	void Awake()
	{
		instance = this;
	}

	public void FireProjectile(Vector3 projStartPos, Quaternion projRotation, bool playerOwned)
	{
		Projectile proj = getProjectile();
		proj.transform.position = projStartPos;
		proj.transform.rotation = projRotation;
		proj.playerOwned = playerOwned;

	}

	Projectile getProjectile()
	{
		Projectile returnMe;
		if(InactiveProjectiles.Count > 0)
		{
			returnMe = InactiveProjectiles[0];
			InactiveProjectiles.Remove(returnMe);
		}
		else
		{
			GameObject newProj = Instantiate(defaultProjectilePrefab, transform);
			returnMe = newProj.GetComponent<Projectile>();
		}
		ActiveProjectiles.Add(returnMe);
		returnMe.gameObject.SetActive(true);
		return returnMe;
	}

	public void ReturnProjectileToPool(Projectile proj)
	{
		InactiveProjectiles.Add(proj);
		proj.gameObject.SetActive(false);
		ActiveProjectiles.Remove(proj);
	}
}
