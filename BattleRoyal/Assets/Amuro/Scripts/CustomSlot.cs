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

        var dragSlot = FindObjectOfType<DragSlot>();       //　DragItemUIに設定しているDragItemDataスクリプトからアイテムデータを取得
        myItemData = dragSlot.GetItem();

        if (myItemData.GetItemType() == MyItemStatus.Item.parts1  && dataName != null
        || myItemData.GetItemType() == MyItemStatus.Item.parts2  && dataName != null
        || myItemData.GetItemType() == MyItemStatus.Item.parts3  && dataName != null) 
        {
            dataName = ProcessingSlot.itemSlot;
            ShowInformation();

            switch (myItemData.GetItemType())
            {


                case MyItemStatus.Item.parts1:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                    plusPower = myItemData.GetItemPower();
                    Player.atk += plusPower;                                            //Player.atkをプレイヤーの攻撃力に当たる変数に変えて使用

                    Destroy(dataName);
                    break;


                case MyItemStatus.Item.parts2:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                    plusPower = myItemData.GetItemPower();
                    Player.atk += plusPower;

                    Destroy(dataName);

                    break;


                case MyItemStatus.Item.parts3:

                    MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                    plusPower = myItemData.GetItemPower();
                    Player.atk += plusPower;

                    Destroy(dataName);

                    break;


                default:
                    break;
            }
        }
        
        dragSlot.DeleteDragItem();                          //　ドラッグしているアイテムデータの削除

        print(dataName);



        
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

        instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;
        instanceDragItemUI.transform.SetParent(transform.parent.parent);
        instanceDragItemUI.GetComponent<DragSlot>().SetDragItem(myItemData);

        Player.atk -= plusPower;                                                                //パーツを外したときに追加された分だけの攻撃力を減少させる

        transform.GetChild(0).GetComponent<Image>().sprite = null;
        informationText.text = null;
        myItemData = null;
        
    }

    public void MouseEndDrag()                                                                   //ドラッグ終了時にアイテム画像を削除
    {
        Destroy(instanceDragItemUI);
    }
}
