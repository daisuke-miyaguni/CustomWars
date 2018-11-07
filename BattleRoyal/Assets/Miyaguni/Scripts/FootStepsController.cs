using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsController : MonoBehaviour
{
    [SerializeField] int stepTime = 90;

    void Start()
    {
        StartCoroutine(Disappearing());
    }

    IEnumerator Disappearing()
    {
        for (int i = 0; i < stepTime; i++)
        {
            GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1 - 1.0f * i / stepTime);
            yield return null;
        }
        Destroy(gameObject);
	}
}