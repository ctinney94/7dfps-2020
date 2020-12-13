using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public class Sproggler : Enemy
	{
		[Header("Visuals")]
		public float scaleProb;
		
		public override void OnSpawn(EnemyManager manager)
		{
			base.OnSpawn(manager);
		}
		public override void OnDeath()
		{
			GameObject _corpse = Instantiate(corpse, transform.position, transform.rotation);
			AudioSource deathSoundSource = _corpse.GetComponent<AudioSource>();
			deathSoundSource.clip = deathSounds[Random.Range(0, deathSounds.Count - 1)];
			deathSoundSource.Play();

			RaycastHit hit;
			if(Physics.Linecast(transform.position, transform.position + Vector3.down * 5, out hit))
				_corpse.transform.localPosition = hit.point;
			base.OnDeath();
		}
		public override void Update()
		{
			base.Update();
			//Move towards the player
			transform.position += transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;

			//Scale self by random amount
			if(Random.value > 1 - scaleProb)
			{
				Vector3 startScale = transform.localScale;
				startScale.y = Random.Range(0.6f, 1.7f);
				transform.localScale = startScale;
			}

		}
	}
}
