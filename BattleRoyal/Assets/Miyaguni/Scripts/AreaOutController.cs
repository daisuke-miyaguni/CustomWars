using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOutController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Area")
        {
            Destroy(this.gameObject);
        }
    }
}