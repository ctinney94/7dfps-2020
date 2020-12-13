using UnityEngine;

public class PauseState : GameState
{
	public override void OnStateActivate()
	{
		Time.timeScale = 0.00001f;
		if(GameStateManager.instance.PauseMenu)
			GameStateManager.instance.PauseMenu.SetActive(true);
		if(GameStateManager.instance.GameplayUI)
			GameStateManager.instance.GameplayUI.SetActive(false);
		menuIndex = 0;
		Q3PlayerMovement.instance.enabled = false;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

	}

	public override void OnStateDeactivate()
	{
		Time.timeScale = 1;
		if(GameStateManager.instance.PauseMenu)
		{
			GameStateManager.instance.PauseMenu.SetActive(false);
		}
		if(GameStateManager.instance.GameplayUI)
			GameStateManager.instance.GameplayUI.SetActive(true);
		Q3PlayerMovement.instance.enabled = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	int menuIndex = 0;

	public override void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
			GameStateManager.instance.ChangeState(GameStateManager.instance.previousState);
	}
}
