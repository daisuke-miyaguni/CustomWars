using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProcessingSlot  : MonoBehaviour
{
    private Text informationText;       //アイテム情報を表示するテキストUI

    [SerializeField]
    private GameObject titleUI;         //アイテム名を表示するテキストUIプレハブ

    private Text nameText;

    [SerializeField]
    private MyItemStatus myItemStatus;

    [SerializeField]
    public ItemData myItemData;             //自身のアイテムデータ

    [SerializeField]
    private GameObject dragItemUI;

    private GameObject instanceDragItemUI;  //アイテムをドラッグしたときに生成する画像

    public static GameObject itemSlot;      //アイテムスロット格納用


    void OnDisable()                        //スロットが非アクティブになったら削除
    {
        Destroy(gameObject);
    }


    public void SetItemData(ItemData itemData)      //アイテムデータの取得と、アイテムイメージの表示
    {
        myItemData = itemData;
        transform.GetChild(2).GetComponent<Image>().sprite = myItemData.GetItemSprite();
    }


    void Start()    //アイテム名とアイテム説明の取得、表示
    {
        nameText = transform.Find("Name").GetChild(0).GetComponent<Text>();
        informationText = transform.Find("Information").GetChild(0).GetComponent<Text>();

        nameText.text = myItemData.GetItemName();
        informationText.text = myItemData.GetItemInformation();

    }


    public void MouseBeginDrag()        //アイテムをドラッグしたときの挙動
    {
        itemSlot = null;

        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform/*.parent.parent*/);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData);

        itemSlot = gameObject;       
    }


    public void MouseEndDrag()          //ドラッグ終了時にアイテム画像を削除
    {
        Destroy(instanceDragItemUI);
    }

}
