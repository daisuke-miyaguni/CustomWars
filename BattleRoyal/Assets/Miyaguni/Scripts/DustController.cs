using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
	DustManager dustManager;

    GameObject dust;

    void Start()
    {
        dust = GameObject.Find("Dust");
        dustManager = dust.GetComponent<DustManager>();
    }


    void OnTriggerStay(Collider other)
    {
    	if(other.gameObject == dust)
    	{
            gameObject.transform.parent = other.transform;
        }

        if(dustManager == null)
        {
            Destroy(this);
        }
    }

	void OnTriggerExit(Collider other)
	{
        if (other.gameObject == dust)
        {
            gameObject.transform.parent = null;
        }
	}
}
