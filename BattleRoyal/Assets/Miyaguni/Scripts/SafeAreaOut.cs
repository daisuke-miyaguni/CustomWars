using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaOut : MonoBehaviour
{
	[SerializeField] string areaTagName;    // 安地範囲のタグ名

	[SerializeField] float deskMoveSpeed;    // 机が寄っていくスピード

    [SerializeField] float destroyTime = 9.0f;    // 削除カウント時間

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
			destroyTime -= Time.deltaTime;
        }

		// 一定の時間離れたら削除
		if(destroyTime < 0.0f)
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