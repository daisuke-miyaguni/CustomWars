using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsManager : MonoBehaviour
{
	[SerializeField] GameObject footSteps;
	[SerializeField] GameObject player;

	[SerializeField] string playerTag;
    bool footStepsTrigger;

	float time = 0;
	float footStepsTime;

	[SerializeField] float maxFootStepTime;

    void Update()
    {
		if(footStepsTrigger)
		{
            InstantFootSteps();
        }
    }

	void FootStepChecker()
	{
		if(footStepsTime > maxFootStepTime)
		{
			footStepsTrigger = false;
		}
	}

	void InstantFootSteps()
	{
        time += Time.deltaTime;
		FootStepChecker();
		footStepsTime += Time.deltaTime;
        if (time > 0.35f)
        {
            time = 0;
            Instantiate(
				footSteps,
				new Vector3(player.transform.position.x, 0.1f, player.transform.transform.position.z),
				player.transform.rotation
			);
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == playerTag)
		{
			player = other.gameObject;
			footStepsTime = 0.0f;
			footStepsTrigger = true;
		}
	}
}
