using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public static int atk　= 10;                  //プレイヤーの攻撃力

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right);
        }

        else
        {
            rb.velocity = Vector3.zero;
        }

        // Debug.Log(atk);

	}
}
