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
                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = true;
                callNum = 1;
                CreateSlot(itemDataBase.GetItemData());

                break;

            case MyItemStatus.Item.parts2:
                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = true;
                callNum = 1;
                CreateSlot(itemDataBase.GetItemData());

                break;

            case MyItemStatus.Item.parts3:            
                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = true;
                callNum = 1;
                CreateSlot(itemDataBase.GetItemData());

                break;

            default:
                break;
        }
    }

    


    /* public void DestroySlot()
     {
         System.Diagnostics.StackFrame caller = new System.Diagnostics.StackFrame(1);

         if(caller.GetMethod().Name == "DropItem_1")
         {
             var slot0 = transform.Find("ItemSlot0");


             foreach (Transform item in slot0)
             {
                 item.gameObject.SetActive(false);                
             }

         }

         else if (caller.GetMethod().Name == "DropItem_2")
         {
             var slot1 = transform.Find("ItemSlot1");

             foreach (Transform item in slot1)
             {
                 item.gameObject.SetActive(false);
             }
         }

         else if (caller.GetMethod().Name == "DropItem_3")
         {
             var slot2 = transform.Find("ItemSlot2");

             foreach (Transform item in slot2)
             {
                 item.gameObject.SetActive(false);
             }
         }

     } */
}
