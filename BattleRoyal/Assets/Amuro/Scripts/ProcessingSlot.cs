using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingSlot : MonoBehaviour
{
    [SerializeField]
    public ItemData myItemData;             //自身のアイテムデータ

    [SerializeField]
    private MyItemStatus myItemStatus;

    [SerializeField]
    private GameObject titleUI;         //アイテム名を表示するテキストUIプレハブ

    [SerializeField]
    private GameObject dragItemUI;

    private GameObject instanceDragItemUI;  //アイテムをドラッグしたときに生成する画像

    private GameObject itemSlot;      //アイテムスロット格納用

    private Text informationText;       //アイテム情報を表示するテキストUI    

    private Text nameText;

    private Text countText;             //所持数を表示

    private bool countdisplay;


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

        StartCoroutine(displayCount());
    }

    public IEnumerator displayCount()
    {
        myItemStatus = gameObject.transform.root.GetComponent<PlayerUIController>().GetMyItemStatus();
        countText = transform.Find("Count").GetChild(0).GetComponent<Text>();

        if (myItemStatus.GetItemCount(myItemData.GetItemId()) > 1)
        {
            transform.Find("Count").gameObject.SetActive(true);
            countText.text = "×" + myItemStatus.GetItemCount(myItemData.GetItemId()).ToString();
        }
        else if (myItemStatus.GetItemCount(myItemData.GetItemId()) <= 1)
        {
            transform.Find("Count").gameObject.SetActive(false);
        }

        yield break;
    }


    public void MouseBeginDrag()        //アイテムをドラッグしたときの挙動
    {
        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData, gameObject, 1);
    }


    public void MouseEndDrag()          //ドラッグ終了時にアイテム画像を削除
    {
        Destroy(instanceDragItemUI);
    }

    public void PanelDelete()
    {
        Destroy(gameObject);
    }

}
