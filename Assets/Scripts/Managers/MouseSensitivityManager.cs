using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityManager : MonoBehaviour
{
	public Slider MouseXSlider;
	public Slider MouseYSlider;

	// Start is called before the first frame update
	void Start()
	{
		if(!PlayerPrefs.HasKey("MouseSensitivityX"))
		{
			PlayerPrefs.SetInt("MouseSensitivityX", 100);
		}
		if(!PlayerPrefs.HasKey("MouseSensitivityY"))
		{
			PlayerPrefs.SetInt("MouseSensitivityY", 100);
		}

		int x = PlayerPrefs.GetInt("MouseSensitivityX");
		int y = PlayerPrefs.GetInt("MouseSensitivityY");
		MouseXSlider.value = x;
		MouseYSlider.value = y;
	}

	public int GetMouseSensitivityX()
	{
		return PlayerPrefs.GetInt("MouseSensitivityX");
	}
	public void SetMouseSensitivityX(int val)
	{
		PlayerPrefs.SetInt("MouseSensitivityX", val);
	}
	public void SetMouseSensitivityX(float val)
	{
		SetMouseSensitivityX(Mathf.FloorToInt(val));
	}
	public void SetMouseSensitivityX(string val)
	{
		SetMouseSensitivityX(int.Parse(val));
	}

	public int GetMouseSensitivityY()
	{
		return PlayerPrefs.GetInt("MouseSensitivityY");
	}
	public void SetMouseSensitivityY(int val)
	{
		PlayerPrefs.SetInt("MouseSensitivityY", val);
	}
	public void SetMouseSensitivityY(float val)
	{
		SetMouseSensitivityY(Mathf.FloorToInt(val));
	}
	public void SetMouseSensitivityY(string val)
	{
		SetMouseSensitivityY(int.Parse(val));
	}
}
