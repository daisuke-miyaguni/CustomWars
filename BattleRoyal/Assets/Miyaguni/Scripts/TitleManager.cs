using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    SceneTransitioner sceneTransitioner;

    void Start()
    {
        sceneTransitioner = gameObject.GetComponent<SceneTransitioner>();
    }

	void Update()
	{
		MoveLobby();
	}

	void MoveLobby()
	{
		if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			sceneTransitioner.MoveScene();
		}
	}
}
