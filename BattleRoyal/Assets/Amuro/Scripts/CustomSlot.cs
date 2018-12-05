using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomSlot : MonoBehaviour
{
    private ItemData myItemData;

    private GameObject instanceDragItemUI;

    [SerializeField]
    private GameObject dragItemUI;

    private GameObject dataName;

    private int plusPower;

    private bool panelParam;

    // private GameObject myPanel;

    WeaponManager wm;

    public void SetWeaponManager(WeaponManager weaponManager)
    {
        this.wm = weaponManager;
        // this.wm = weaponManager.GetComponent<WeaponManager>();
    }

    [SerializeField]
    private Text informationText;

    public void SetItemData(ItemData itemData)      //アイテムデータの取得と、アイテムイメージの表示
    {
        myItemData = itemData;
    }


    public void MouseDrop()     //　スロットの上にアイテムがドロップされた時に実行
    {



        if (FindObjectOfType<DragSlot>() == null)
        {
            return;
        }

        if(panelParam != false)
        {
            return;
        }

        transform.GetChild(0).GetComponent<Image>().sprite = null;
        informationText.text = null;
        myItemData = null;

        var dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();

        if (myItemData.GetItemType() == MyItemStatus.Item.parts1 && panelParam == false
        || myItemData.GetItemType() == MyItemStatus.Item.parts2  && panelParam == false
        || myItemData.GetItemType() == MyItemStatus.Item.parts3  && panelParam == false) 
        {
            dataName = ProcessingSlot.itemSlot;             //ドラッグしてきた持ち物パネルを取得持ち物
            ShowInformation();

            //  myPanel.GetComponent<CustomSlot>().PanelDelete();

            switch (myItemData.GetItemType())               //カスタムパネルに装備
            {


                case MyItemStatus.Item.parts1:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                    plusPower = myItemData.GetItemPower();
                    // Player.atk += plusPower;
                    // wm.SetWeaponPower(plusPower);
                    wm.AttachParts(plusPower);

                    panelParam = true;

                    Destroy(dataName);                      //持ち物欄からパネルを削除
                    break;


                case MyItemStatus.Item.parts2:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                    plusPower = myItemData.GetItemPower();
                    // Player.atk += plusPower;
                    // wm.SetWeaponPower(plusPower);
                    wm.AttachParts(plusPower);

                    panelParam = true;

                    Destroy(dataName);

                    break;


                case MyItemStatus.Item.parts3:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                    plusPower = myItemData.GetItemPower();
                    // Player.atk += plusPower;
                    // wm.SetWeaponPower(plusPower);
                    wm.AttachParts(plusPower);

                    panelParam = true;

                    Destroy(dataName);

                    break;


                default:
                    break;
            }
            //Debug.Log(myPanel);

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
        if (transform.GetChild(0).GetComponent<Image>().sprite.name == "Background"             //アイテムが無いパネルをドラッグできないようにした
         || transform.GetChild(0).GetComponent<Image>().sprite.name == "None")
        {
            return;
        }

        // myPanel = gameObject;

        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData);

        // Player.atk -= plusPower;
        wm.AttachParts(-plusPower);
        panelParam = false;

        
    }

    public void MouseEndDrag()                                                                   //ドラッグ終了時にアイテム画像を削除
    {
        Destroy(instanceDragItemUI);
    }

    private void PanelDelete()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        informationText.text = null;
        myItemData = null;
        // myPanel = null;
    }
}
