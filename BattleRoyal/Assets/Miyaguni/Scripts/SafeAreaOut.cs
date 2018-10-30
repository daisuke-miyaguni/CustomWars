using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaOut : MonoBehaviour
{
	[SerializeField] string areaTagName;

	[SerializeField] float deskMoveSpeed;

	GameObject area;

    void Update()
	{
		Move();
	}

	void Move()
	{
        if (area != null)
        {
            gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * deskMoveSpeed;
        }

		if(Mathf.Abs(gameObject.transform.position.x) > 150.0f ||
		   Mathf.Abs(gameObject.transform.position.z) > 150.0f)
		{
			Destroy(gameObject);
		}
	}

    public void SafeAreaExit()
	{
		area = GameObject.FindWithTag(areaTagName);
		gameObject.transform.LookAt(area.transform);
	}
}