using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomSlot : MonoBehaviour
{
    private ItemData myItemData;

    private MyItemStatus myItemStatus;

    private DragSlot dragSlot;

    private WeaponManager wm;
    private GameObject instanceDragItemUI;

    [SerializeField]
    private GameObject dragItemUI;

    private GameObject dataName;

    public static GameObject thisCustom;

    [SerializeField]
    private Text informationText;

    private float plusPower;

    private int id;

    private bool panelParam = false;

    public void SetWeaponManager(WeaponManager weaponManager)
    {
        this.wm = weaponManager;
    }

    private void Start()
    {
        // myItemStatus = FindObjectOfType<MyItemStatus>();
    }

    public void InitMyItemStatus(PlayerController myPlayer)
    {
        this.myItemStatus = myPlayer.GetComponent<PlayerController>().GetMyItemStatus();
    }

    public GameObject GetCustomData()
    {
        return thisCustom;
    }

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

        AudioManager.Instance.PlaySE("closing-wooden-door-1");
        dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();
        id = myItemData.GetItemId();

        dataName = dragSlot.GetSlotData();             //ドラッグしてきた持ち物パネルを取得

        switch (panelParam)
        {
            case false:
                if (myItemData.GetItemSet() == PocketStatus.Pocket.none)
                {
                    switch(id)
                    {
                        default:
                        case 0:
                            plusPower = myItemData.GetItemPower();
                            break;
                        case 1:
                            plusPower = myItemData.GetItemSpeed();
                            break;
                        case 2:
                            plusPower = myItemData.GetItemDefence();
                            break;
                    }

                    wm.AttachParts(plusPower, id);

                    ShowInformation();

                    panelParam = true;
                }

                break;

            default:

                dataName = null;

                break;
        }

        if (myItemData.GetItemSet() != PocketStatus.Pocket.none)
        {
            return;
        }

        switch (dragSlot.GetDeleteNum())
        {
            case 1:
                myItemStatus.SetItemCount(id, -1);
                var processingSlot = dataName.GetComponent<ProcessingSlot>();
                processingSlot.StartCoroutine("displayCount");

                if (myItemStatus.GetItemCount(id) <= 0)
                {
                    myItemStatus.SetItemFlag(id, false);
                    processingSlot.PanelDelete();
                }

                break;

            case 2:
                dataName.GetComponent<CustomSlot>().PanelDelete();
                break;

            case 3:
                break;

            default:
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

        AudioManager.Instance.PlaySE("cancel2");


        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData, gameObject, 2);

        thisCustom = gameObject;

        // Player.atk -= plusPower;
        // wm.AttachParts(-plusPower, id);
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
        wm.AttachParts(-plusPower, id);
    }
}