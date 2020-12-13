using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
	public static LineManager instance { get; private set; } = null;

	struct LineRendererWithAliveTime
	{
		public LineRenderer lineRenderer;
		public float time;
	}

	public GameObject LineRendererPrefab;
	public float maxLineLifeTime;
	List<LineRendererWithAliveTime> ActiveLineRenderers = new List<LineRendererWithAliveTime>();
	List<LineRenderer> InactiveLineRenderers = new List<LineRenderer>();

	void Awake()
	{
		instance = this;
	}

	public void ShowLine(Vector3 start, Vector3 end)
	{
		LineRenderer newLine = getLineRenderer();
		newLine.startWidth = 0.05f;
		newLine.endWidth = 0.05f;
		newLine.SetPosition(0, start);
		newLine.SetPosition(1, end);
	}

	LineRenderer getLineRenderer()
	{
		LineRenderer returnMe;
		if (InactiveLineRenderers.Count > 0)
		{
			returnMe = InactiveLineRenderers[0];
			InactiveLineRenderers.Remove(returnMe);
		}
		else
		{
			GameObject newLR = Instantiate(LineRendererPrefab, transform);
			returnMe = newLR.GetComponent<LineRenderer>();
		}
		ActiveLineRenderers.Add(
			new LineRendererWithAliveTime()
			{
				lineRenderer = returnMe,
				time = 0.0f
			}
		);
		returnMe.gameObject.SetActive(true);
		return returnMe;
	}

	void Update()
	{
		for (int i=0; i < ActiveLineRenderers.Count; i++)
		{
			LineRendererWithAliveTime thing = ActiveLineRenderers[i];

			thing.time += Time.deltaTime;
			if (thing.time > maxLineLifeTime)
			{
				thing.lineRenderer.gameObject.SetActive(false);
				InactiveLineRenderers.Add(thing.lineRenderer);
				ActiveLineRenderers.RemoveAt(i);
			}
			else
			{
				ActiveLineRenderers[i] = thing;
			}
		}
	}
}
