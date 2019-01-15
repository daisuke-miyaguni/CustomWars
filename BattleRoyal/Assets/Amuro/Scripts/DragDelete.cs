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

    // private PhotonView playerPV;

    private void Start()
    {
        // myItemStatus = FindObjectOfType<MyItemStatus>();
        itemSpawner = GameObject.FindWithTag("ItemSpawner").gameObject.GetComponent<ItemSpawner>();
    }

    public void SetMyPlayer(GameObject myPlayer)
    {
        this.myPlayer = myPlayer;
        this.myItemStatus = myPlayer.GetComponent<MyItemStatus>();
        // playerPV = myPlayer.GetComponent<PhotonView>();
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

        // Vector3 p_pos = new Vector3
        // (
        //     myPlayer.transform.position.x,
        //     myPlayer.transform.position.y + 1.0f,
        //     myPlayer.transform.position.z + 0.8f
        // );

        switch (dragSlot.GetDeleteNum())
        {
            case 1:
                myItemStatus.SetItemCount(id, -1);
                var processingSlot = deleteSlot.GetComponent<ProcessingSlot>();
                processingSlot.StartCoroutine("displayCount");

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

        // ドロップされたアイテムのタイプを取得し、プレイヤーの場所にオブジェクトを生成        
        itemSpawner.CallItemSpawn(this.gameObject, myPlayer.gameObject.transform.position, id);

        dragSlot.DeleteDragItem();
    }
}
