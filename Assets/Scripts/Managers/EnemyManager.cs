using Assets.Scripts.Enemies;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
	public class EnemyManager : MonoBehaviour
	{
		public GameObject enemyPrefab;
		public int maxEnemies;
		public List<Transform> spawnLocations;
		public float spawnProbabilityPerFrame;

		List<Enemy> activeEnemies = new List<Enemy>();
		List<Enemy> inactiveEnemies = new List<Enemy>();

		void Update()
		{
			if(GameStateManager.instance.GetState() == GameStateManager.GameStates.STATE_GAMEPLAY)
			{
				if(activeEnemies.Count < maxEnemies && UnityEngine.Random.value > 1.0f - spawnProbabilityPerFrame)
				{
					getEnemy();
				}
			}
		}

		Enemy getEnemy()
		{
			Enemy returnMe;
			if(inactiveEnemies.Count > 0)
			{
				returnMe = inactiveEnemies[0];
				inactiveEnemies.Remove(returnMe);
			}
			else
			{
				GameObject newObj = Instantiate(enemyPrefab, transform);
				returnMe = newObj.GetComponent<Enemy>();
			}
			int spawnLocationIndex = UnityEngine.Random.Range(0, spawnLocations.Count);
			returnMe.transform.position = spawnLocations[spawnLocationIndex].position;
			activeEnemies.Add(returnMe);
			returnMe.gameObject.SetActive(true);
			returnMe.OnSpawn(this);
			return returnMe;
		}

		public void RemoveEnemyFromActiveList(Enemy removeMe)
		{
			activeEnemies.Remove(removeMe);
			inactiveEnemies.Add(removeMe);
		}

	}
}
