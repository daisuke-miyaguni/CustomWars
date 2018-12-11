using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    private enum PunRPCList
    {
        BoxOpen,
        DeleteItemBox
    }
	[SerializeField] string weaponTagName;    // 武器のタグ名
    [SerializeField] GameObject[] itemName;    // Itemの名前


    int itemCount = 3;
    [SerializeField] float itemSpawnPower;
    
    [SerializeField] GameObject icon;

    BoxCollider bc;		// 宝箱のオープン範囲
	PhotonView photonView;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
		bc = GetComponent<BoxCollider>();
		itemCount = 3;
	}
    
    // ボタンでボックスを開ける
    public void OpenOnClick()
    {
        photonView.RPC(PunRPCList.BoxOpen.ToString(), PhotonTargets.MasterClient);
    }

    // 箱が開く処理
    [PunRPC]
	void BoxOpen()
	{
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
        photonView.RPC(PunRPCList.DeleteItemBox.ToString(), PhotonTargets.AllViaServer);
    }

	[PunRPC]
	void DeleteItemBox()
	{
		Destroy(this.gameObject);
	}
}
