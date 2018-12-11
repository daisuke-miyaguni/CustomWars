using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketItem : MonoBehaviour
{
    private ItemData myItemData;

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
    }

    public void MouseDrop()                                 //　スロットの上にアイテムがドロップされた時に実行
    {
        if (FindObjectOfType<DragSlot>() == null || panelParam)
        {
            return;
        }

       
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        myItemData = null;

        var dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();

        if (myItemData.GetItemType() == MyItemStatus.Item.mon && panelParam == false
        || myItemData.GetItemType() == MyItemStatus.Item.ball && panelParam == false
        || myItemData.GetItemType() == MyItemStatus.Item.riyo && panelParam == false)
        {
            itemSlot = ProcessingSlot.itemSlot;             //ドラッグしてきた持ち物パネルを取得持ち物
            Debug.Log(itemSlot);
            
            ShowInformation();

            //  myPanel.GetComponent<CustomSlot>().PanelDelete();

            switch (myItemData.GetItemType())               //カスタムパネルに装備
            {


                case MyItemStatus.Item.mon:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.mon] = false;

                    panelParam = true;

                    itemNum = 0;

                    pocketStatus.SetItemData(myItemData, slotNum);

                    Destroy(itemSlot);                      //持ち物欄からパネルを削除
                    break;


                case MyItemStatus.Item.ball:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = false;

                    panelParam = true;

                    itemNum = 1;

                    pocketStatus.SetItemData(myItemData, slotNum);

                    Destroy(itemSlot);

                    break;


                case MyItemStatus.Item.riyo:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = false;

                    panelParam = true;

                    itemNum = 2;

                    pocketStatus.SetItemData(myItemData, slotNum);

                    Destroy(itemSlot);

                    break;


                default:
                    break;
            }

            if(thisPocket != null)
            {
                thisPocket.GetComponent<PocketItem>().PanelDelete();
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

        // myPanel = gameObject;

        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData);

        thisPocket = gameObject;

        panelParam = false;


    }


    public void MouseEndDrag()                                                                   //ドラッグ終了時にアイテム画像を削除
    {
        thisPocket = null;
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
