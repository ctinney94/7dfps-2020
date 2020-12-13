using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
	public static PlayerCollision instance;

	public float hp, regen;
	public Slider healthSlider1, healthSlider2;

	private void Awake()
	{
		instance = this;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			hp -= 20.0f;
		}
	}

	public void IncreaseHP(float amount)
	{
		hp += amount;
	}

	void Update()
	{
		if(GameStateManager.instance.GetState() != GameStateManager.GameStates.STATE_GAMEPLAY)
			return;

		hp = Mathf.Clamp(hp + (Time.deltaTime * regen), 0, 100);
		healthSlider1.value = 100 - hp;
		healthSlider2.value = 100 - hp;
		if(hp == 0)
			GameStateManager.instance.ChangeState(GameStateManager.GameStates.STATE_GAMEOVER);
	}
}
