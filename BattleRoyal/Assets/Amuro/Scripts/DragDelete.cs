using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    private GameObject slotName;

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
       


        switch (myItemData.GetItemType())
        {
            case MyItemStatus.Item.parts1:

                 MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                 GameObject parts1 = (GameObject)Resources.Load("parts1");                  //捨てたアイテムをプレイヤーポジションに生成する
                 Vector3 p_pos = GameObject.Find("Sphere").transform.position;
                 Instantiate(parts1, p_pos, Quaternion.identity);
                 
                 Destroy(slotName);
                 break;

            case MyItemStatus.Item.parts2:

                 MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                GameObject parts2 = (GameObject)Resources.Load("parts2");
                p_pos = GameObject.Find("Sphere").transform.position;
                Instantiate(parts2, p_pos, Quaternion.identity);
                
                Destroy(slotName);

                break;

            case MyItemStatus.Item.parts3:

                 MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                GameObject parts3 = (GameObject)Resources.Load("parts3");
                p_pos = GameObject.Find("Sphere").transform.position;
                Instantiate(parts3, p_pos, Quaternion.identity);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.mon:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                GameObject mon = (GameObject)Resources.Load("mon");
                p_pos = GameObject.Find("Sphere").transform.position;
                Instantiate(mon, p_pos, Quaternion.identity);

                Destroy(slotName);

                break;

            default:
                 break;
        }

    }
    
}
