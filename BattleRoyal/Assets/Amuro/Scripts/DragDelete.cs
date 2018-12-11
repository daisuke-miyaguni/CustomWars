using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    private GameObject itemSlot;

    private GameObject deleteSlot;

    public void DropDragItem()                                                          //捨てるアイテムのアイテムデータ取得
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();                                    //アイテムがドロップされた時に、どのようなアイテムかを取得
        myItemData = dragSlot.GetItem();
        
        if(ProcessingSlot.itemSlot != null)
        {
            itemSlot = ProcessingSlot.itemSlot;
        }

        if(CustomSlot.thisCustom != null)
        {
            deleteSlot = CustomSlot.thisCustom;
        }

        if(PocketItem.thisPocket != null)
        {
            deleteSlot = PocketItem.thisPocket;
        }

        Vector3 p_pos = GameObject.Find("Sphere").transform.position;


        switch (myItemData.GetItemType())                                               //ドロップされたアイテムのタイプを取得し、プレイヤーの場所にオブジェクトを生成
        {
            case MyItemStatus.Item.parts1:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                GameObject parts1 = (GameObject)Resources.Load("parts1");
                Instantiate(parts1, p_pos, Quaternion.identity);

                if(deleteSlot != null)
                {
                    deleteSlot.GetComponent<CustomSlot>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.parts2:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                GameObject parts2 = (GameObject)Resources.Load("parts2");
                Instantiate(parts2, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<CustomSlot>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.parts3:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                GameObject parts3 = (GameObject)Resources.Load("parts3");
                Instantiate(parts3, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<CustomSlot>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.ball:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = false;

                GameObject show = (GameObject)Resources.Load("show");
                Instantiate(show, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<PocketItem>().PanelDelete();
                }

                break;

            case MyItemStatus.Item.riyo:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = false;

                GameObject riyo = (GameObject)Resources.Load("riyo");
                Instantiate(riyo, p_pos, Quaternion.identity);

                if (deleteSlot != null)
                {
                    deleteSlot.GetComponent<PocketItem>().PanelDelete();
                }

                break;

            default:
                break;
        }
        
        if(itemSlot != null)                                                            //持ち物欄からアイテムパネルを消去
        {
            itemSlot.GetComponent<ProcessingSlot>().PanelDelete();
        }

    }
    
}
