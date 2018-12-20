using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaOutObject : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (Time.timeScale != 0)
        if (other.gameObject.tag != "Player")
        {
            Destroy(other.gameObject);

        }
    }
}
