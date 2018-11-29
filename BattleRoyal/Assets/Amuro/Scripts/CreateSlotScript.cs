using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlotScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

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

                        instanceSlot.name = "ItemSlot";

                        instanceSlot.transform.SetParent(transform);

                        instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);

                        instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
                    }
                }

                break;

            case 1:
                {
                    var instanceSlot = Instantiate(slot) as GameObject;

                    instanceSlot.name = "ItemSlot";

                    instanceSlot.transform.SetParent(transform);

                    instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);

                    instanceSlot.GetComponent<ProcessingSlot>().SetItemData(myItemData);

                    break;
                }                

            default:

                break;

        }
       
        
    }

    public void MouseDrop()                                 //item_panelでアイテムをもったままドラッグ終了したら呼び出す
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            print("ok");
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();


        switch (myItemData.GetItemType())
        {
            
            case MyItemStatus.Item.parts1:
                if (MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] == false)
                {
                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = true;
                    callNum = 1;
                    CreateSlot(itemDataBase.GetItemData());
                }

                break;

            case MyItemStatus.Item.parts2:
                if(MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] == false)
                {
                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = true;
                callNum = 1;
                CreateSlot(itemDataBase.GetItemData());
                }
                
                break;

            case MyItemStatus.Item.parts3:
                if(MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] == false)
                {
                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = true;
                callNum = 1;
                CreateSlot(itemDataBase.GetItemData());
                }

                break;

            case MyItemStatus.Item.mon:
                if (MyItemStatus.itemFlags[(int)MyItemStatus.Item.mon] == false)
                {
                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.mon] = true;
                    callNum = 1;
                    CreateSlot(itemDataBase.GetItemData());
                }

                break;

            case MyItemStatus.Item.ball:
                if (MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] == false)
                {
                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = true;
                    callNum = 1;
                    CreateSlot(itemDataBase.GetItemData());
                }

                break;

            case MyItemStatus.Item.riyo:
                if (MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] == false)
                {
                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = true;
                    callNum = 1;
                    CreateSlot(itemDataBase.GetItemData());
                }

                break;

            default:
                break;
        }
    }
}
