using Flockaroo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : GameState
{
	public override void OnStateActivate()
	{
		GameStateManager gsm = GameStateManager.instance;

		if(gsm.GameOverUI)
			GameStateManager.instance.GameOverUI.SetActive(true);
		if(gsm.timerTextGO)
		{
			TimeSpan ts = TimeSpan.FromSeconds(gsm.timer);
			string ms = "" + Mathf.Floor(gsm.timer % 1 * 1000);
			while(ms.Length < 3)
				ms += "0";
			gsm.timerTextGO.text = ts.ToString(@"mm\:ss\:") + ms;
		}
		gsm.restartCam.SetActive(true);
		gsm.restartUI.SetActive(true);
		GameObject.Find("GunCam").SetActive(false);
		Camera.main.GetComponent<SketchyEffect>().enabled = true;
		Camera.main.transform.localPosition = new Vector3(0, -1, 0);
	}

	public override void OnStateDeactivate()
	{
		if(GameStateManager.instance.GameOverUI)
			GameStateManager.instance.GameOverUI.SetActive(false);

		GameStateManager.instance.GunCam.SetActive(true);
		Camera.main.GetComponent<SketchyEffect>().enabled = false;
		Camera.main.transform.localPosition = new Vector3();
	}

	public override void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(2);
			GameStateManager.instance.ChangeState(GameStateManager.GameStates.STATE_GAMEPLAY);
			//SceneManager.LoadScene(1);
		}
	}
}