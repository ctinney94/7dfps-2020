using Assets.Scripts.Enemies;
using UnityEngine;

public class KillPlayerBox : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
			PlayerCollision.instance.IncreaseHP(-100.0f);
		else if(other.gameObject.tag == "Enemy")
			other.GetComponent<Enemy>().OnDeath();
	}
}
