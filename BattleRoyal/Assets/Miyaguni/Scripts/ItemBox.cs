﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
	[SerializeField] string weaponTagName;    // 武器のタグ名
    [SerializeField] string playerTagName;	  // Playerのタグ名
    [SerializeField] Image openIcon;	// 開くを伝えるアイコン

    [SerializeField] string[] itemName;		// Itemの名前

	BoxCollider bc;		// 宝箱のオープン範囲
	CapsuleCollider cc;		// Icon表示範囲

	void Start()
	{
		bc = GetComponent<BoxCollider>();
		cc = GetComponent<CapsuleCollider>();
	}

	// 宝箱が殴られて開く処理
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == weaponTagName)
		{
			BoxOpen();
		}
	}

	// 開くIconの表示処理
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == playerTagName)
		{
			// 表示する
			openIcon.enabled = true;
			// Playerを向かせる
			openIcon.transform.rotation = Quaternion.LookRotation(other.gameObject.transform.position);
		} 
	}

	// Iconの非表示処理
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == playerTagName)
		{
			// 非表示にする
            openIcon.enabled = false;
        }
	}

	// 箱が開く処理
	void BoxOpen()
	{
		// ランダムに生成する処理
		GameObject item = PhotonNetwork.Instantiate(
			itemName[Random.Range(0, itemName.Length)],
			gameObject.transform.position, 
			gameObject.transform.rotation,
			0
		);
		// 箱の当たり判定削除
		Destroy(bc.GetComponent<BoxCollider>());
		// Iconの表示範囲削除
		Destroy(cc.GetComponent<CapsuleCollider>());
		// Icon削除
		Destroy(openIcon);
		// このプログラムを削除
		Destroy(this);
	}
}
