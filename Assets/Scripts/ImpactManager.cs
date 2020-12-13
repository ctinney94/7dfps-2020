using System.Collections.Generic;
using UnityEngine;

public class ImpactManager : MonoBehaviour
{
	public static ImpactManager instance { get; private set; } = null;

	struct ParticleSystemWithAliveTime
	{
		public ParticleSystem particleSystem;
		public float time;
	}

	public GameObject particleSystemPrefab;
	public float maxParticleLifeTime;
	List<ParticleSystemWithAliveTime> ActiveParticleSystems = new List<ParticleSystemWithAliveTime>();
	List<ParticleSystem> InactiveParticleSystems = new List<ParticleSystem>();

	void Awake()
	{
		instance = this;
	}

	public void ShowImpact(Vector3 pos)
	{
		ParticleSystem newPS = getParticleSystem();
		newPS.transform.position = pos;
		newPS.Play();

	}

	ParticleSystem getParticleSystem()
	{
		ParticleSystem returnMe;
		if(InactiveParticleSystems.Count > 0)
		{
			returnMe = InactiveParticleSystems[0];
			InactiveParticleSystems.Remove(returnMe);
		}
		else
		{
			GameObject newLR = Instantiate(particleSystemPrefab, transform);
			returnMe = newLR.GetComponent<ParticleSystem>();
		}
		ActiveParticleSystems.Add(
			new ParticleSystemWithAliveTime()
			{
				particleSystem = returnMe,
				time = 0.0f
			}
		);
		returnMe.gameObject.SetActive(true);
		return returnMe;
	}

	void Update()
	{
		for(int i = 0; i < ActiveParticleSystems.Count; i++)
		{
			ParticleSystemWithAliveTime thing = ActiveParticleSystems[i];

			thing.time += Time.deltaTime;
			if(thing.time > maxParticleLifeTime)
			{
				thing.particleSystem.gameObject.SetActive(false);
				InactiveParticleSystems.Add(thing.particleSystem);
				ActiveParticleSystems.RemoveAt(i);
			}
			else
			{
				ActiveParticleSystems[i] = thing;
			}
		}
	}
}
