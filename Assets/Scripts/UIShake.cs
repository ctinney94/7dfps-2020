using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
	RectTransform rt;
	Vector3 startPos;
	public float shakeAmountX;
	public float shakeAmountY;

	// Start is called before the first frame update
	void Awake()
    {
		rt = GetComponent<RectTransform>();
		startPos = rt.anchoredPosition;
	}

    // Update is called once per frame
    void Update()
    {
		rt.anchoredPosition = startPos + new Vector3(Random.Range(-shakeAmountX, shakeAmountX), Random.Range(-shakeAmountY, shakeAmountY), 0);
    }
}
