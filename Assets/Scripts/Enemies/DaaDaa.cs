using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public class DaaDaa : Winger
	{
		public float shotProb;

		public override void Update()
		{
			base.Update();
			if(Random.value > 1 - shotProb)
				ProjectileManager.instance.FireProjectile(transform.position, transform.rotation, false);
		}
	}
}