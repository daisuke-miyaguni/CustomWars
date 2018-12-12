using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    PhotonView playerPV;

    GameObject myPlayer;

    public void SetMyPlayer(GameObject player)
    {
        this.myPlayer = player;
        this.playerPV = player.GetComponent<PhotonView>();
    }

    private GameObject itemSlot;

    private GameObject deleteSlot;

    public void DropDragItem()                                                          //捨てるアイテムのアイテムデータ取得
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();                                    //アイテムがドロップされた時に、どのようなアイテムかを取得
        myItemData = dragSlot.GetItem();

        if (ProcessingSlot.itemSlot != null)
        {
            itemSlot = ProcessingSlot.itemSlot;
        }

        if (CustomSlot.thisCustom != null)
        {
            deleteSlot = CustomSlot.thisCustom;
        }

        if (PocketItem.thisPocket != null)
        {
            deleteSlot = PocketItem.thisPocket;
        }

        // Vector3 p_pos = GameObject.Find("Sphere").transform.position;

        Vector3 p_pos = new Vector3
        (
            myPlayer.transform.position.x,
            myPlayer.transform.position.y + 1.0f,
            myPlayer.transform.position.z + 0.8f
        );

        GameObject item = null;

        // ドロップされたアイテムのタイプを取得し、プレイヤーの場所にオブジェクトを生成
        switch (myItemData.GetItemType())
        {
            case MyItemStatus.Item.parts1:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                item = (GameObject)Resources.Load("parts1");
                // Instantiate(parts1, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<CustomSlot>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.parts2:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                item = (GameObject)Resources.Load("parts2");
                // Instantiate(parts2, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<CustomSlot>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.parts3:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                item = (GameObject)Resources.Load("parts3");
                // Instantiate(parts3, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<CustomSlot>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.ball:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = false;

                item = (GameObject)Resources.Load("show");
                // Instantiate(show, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<PocketItem>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.riyo:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = false;

                item = (GameObject)Resources.Load("riyo");
                // Instantiate(riyo, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<PocketItem>().PanelDelete();
                }

                break;

            default:
                break;
        }

        if (itemSlot != null)                                                            //持ち物欄からアイテムパネルを消去
        {
            itemSlot.GetComponent<ProcessingSlot>().PanelDelete();
        }

        playerPV.RPC("DropItem", PhotonTargets.MasterClient, item, p_pos);

    }

    [PunRPC]
    void DropItem(GameObject drop, Vector3 spawnPos)
    {
        PhotonNetwork.InstantiateSceneObject(drop.name, spawnPos, Quaternion.identity, 0, null);
    }

}
