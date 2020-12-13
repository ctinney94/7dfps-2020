using Assets.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public abstract class Enemy : MonoBehaviour
	{
		EnemyManager myManager;


		[Header("Movement")]
		public float maxMoveSpeed;
		public float minMoveSpeed;
		protected float moveSpeed;
		[Header("Sound")]
		public float soundProb;
		public List<AudioClip> sounds;
		public List<AudioClip> deathSounds;
		public AudioSource audioSource;

		[Header("Visuals")]
		public GameObject corpse;
		public List<Sprite> possibleSpritesToUse;
		public SpriteRenderer spriteRenderer;

		public virtual void OnSpawn(EnemyManager manager)
		{
			myManager = manager;
			playRandomSound();
			if(possibleSpritesToUse.Count > 0)
			{
				Sprite useMe = possibleSpritesToUse[Random.Range(0, possibleSpritesToUse.Count - 1)];
				spriteRenderer.sprite = useMe;
			}
			moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
		}
		public virtual void OnDeath()
		{
			audioSource.Stop();
			PlayerCollision.instance.IncreaseHP(15.0f);
			gameObject.SetActive(false);
			myManager.RemoveEnemyFromActiveList(this);
		}

		bool flipped;

		public virtual void Update()
		{
			if(GameStateManager.instance.GetState() == GameStateManager.GameStates.STATE_PAUSE)
				return;

			transform.LookAt(Camera.main.transform);
			//Flip if player is to the right or left
			if(transform.childCount > 0)
			{
				bool isNowFlipped = Camera.main.WorldToScreenPoint(transform.position).x < Screen.width / 2.0f;
				if(isNowFlipped != flipped)
				{
					Vector3 scale = transform.localScale;
					scale.x *= -1;
					transform.localScale = scale;
				}
				flipped = isNowFlipped;
			}

			//Play some horrible sounds
			if(Random.value > 1 - soundProb)
			{
				playRandomSound();
			}
		}

		void playRandomSound()
		{
			audioSource.clip = sounds[Random.Range(0, sounds.Count-1)];
			audioSource.Play();
		}
	}
}
