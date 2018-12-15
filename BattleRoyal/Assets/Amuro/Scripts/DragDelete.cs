using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    private ItemSpawner itemSpawner;

    private MyItemStatus myItemStatus;

    private GameObject itemSlot;

    private GameObject deleteSlot;

    private GameObject myPlayer;

    private PhotonView playerPV;

    private void Start()
    {
        myItemStatus = FindObjectOfType<MyItemStatus>();
        itemSpawner = GameObject.FindWithTag("ItemSpawner").gameObject.GetComponent<ItemSpawner>();
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

        int itemNum = 0;

        // ドロップされたアイテムのタイプを取得し、プレイヤーの場所にオブジェクトを生成
        switch (myItemData.GetItemType())
        {
            case MyItemStatus.Item.parts1:
                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                }

                // item = (GameObject)Resources.Load("parts1");
                itemNum = 0;

                break;

            case MyItemStatus.Item.parts2:
                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                }

                // item = (GameObject)Resources.Load("parts2");
                itemNum = 1;

                break;

            case MyItemStatus.Item.parts3:
                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);

                }

                // item = (GameObject)Resources.Load("parts3");
                itemNum = 2;

                break;

            case MyItemStatus.Item.mon:
                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                }

                // item = (GameObject)Resources.Load("mon");
                itemNum = 3;

                break;

            case MyItemStatus.Item.ball:
                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                }

                // item = (GameObject)Resources.Load("show");
                itemNum = 4;

                break;

            case MyItemStatus.Item.riyo:
                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                }

                // item = (GameObject)Resources.Load("riyo");
                itemNum = 5;

                break;

            default:
                break;
        }
        
        itemSpawner.CallItemSpawn(this.gameObject, myPlayer.gameObject.transform.position, itemNum);

        var itemData = myItemData;

        switch (dragSlot.GetDeleteNum())
        {
            case 1:
                myItemStatus.SetItemCount(id, -1);

                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                    deleteSlot.GetComponent<ProcessingSlot>().PanelDelete();
                }

                break;

            case 2:
                myItemStatus.SetItemCount(id, 0);
                deleteSlot.GetComponent<CustomSlot>().PanelDelete();

                break;

            case 3:
                myItemStatus.SetItemCount(id, 0);
                deleteSlot.GetComponent<PocketItem>().PanelDelete();

                break;

            default:
                break;
        }
    }
}
