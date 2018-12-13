using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    DustManager dustManager;

    GameObject[] dust = new GameObject[2];

    void Start()
    {
        // dust = GameObject.FindWithTag("Dust");
        dust = GameObject.FindGameObjectsWithTag("Dust");
        for (int i = 0; i < dust.Length; i++)
        {
            dustManager = dust[i].GetComponent<DustManager>();
        }
    }


    void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < dust.Length; i++)
        {
            if (other.gameObject == dust[i])
            {
                gameObject.transform.parent = other.transform;
            }
        }

        if (dustManager == null)
        {
            Destroy(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < dust.Length; i++)
        {
            if (other.gameObject == dust[i])
            {
                gameObject.transform.parent = null;
            }
        }

    }
}
