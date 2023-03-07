using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipePanels : MonoBehaviour
{
	private int pageNumber;

	public GameObject[] panels;

	private Vector2 startTouchPosition;
	private Vector2 endTouchPosition;

    void Update()
    {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			startTouchPosition = Input.GetTouch(0).position;


		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			endTouchPosition = Input.GetTouch(0).position;

			if (endTouchPosition.y -100<= startTouchPosition.y+100)
				NextPanel();
			else
				PrivoisPanel();
		}
    }

	private void NextPanel()
	{
		panels[0].SetActive(true);
		panels[1].SetActive(false);
		panels[2].SetActive(false);
		panels[3].SetActive(true);

	}

	private void PrivoisPanel()
	{
		panels[0].SetActive(false);
		panels[1].SetActive(true);
		panels[2].SetActive(true);
		panels[3].SetActive(false);
	}
}
