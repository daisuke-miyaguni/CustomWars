using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketItem : MonoBehaviour
{
    private ItemData myItemData;

    private MyItemStatus myItemStatus;

    private ProcessingSlot processingSlot;

    private DragSlot dragSlot;

    [SerializeField]
    private PocketStatus pocketStatus;

    private GameObject instanceDragItemUI;

    private GameObject itemSlot;

    [SerializeField]
    private GameObject dragItemUI;

    public static GameObject thisPocket;

    [SerializeField]
    private int slotNum;

    private int itemNum;

    private bool panelParam;                                //パネルにアイテムが入っているかどうか

    [SerializeField]
    private Text informationText;

    private void Start()
    {
        pocketStatus = FindObjectOfType<PocketStatus>();
        // myItemStatus = FindObjectOfType<MyItemStatus>();
    }

    public void InitMyItemStatus(PlayerController myPlayer)
    {
        this.myItemStatus = myPlayer.GetComponent<PlayerController>().GetMyItemStatus();
    }

    public GameObject GetPocketData()
    {
        return thisPocket;
    }
    public void MouseDrop()                                 //　スロットの上にアイテムがドロップされた時に実行
    {
        if (FindObjectOfType<DragSlot>() == null || panelParam)
        {
            return;
        }


        transform.GetChild(0).GetComponent<Image>().sprite = null;
        myItemData = null;

        dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();
        var id = myItemData.GetItemId();

        if (myItemData.GetItemType() == MyItemStatus.Item.mon && panelParam == false
        || myItemData.GetItemType() == MyItemStatus.Item.ball && panelParam == false
        || myItemData.GetItemType() == MyItemStatus.Item.riyo && panelParam == false)
        {
            itemSlot = ProcessingSlot.itemSlot;             //ドラッグしてきた持ち物パネルを取得持ち物
            //Debug.Log(itemSlot);

            ShowInformation();

            switch (myItemData.GetItemType())               //カスタムパネルに装備
            {
                case MyItemStatus.Item.mon:

                    panelParam = true;

                    itemNum = 0;

                    pocketStatus.SetItemData(myItemData, slotNum);

                    break;

                case MyItemStatus.Item.ball:

                    panelParam = true;

                    itemNum = 1;

                    pocketStatus.SetItemData(myItemData, slotNum);

                    break;


                case MyItemStatus.Item.riyo:

                    panelParam = true;

                    itemNum = 2;

                    pocketStatus.SetItemData(myItemData, slotNum);

                    break;

                default:
                    break;
            }

            switch (dragSlot.GetDeleteNum())
            {
                case 1:
                    myItemStatus.SetItemCount(id, 1);
                    var processingSlot = itemSlot.GetComponent<ProcessingSlot>();
                    StartCoroutine(processingSlot.displayCount());

                    if (myItemStatus.GetItemCount(id) <= 0)
                    {
                        myItemStatus.SetItemFlag(id, false);
                        itemSlot.GetComponent<ProcessingSlot>().PanelDelete();
                    }

                    break;

                case 2:

                    break;

                case 3:

                    itemSlot.GetComponent<PocketItem>().PanelDelete();
                    break;
            }

        }
        //dragSlot.DeleteDragItem();                          //　ドラッグしているアイテムデータの削除
        //pocketStatus.SetItemData(myItemData, slotNum);
    }


    void ShowInformation()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();        //　アイテムイメージを設定

    }


    public void MouseBeginDrag()                                                                //パネルをドラッグした際にアイテム画像を生成
    {
        if (!panelParam)
        {
            return;
        }

        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData, gameObject, 3);
    }


    public void MouseEndDrag()                                                                   //ドラッグ終了時にアイテム画像を削除
    {
        Destroy(instanceDragItemUI);
    }


    public void PanelDelete()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        myItemData = null;
        panelParam = false;
        pocketStatus.SetItemDelete(slotNum);
    }


    public int GetItemNum()
    {
        return itemNum;
    }


}
