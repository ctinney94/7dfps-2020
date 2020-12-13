using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickContinue : MonoBehaviour
{
	[System.Serializable]
	public class ListWrapper
	{
		[SerializeField]
		public List<GameObject> items;
	}

	public List<ListWrapper> stages;
	public List<KeyCode> continueKeys;
	int index = 0;

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(continueKeys[index]))
		{
			foreach(GameObject g in stages[index].items)
			{
				if(g)
					g.SetActive(false);
			}
			index++;

			foreach(GameObject g in stages[index].items)
			{
				if(g)
					g.SetActive(true);
			}
		}
		if(Input.GetKeyDown(KeyCode.K))
		{
			Application.LoadLevel(2);
		}
	}
}
