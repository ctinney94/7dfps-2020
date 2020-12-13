using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
	float aliveTime = 0;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			other.GetComponent<Enemy>().OnDeath();
		}
	}
	
	void Update()
	{
		aliveTime += Time.deltaTime;
		if(aliveTime > 0.5f)
			GetComponent<SphereCollider>().enabled = false;

		if(aliveTime > 5)
			Destroy(gameObject);
	}
}
