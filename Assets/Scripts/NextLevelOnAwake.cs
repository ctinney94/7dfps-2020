using UnityEngine;

public class NextLevelOnAwake : MonoBehaviour
{
	public int level;
	
	private void Awake()
	{
		Application.LoadLevel(level);
	}
}
