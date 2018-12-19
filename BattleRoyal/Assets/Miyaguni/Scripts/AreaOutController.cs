using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOutController : MonoBehaviour
{
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Desk")
        {
            Destroy(gameObject);
        }
    }
}