using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueBoxContoller : MonoBehaviour
{
    [SerializeField] GameObject tissue;
    string playerTag = "Player";

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == playerTag && !other.transform.Find(tissue.name))
        {
            GameObject glider = Instantiate(
                tissue,
                new Vector3
                (
                    other.gameObject.transform.position.x,
                    other.gameObject.transform.position.y + 2.5f,
                    other.gameObject.transform.position.z
                ),
                Quaternion.Euler(Vector3.zero)
            );
            glider.name = "Tissue";
            glider.transform.parent = other.gameObject.transform;
        }
    }
}