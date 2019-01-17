using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketItem : MonoBehaviour
{
    private ItemData myItemData;

    private MyItemStatus myItemStatus;

    // private ProcessingSlot processingSlot;

    private DragSlot dragSlot;

    [SerializeField]
    private PocketStatus pocketStatus;

    private GameObject instanceDragItemUI;

    private GameObject itemSlot;

    [SerializeField]
    private GameObject dragItemUI;

    public static GameObject thisPocket;

    [SerializeField]
    private int slotNum;

    private int itemNum;

    private bool panelParam;                                //パネルにアイテムが入っているかどうか

    [SerializeField]
    private Text informationText;

    private void Start()
    {
        pocketStatus = FindObjectOfType<PocketStatus>();
        // myItemStatus = FindObjectOfType<MyItemStatus>();
    }

    public void InitMyItemStatus(PlayerController myPlayer)
    {
        this.myItemStatus = myPlayer.GetComponent<PlayerController>().GetMyItemStatus();
    }

    public GameObject GetPocketData()
    {
        return thisPocket;
    }
    public void MouseDrop()                                 //　スロットの上にアイテムがドロップされた時に実行
    {
        if (FindObjectOfType<DragSlot>() == null || panelParam)
        {
            return;
        }

        AudioManager.Instance.PlaySE("closing-wooden-door-1");
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        myItemData = null;

        dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();
        int id = myItemData.GetItemId();

        if (id > 2 && panelParam == false)
        {
            itemSlot = dragSlot.GetSlotData();            //ドラッグしてきた持ち物パネルを取得持ち物
            //Debug.Log(itemSlot);

            ShowInformation();

            //カスタムパネルに装備
            panelParam = true;
            pocketStatus.SetItemData(myItemData, slotNum);


            switch (dragSlot.GetDeleteNum())
            {
                case 1:
                    myItemStatus.SetItemCount(id, -1);
                    ProcessingSlot processingSlot = itemSlot.GetComponent<ProcessingSlot>();
                    StartCoroutine(processingSlot.displayCount());

                    if (myItemStatus.GetItemCount(id) <= 0)
                    {
                        myItemStatus.SetItemFlag(id, false);
                        itemSlot.GetComponent<ProcessingSlot>().PanelDelete();
                    }

                    break;

                case 3:

                    itemSlot.GetComponent<PocketItem>().PanelDelete();
                    break;
                default:
                    break;
            }

        }
        dragSlot.DeleteDragItem();                          //　ドラッグしているアイテムデータの削除
        //pocketStatus.SetItemData(myItemData, slotNum);
    }


    void ShowInformation()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();        //　アイテムイメージを設定

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
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData, gameObject, 3);
    }


    public void MouseEndDrag()                                                                   //ドラッグ終了時にアイテム画像を削除
    {
        Destroy(instanceDragItemUI);
    }


    public void PanelDelete()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        myItemData = null;
        panelParam = false;
        pocketStatus.SetItemDelete(slotNum);
    }
}
