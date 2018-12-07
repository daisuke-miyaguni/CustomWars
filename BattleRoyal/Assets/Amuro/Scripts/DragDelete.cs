using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    PhotonView playerPV;

    private GameObject slotName;

    GameObject myPlayer;

    [SerializeField] GameObject[] items;

    public void SetMyPlayer(GameObject player)
    {
        this.myPlayer = player;
        this.playerPV = player.GetComponent<PhotonView>();
    }

    /* private GameObject panel;

    CreateSlotScript create;

    public void OnEnable()
    {
        panel = GameObject.Find("item_panel");

        create = panel.GetComponent<CreateSlotScript>();
    } */


    public void DropDragItem()                                                          //捨てるアイテムのアイテムデータ取得
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();
        myItemData = dragSlot.GetItem();

        slotName = ProcessingSlot.itemSlot;
        // Vector3 p_pos = GameObject.Find("Sphere").transform.position;
        Vector3 p_pos = new Vector3
        (
            myPlayer.transform.position.x,
            myPlayer.transform.position.y + 1.0f,
            myPlayer.transform.position.z + 0.8f
        );

        GameObject item = items[(int)myItemData.GetItemType()];
        MyItemStatus.itemFlags[(int)myItemData.GetItemType()] = false;
        Destroy(slotName);
        // item = (GameObject)Resources.Load(myItemData.GetType().ToString());                  //捨てたアイテムをプレイヤーポジションに生成する

        // switch (myItemData.GetItemType())
        // {
        //     case MyItemStatus.Item.parts1:

        //         MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

        //         item = (GameObject)Resources.Load(items[0].name);                  //捨てたアイテムをプレイヤーポジションに生成する
        //         // PhotonNetwork.InstantiateSceneObject(parts1.name, p_pos, Quaternion.identity,0,null);

        //         Destroy(slotName);
        //         break;

        //     case MyItemStatus.Item.parts2:

        //         MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

        //         item = (GameObject)Resources.Load(items[1].name);
        //         // PhotonNetwork.InstantiateSceneObject(parts2.name, p_pos, Quaternion.identity,0,null);

        //         Destroy(slotName);

        //         break;

        //     case MyItemStatus.Item.parts3:

        //         MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

        //         item = (GameObject)Resources.Load(items[2].name);
        //         // PhotonNetwork.InstantiateSceneObject(parts3.name, p_pos, Quaternion.identity,0,null);

        //         Destroy(slotName);

        //         break;

        //     case MyItemStatus.Item.mon:

        //         MyItemStatus.itemFlags[(int)MyItemStatus.Item.mon] = false;

        //         item = (GameObject)Resources.Load(items[3].name);
        //         // PhotonNetwork.InstantiateSceneObject(mon.name, p_pos, Quaternion.identity,0,null);

        //         Destroy(slotName);

        //         break;

        //     case MyItemStatus.Item.ball:

        //         MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = false;

        //         item = (GameObject)Resources.Load("show");
        //         // PhotonNetwork.InstantiateSceneObject(show.name, p_pos, Quaternion.identity,0,null);

        //         Destroy(slotName);

        //         break;

        //     case MyItemStatus.Item.riyo:

        //         MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = false;

        //         item = (GameObject)Resources.Load("riyo");
        //         // PhotonNetwork.InstantiateSceneObject(riyo.name, p_pos, Quaternion.identity,0,null);

        //         Destroy(slotName);

        //         break;

        //     default:
        //         break;
        // }

        playerPV.RPC("DropItem", PhotonTargets.MasterClient, item, p_pos);

    }

    [PunRPC]
    void DropItem(GameObject drop, Vector3 spawnPos)
    {
        PhotonNetwork.InstantiateSceneObject(drop.name, spawnPos, Quaternion.identity, 0, null);
    }

}
