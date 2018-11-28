﻿using System.Collections;
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

        Vector3 p_pos = GameObject.Find("Sphere").transform.position;



        switch (myItemData.GetItemType())
        {
            case MyItemStatus.Item.parts1:

                 MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                 GameObject parts1 = (GameObject)Resources.Load("parts1");                  //捨てたアイテムをプレイヤーポジションに生成する
                 Instantiate(parts1, p_pos, Quaternion.identity);
                 
                 Destroy(slotName);
                 break;

            case MyItemStatus.Item.parts2:

                 MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                GameObject parts2 = (GameObject)Resources.Load("parts2");
                Instantiate(parts2, p_pos, Quaternion.identity);
                
                Destroy(slotName);

                break;

            case MyItemStatus.Item.parts3:

                 MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                GameObject parts3 = (GameObject)Resources.Load("parts3");
                Instantiate(parts3, p_pos, Quaternion.identity);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.mon:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.mon] = false;

                GameObject mon = (GameObject)Resources.Load("mon");
                Instantiate(mon, p_pos, Quaternion.identity);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.ball:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = false;

                GameObject show = (GameObject)Resources.Load("show");
                Instantiate(show, p_pos, Quaternion.identity);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.riyo:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = false;

                GameObject riyo = (GameObject)Resources.Load("riyo");
                Instantiate(riyo, p_pos, Quaternion.identity);

                Destroy(slotName);

                break;

            default:
                 break;
        }

    }
    
}
