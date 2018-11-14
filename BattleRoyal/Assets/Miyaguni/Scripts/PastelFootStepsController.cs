using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastelFootStepsController : MonoBehaviour
{
    [SerializeField] int stepTime;
	float[] stepColor = new float[3];

    void Start()
    {
        stepColor[0] = Random.Range(0.0f, 1.0f);
        stepColor[1] = Random.Range(0.0f, 1.0f);
        stepColor[2] = Random.Range(0.0f, 0.5f);
        StartCoroutine(Disappearing());
    }

    IEnumerator Disappearing()
    {
        for (int i = 0; i < stepTime; i++)
        {
            GetComponent<MeshRenderer>().material.color = new Color(stepColor[0], stepColor[1], stepColor[2], 1 - 1.0f * i / stepTime);
            yield return null;
        }
        Destroy(gameObject);
	}
}