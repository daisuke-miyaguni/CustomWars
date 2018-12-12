using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 動作テストのためのプログラム
public class CollisionTest : MonoBehaviour
{
	[SerializeField] GameObject targetObject;    // ターゲットにするオブジェクト

	Rigidbody rb;     // 重力

	[SerializeField] float power;     // パワー
    [SerializeField] float speed;     // スピード

    [SerializeField] string moveXAxis;
    [SerializeField] string moveZAxis;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Move();
	}

	void Move()
	{
		// X移動
        float moveX = Input.GetAxis(moveXAxis) * speed;
		// Z移動
        float moveZ = Input.GetAxis(moveZAxis) * speed;
		//Player移動
		// rb.AddForce(transform.forward * moveZ, ForceMode.Force);
        // rb.AddForce(transform.right * moveX, ForceMode.Force);
		rb.velocity = new Vector3(moveX, 0, moveZ);

        // クリックでターゲットに向かって飛んでいく
        if (Input.GetMouseButtonDown(0))
        {
			if(targetObject != null)
			{
            	transform.LookAt(targetObject.transform);
            	rb.AddForce(transform.forward * power, ForceMode.Impulse);
			}
        }
	}
}
