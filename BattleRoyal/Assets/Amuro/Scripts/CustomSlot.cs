using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomSlot : MonoBehaviour
{
    private ItemData myItemData;

    private GameObject instanceDragItemUI;

    private DragSlot dragSlot;

    [SerializeField]
    private GameObject dragItemUI;

    private GameObject dataName;

    public static GameObject thisCustom;

    private int plusPower;

    public int customNum;

    private bool panelParam = false;



    // private GameObject myPanel;


    [SerializeField]
    private Text informationText;

    public void SetItemData(ItemData itemData)      //アイテムデータの取得と、アイテムイメージの表示
    {
        myItemData = itemData;
    }


    public void MouseDrop()     //　スロットの上にアイテムがドロップされた時に実行
    {
        if (FindObjectOfType<DragSlot>() == null || panelParam)
        {
            return;
        }

        dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();

        dataName = ProcessingSlot.itemSlot;             //ドラッグしてきた持ち物パネルを取得

        switch (customNum)
        {
            case 0:
                if (myItemData.GetItemType() != MyItemStatus.Item.parts1 || panelParam)
                {
                    return;
                }

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                    plusPower = myItemData.GetItemPower();
                    Player.atk += plusPower;

                    ShowInformation();

                    panelParam = true;

                    Destroy(dataName);
                   
                break;

            case 1:
                if (myItemData.GetItemType() != MyItemStatus.Item.parts2 || panelParam)
                {
                    return;
                }

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                    plusPower = myItemData.GetItemPower();
                    Player.atk += plusPower;

                    ShowInformation();

                    panelParam = true;

                    Destroy(dataName);
                    
                break;

            case 2:
                if (myItemData.GetItemType() != MyItemStatus.Item.parts3 || panelParam)
                {
                    return;
                }

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                    plusPower = myItemData.GetItemPower();
                    Player.atk += plusPower;

                    ShowInformation();

                    panelParam = true;

                    Destroy(dataName);
                    
                break;

            default:

                Debug.Log("default");

                dataName = null;

                break;
        } 

        dragSlot.DeleteDragItem();                          //　ドラッグしているアイテムデータの削除
    }

    void ShowInformation()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();        //　アイテムイメージを設定

        Text nameUI = GetComponentInChildren<Text>();                                           //　スロットのTextを取得し名前を設定
        nameUI.text = myItemData.GetItemName();

        var text = "<Color=white>" + myItemData.GetItemName() + "</Color>\n";                   //　アイテム情報を表示する
                                                                                                
        informationText.text = text;
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

        thisCustom = gameObject;

        Player.atk -= plusPower;        
    }

    public void MouseEndDrag()                                                                   //ドラッグ終了時にアイテム画像を削除
    {
        thisCustom = null;
        Destroy(instanceDragItemUI);
    }

    public void PanelDelete()
    {
        Debug.Log(gameObject);
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        informationText.text = null;
        myItemData = null;
        panelParam = false;
        // myPanel = null;
    }
}