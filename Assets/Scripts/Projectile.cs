using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float moveSpeed;
	public GameObject explosionPrefab;
	public bool playerOwned;
	public List<Sprite> sprites;
	public SpriteRenderer spriteRenderer;

	private void OnTriggerEnter(Collider other)
	{
		bool nearPlayer = Vector3.Distance(Camera.main.transform.position, transform.position) < 15.0f;
		if(playerOwned)
		{
			if(other.gameObject.tag != "Player")
			{
				//spawn explosion
				Instantiate(explosionPrefab, transform.position, transform.rotation);
				ProjectileManager.instance.ReturnProjectileToPool(this);

				if(nearPlayer)
				{
					Vector3 dir = Camera.main.transform.position - transform.position;
					Q3PlayerMovement.instance.AddVelocity(dir.normalized * 25.0f);
				}
			}
		}
		else
		{
			if(other.gameObject.tag != "Enemy")
			{
				ProjectileManager.instance.ReturnProjectileToPool(this);

				if(nearPlayer)
				{
					Vector3 dir = Camera.main.transform.position - transform.position;
					Q3PlayerMovement.instance.AddVelocity(dir.normalized * 20.0f);
					if(!playerOwned)
						PlayerCollision.instance.IncreaseHP(-25.0f);
				}
			}
		}
	}

	void Update()
	{
		if(!playerOwned)
			spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 2)];
		else
			spriteRenderer.sprite = sprites[3];

		Vector3 moveDir = transform.rotation * Vector3.forward * Time.deltaTime * (playerOwned ? 3 : 1);
		transform.position += moveDir * moveSpeed;
	}
}
