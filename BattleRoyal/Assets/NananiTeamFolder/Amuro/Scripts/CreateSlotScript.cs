using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlotScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

    private GameObject customObject;

    private Sprite itemSprite;

    [SerializeField]
    private MyItemStatus myItemStatus;

    [SerializeField]
    private ItemDataBase itemDataBase;

    private ItemData myItemData;

    private int callNum;

    public void SetMyItemStatus(MyItemStatus myPlayer)
    {
        this.myItemStatus = myPlayer;
    }

    void OnEnable()
    {
        callNum = 0;
        CreateSlot(itemDataBase.GetItemData());
    }


    public void CreateSlot(ItemData[] itemLists)
    {
        switch (callNum)
        {
            case 0:

                foreach (var item in itemLists)
                {
                    if (myItemStatus.GetItemFlag(item.GetItemType()))
                    {
                        var instanceSlot = Instantiate(slot) as GameObject;

                        instanceSlot.name = item.GetItemName();

                        instanceSlot.transform.SetParent(transform);

                        instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);

                        instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
                    }
                }

                break;

            case 1:
                if (myItemStatus.GetItemCount(myItemData.GetItemId()) <= 1)
                {
                    var instanceSlot = Instantiate(slot) as GameObject;

                    instanceSlot.name = myItemData.GetItemName();

                    instanceSlot.transform.SetParent(transform);

                    instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);

                    var display = instanceSlot.GetComponent<ProcessingSlot>();

                    display.SetItemData(myItemData);

                }

                var itemSlot = transform.Find(myItemData.GetItemName()).GetComponent<ProcessingSlot>();

                StartCoroutine(itemSlot.displayCount());

                break;

            default:

                break;
        }
    }

    public void MouseDrop()                                 //item_panelでアイテムをもったままドラッグ終了したら呼び出す
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            //print("ok");
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();
        var id = myItemData.GetItemId();

        if (dragSlot.GetSlotData() != null)
        {
            customObject = dragSlot.GetSlotData();
        }

        switch (myItemData.GetItemType())                   //取得したアイテムのパネルを表示
        {

            case MyItemStatus.Item.parts1:
                callNum = 1;

                if (myItemStatus.GetItemFlag(MyItemStatus.Item.parts1) == false)
                {
                    myItemStatus.SetItemFlag(id, true);
                }
                break;

            case MyItemStatus.Item.parts2:
                callNum = 1;

                if (myItemStatus.GetItemFlag(MyItemStatus.Item.parts2) == false)
                {
                    myItemStatus.SetItemFlag(id, true);
                }
                break;

            case MyItemStatus.Item.parts3:
                callNum = 1;

                if (myItemStatus.GetItemFlag(MyItemStatus.Item.parts3) == false)
                {
                    myItemStatus.SetItemFlag(id, true);
                }
                break;

            case MyItemStatus.Item.mon:
                callNum = 1;

                if (myItemStatus.GetItemFlag(MyItemStatus.Item.mon) == false)
                {
                    myItemStatus.SetItemFlag(id, true);
                }
                break;

            case MyItemStatus.Item.ball:
                callNum = 1;

                if (myItemStatus.GetItemFlag(MyItemStatus.Item.ball) == false)
                {
                    myItemStatus.SetItemFlag(id, true);
                }
                break;

            case MyItemStatus.Item.riyo:
                callNum = 1;

                if (myItemStatus.GetItemFlag(MyItemStatus.Item.riyo) == false)
                {
                    myItemStatus.SetItemFlag(id, true);
                }
                break;

            default:
                break;
        }

        switch (dragSlot.GetDeleteNum())
        {
            case 1:
                break;

            case 2:
                myItemStatus.SetItemCount(id, 1);
                CreateSlot(itemDataBase.GetItemData());
                customObject.GetComponent<CustomSlot>().PanelDelete();
                break;

            case 3:
                myItemStatus.SetItemCount(id, 1);
                CreateSlot(itemDataBase.GetItemData());
                customObject.GetComponent<PocketItem>().PanelDelete();
                break;
        }
    }
}
