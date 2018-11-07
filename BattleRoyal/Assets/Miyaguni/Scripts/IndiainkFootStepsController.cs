using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndiainkFootStepsController : MonoBehaviour
{
    [SerializeField] int stepTime;

    void Start()
    {
        StartCoroutine(Disappearing());
    }

    IEnumerator Disappearing()
    {
        for (int i = 0; i < stepTime; i++)
        {
            GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1 - 1.0f * i / stepTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}