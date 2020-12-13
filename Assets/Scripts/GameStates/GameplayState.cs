using System;
using UnityEngine;

public class GameplayState : GameState
{
	public override void OnStateActivate()
	{
		if(GameStateManager.instance.GameplayUI)
			GameStateManager.instance.GameplayUI.SetActive(true);
	}

	public override void OnStateDeactivate()
	{
		if(GameStateManager.instance.GameplayUI)
			GameStateManager.instance.GameplayUI.SetActive(false);
	}

    public override void Update()
    {
        if (GameStateManager.instance.timerText)
        {
            GameStateManager.instance.timer += Time.deltaTime;
            TimeSpan ts = TimeSpan.FromSeconds(GameStateManager.instance.timer);
            string ms = "" + Mathf.Floor(GameStateManager.instance.timer % 1 * 1000);
            while (ms.Length < 3)
                ms += "0";
            GameStateManager.instance.timerText.text = ts.ToString(@"mm\:ss\:") + ms;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            GameStateManager.instance.ChangeState(GameStateManager.GameStates.STATE_PAUSE);

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(2);
    }
}