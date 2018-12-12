using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    private MyItemStatus myItemStatus;

    private GameObject itemSlot;

    private GameObject deleteSlot;

    private GameObject myPlayer;

    private PhotonView playerPV;

    private void Start()
    {
        myItemStatus = FindObjectOfType<MyItemStatus>();
    }
    public void SetMyPlayer(GameObject myPlayer)
    {
        this.myPlayer = myPlayer;
        playerPV = myPlayer.GetComponent<PhotonView>();
    }

    public void DropDragItem()                                                          //捨てるアイテムのアイテムデータ取得
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();                                    //アイテムがドロップされた時に、どのようなアイテムかを取得
        myItemData = dragSlot.GetItem();
        var id = myItemData.GetItemId();

        if (dragSlot.GetSlotData() != null)
        {
            deleteSlot = dragSlot.GetSlotData();
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

                myItemStatus.SetItemFlag(id, false);

                item = (GameObject)Resources.Load("parts1");
                // Instantiate(parts1, p_pos, Quaternion.identity);

                break;

            case MyItemStatus.Item.parts2:

                myItemStatus.SetItemFlag(id, false);

                item = (GameObject)Resources.Load("parts2");
                // Instantiate(parts2, p_pos, Quaternion.identity);

                break;

            case MyItemStatus.Item.parts3:

                myItemStatus.SetItemFlag(id, false);

                item = (GameObject)Resources.Load("parts3");
                // Instantiate(parts3, p_pos, Quaternion.identity);

                break;

            case MyItemStatus.Item.mon:

                myItemStatus.SetItemFlag(id, false);

                GameObject mon = (GameObject)Resources.Load("mon");
                Instantiate(mon, p_pos, Quaternion.identity);

                break;

            case MyItemStatus.Item.ball:

                myItemStatus.SetItemFlag(id, false);

                item = (GameObject)Resources.Load("show");
                // Instantiate(show, p_pos, Quaternion.identity);

                break;

            case MyItemStatus.Item.riyo:

                myItemStatus.SetItemFlag(id, false);

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

        var item = myItemData;

        switch (dragSlot.GetDeleteNum())
        {
            case 1:

                deleteSlot.GetComponent<ProcessingSlot>().PanelDelete();

                break;

            case 2:

                deleteSlot.GetComponent<CustomSlot>().PanelDelete();

                break;

            case 3:

                deleteSlot.GetComponent<PocketItem>().PanelDelete();

                break;

            default:

                break;
        }

        playerPV.RPC("DropItem", PhotonTargets.MasterClient, item, p_pos);

    }

    [PunRPC]
    void DropItem(GameObject drop, Vector3 spawnPos)
    {
        PhotonNetwork.InstantiateSceneObject(drop.name, spawnPos, Quaternion.identity, 0, null);
    }

}
