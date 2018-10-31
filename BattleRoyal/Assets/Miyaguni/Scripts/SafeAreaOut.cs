using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaOut : MonoBehaviour
{
	[SerializeField] string areaTagName;    // 安地範囲のタグ名

	[SerializeField] float deskMoveSpeed;    // 机が寄っていくスピード

	GameObject area;	// 安地のゲームオブジェクト

    void Update()
	{
		Move();
	}

	void Move()
	{
		// 安地が定義されていたら安地から離れる
        if (area != null)
        {
            gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * deskMoveSpeed;
        }

		// 一定の距離離れたら削除
		if(Mathf.Abs(gameObject.transform.position.x) > 150.0f ||
		   Mathf.Abs(gameObject.transform.position.z) > 150.0f)
		{
			Destroy(gameObject);
		}
	}

	// 安地の外に行った処理
    public void SafeAreaExit()
	{
		// 安地を見つける
		area = GameObject.FindWithTag(areaTagName);
		// 安地を向く
		gameObject.transform.LookAt(area.transform);
	}
}