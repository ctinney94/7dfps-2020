using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
	public enum GameStates
	{
		STATE_GAMEPLAY = 0,
		STATE_PAUSE,
		STATE_UPGRADE,
		STATE_GAMEOVER,
		STATE_SPLASH,
		GAMESTATES_COUNT
	}

	public float timer;

	public GameObject PauseMenu, GameplayUI, GameOverUI, GunCam, restartCam, restartUI;

	public Text timerText, timerTextGO;

	public static GameStateManager instance { get; private set; } = null;

	private GameState[] states = new GameState[(int)GameStates.GAMESTATES_COUNT];
	private GameStates currentState = GameStates.STATE_GAMEPLAY;
	public GameStates previousState;

	private void Awake()
	{
		if(instance)
		{
			DestroyImmediate(this);
		}
		else
		{
			instance = this;
			states[(int)GameStates.STATE_GAMEPLAY] = new GameplayState();
			states[(int)GameStates.STATE_PAUSE] = new PauseState();
			states[(int)GameStates.STATE_SPLASH] = new SplashState();
			states[(int)GameStates.STATE_GAMEOVER] = new GameOverState();
		}
	}

	private void Update()
	{
		if(!instance)
		{
			instance = this;
			states[(int)GameStates.STATE_GAMEPLAY] = new GameplayState();
			states[(int)GameStates.STATE_PAUSE] = new PauseState();
			states[(int)GameStates.STATE_SPLASH] = new SplashState();
			states[(int)GameStates.STATE_GAMEOVER] = new GameOverState();
		}
		else
		{
			states[(int)currentState].Update();
		}
	}

	public void ChangeState(GameStates _state)
	{
		previousState = currentState;
		states[(int)currentState].OnStateDeactivate();
		currentState = _state;
		states[(int)currentState].OnStateActivate();
	}

	public GameStates GetState()
	{
		return currentState;
	}
}
