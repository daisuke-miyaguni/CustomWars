using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomSlot : MonoBehaviour
{
    private ItemData myItemData;

    private MyItemStatus myItemStatus;

    private GameObject instanceDragItemUI;

    private DragSlot dragSlot;

    private WeaponManager wm;

    [SerializeField]
    private GameObject dragItemUI;

    private GameObject dataName;

    public static GameObject thisCustom;

    private int plusPower;

    public int customNum;

    private bool panelParam = false;

    [SerializeField]
    private Text informationText;

    private void Start()
    {
        myItemStatus = FindObjectOfType<MyItemStatus>();
    }

    public GameObject GetCustomData()
    {
        return thisCustom;
    }

    public void SetItemData(ItemData itemData)      //アイテムデータの取得と、アイテムイメージの表示
    {
        myItemData = itemData;
    }

    public void SetWeaponManager(WeaponManager wm)
    {
        this.wm = wm;
    }

    public void MouseDrop()     //　スロットの上にアイテムがドロップされた時に実行
    {
        if (FindObjectOfType<DragSlot>() == null || panelParam)
        {
            return;
        }

        dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();
        var id = myItemData.GetItemId();

        dataName = dragSlot.GetSlotData();             //ドラッグしてきた持ち物パネルを取得

        switch (customNum)
        {
            case 0:
                if (myItemData.GetItemType() != MyItemStatus.Item.parts1 || panelParam)
                {
                    return;
                }

                myItemStatus.SetItemFlag(id, false);

                plusPower = myItemData.GetItemPower();
                wm.SetWeaponPower(plusPower);

                ShowInformation();

                panelParam = true;

                Destroy(dataName);

                break;

            case 1:
                if (myItemData.GetItemType() != MyItemStatus.Item.parts2 || panelParam)
                {
                    return;
                }

                myItemStatus.SetItemFlag(id, false);

                plusPower = myItemData.GetItemPower();
                wm.SetWeaponPower(plusPower);

                ShowInformation();

                panelParam = true;

                Destroy(dataName);

                break;

            case 2:
                if (myItemData.GetItemType() != MyItemStatus.Item.parts3 || panelParam)
                {
                    return;
                }

                myItemStatus.SetItemFlag(id, false);

                plusPower = myItemData.GetItemPower();
                wm.SetWeaponPower(plusPower);

                ShowInformation();

                panelParam = true;

                Destroy(dataName);

                break;

            default:

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


        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData, gameObject, 2);

        thisCustom = gameObject;
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
        wm.SetWeaponPower(-plusPower);
    }
}