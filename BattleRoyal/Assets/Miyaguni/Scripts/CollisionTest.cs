using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 動作テストのためのプログラム
public class CollisionTest : MonoBehaviour
{
	[SerializeField] GameObject targetObject;    // ターゲットにするオブジェクト

	[SerializeField] Rigidbody rigidbody;	  // リジッドボディ

	[SerializeField] float power;     // パワー
    [SerializeField] float speed;     // スピード

    [SerializeField] string moveXAxis;
    [SerializeField] string moveZAxis;

	void Update()
	{
		Move();
	}

	void Move()
	{
		// X移動
        float moveX = Input.GetAxis(moveXAxis);
		// Z移動
        float moveZ = Input.GetAxis(moveZAxis) * speed;
		//Player移動
		rigidbody.AddForce(transform.forward * moveZ, ForceMode.Force);
        //rigidbody.AddForce(transform.right * moveX, ForceMode.Force);
		transform.Rotate(0,moveX,0);

        // クリックでターゲットに向かって飛んでいく
        if (Input.GetMouseButtonDown(0))
        {
            transform.LookAt(targetObject.transform);
            rigidbody.AddForce(transform.forward * power, ForceMode.Impulse);
        }
	}
}
