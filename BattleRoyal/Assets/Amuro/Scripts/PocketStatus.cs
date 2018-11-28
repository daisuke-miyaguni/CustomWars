using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketStatus : MonoBehaviour
{

    private ItemData itemData;
    private Pocket panelData;
    private int itemNum_1;
    private int itemNum_2;

    [SerializeField]
    public enum Pocket
    {
        mon_p,
        ball_p,
        riyo_p,
        none                                                        //セットできないアイテムに当てはめるパラメータ
    };

    [SerializeField]
    private ItemData[] pocketDatas = new ItemData[4];               //use_panelにせっとされているアイテム

    [SerializeField]
    private Transform pocket_item_panel;                            //アイテムを表示させるパネルの親要素のパネルを入れる

    private void Start()
    {
        pocket_item_panel = transform.Find("pocket_item_panel");
    }

    public void SetItemData(ItemData itemData, int slotNum)         //アイテムスロットにアイテムデータをセット
    {
        pocketDatas[slotNum] = itemData;
        pocket_item_panel.GetChild(slotNum).GetChild(0).GetComponent<Image>().sprite = itemData.GetItemSprite();
    }

    

    public Pocket GetPocketData()                                 //アイテムスロットのアイテム情報取得
    {
        return panelData;
    }
}
