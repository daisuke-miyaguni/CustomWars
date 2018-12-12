using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public readonly int maxHp = 100;              //最大HP

    public static int hp;                         //HP

    public static int atk　= 10;                  //プレイヤーの攻撃力


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        hp = 40;
        //slider = GameObject.Find("HPBar").GetComponent<Slider>();         //テスト用
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovePlayer();

        HpCtl();
	}

    private void MovePlayer()
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
    }

    private void HpCtl()
    {
        if(hp <= 0)
        {
            hp = 0;
        }
    }

    public void Recovery(int amount)
    {
        hp += amount;

    }
}
