using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
	[SerializeField] string weaponTagName;    // 武器のタグ名
    // [SerializeField] string playerTagName;	  // Playerのタグ名

    // [SerializeField] Image openIcon;	// 開くを伝えるアイコン

    // [SerializeField] string[] itemName;		// Itemの名前
	// [SerializeField] List<GameObject> itemName = new List<GameObject>{};    // Itemの名前
    [SerializeField] GameObject[] itemName;    // Itemの名前


    int itemCount = 3;
    [SerializeField] float itemSpawnPower;
    
    [SerializeField] GameObject icon;

    BoxCollider bc;		// 宝箱のオープン範囲
	// CapsuleCollider cc;		// Icon表示範囲
	PhotonView photonView;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
		bc = GetComponent<BoxCollider>();
		itemCount = 3;
		// cc = GetComponent<CapsuleCollider>();
	}

	// 宝箱が殴られて開く処理
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == weaponTagName)
		{
			photonView.RPC("BoxOpen", PhotonTargets.MasterClient);
			// BoxOpen();
		}
	}

	// // 開くIconの表示処理
	// void OnTriggerStay(Collider other)
	// {
	// 	if(other.gameObject.tag == playerTagName && photonView.isMine)
	// 	{
	// 		// 表示する
	// 		openIcon.enabled = true;
	// 		// Playerを向かせる
	// 		openIcon.transform.rotation = Quaternion.LookRotation(other.gameObject.transform.position);
	// 	} 
	// }

	// // Iconの非表示処理
	// void OnTriggerExit(Collider other)
	// {
	// 	if(other.gameObject.tag == playerTagName && photonView.isMine)
	// 	{
	// 		// 非表示にする
    //         openIcon.enabled = false;
    //     }
	// }

    // 箱が開く処理
    [PunRPC]
	void BoxOpen()
	{
        // ランダムに生成する処理
        // for(int i = 0; i < itemCount; i++)
        // {
        // 	int itemNum = Random.Range(0, itemName.Count);
        //     GameObject item = PhotonNetwork.InstantiateSceneObject
        // 	(
        // 		this.itemName[itemNum].name,
        // 		this.transform.position,
        // 		// new Vector3
        // 		// (
        // 		// 	this.transform.position.x * Random.Range(-0.3f, 0.3f),
        // 		// 	this.transform.position.y * Random.Range(-0.1f, 0.5f),
        // 		// 	this.transform.position.z * Random.Range( 0.0f, 1.0f)
        // 		// ),
        // 		gameObject.transform.rotation,
        // 		0,
        // 		null
        // 	);

        //     Rigidbody itemRb = item.GetComponent<Rigidbody>();
        //     itemRb.AddForce(Random.Range(-itemSpawnPower, itemSpawnPower), itemSpawnPower, Random.Range(0.5f, itemSpawnPower), ForceMode.VelocityChange);
        //     this.itemName.Remove(itemName[itemNum]);
        // }
		GameObject[] randItem = itemName.OrderBy(i => Guid.NewGuid()).ToArray();

        for (int i = 0; i < itemCount; i++)
        {
            GameObject item = PhotonNetwork.InstantiateSceneObject
            (
                randItem[i].name,
                this.transform.position,
                gameObject.transform.rotation,
                0,
                null
            );

            Rigidbody itemRb = item.GetComponent<Rigidbody>();
            itemRb.AddForce(UnityEngine.Random.Range(-itemSpawnPower, itemSpawnPower), itemSpawnPower, UnityEngine.Random.Range(0.5f, itemSpawnPower), ForceMode.VelocityChange);
        }
        photonView.RPC("DeleteItemBox", PhotonTargets.AllViaServer);
    }

	[PunRPC]
	void DeleteItemBox()
	{
		Destroy(this.gameObject);
	}
}
