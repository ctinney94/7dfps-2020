using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdateText : MonoBehaviour
{
	public void SetText(float val)
	{
		GetComponent<InputField>().text = "" + val;
	}

	public void SetSliderVal(string val)
	{
		GetComponent<Slider>().value = float.Parse(val);
	}
}
