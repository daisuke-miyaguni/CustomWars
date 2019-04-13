using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketStatus : MonoBehaviour
{

    private ItemData itemData;
    private UseItem useItem;

    [SerializeField]
    public enum Pocket
    {
        mon_p = 0,
        ball_p = 1,
        riyo_p = 2,
        none = 3                                                    //セットできないアイテムに当てはめるパラメータ
    };

    [SerializeField]
    private ItemData[] pocketDatas = new ItemData[4];               //use_panelにせっとされているアイテム

    [SerializeField]
    private Transform pocket_item_panel;                            //アイテムを表示させるパネルの親要素のパネルを入れる

    [SerializeField]
    private Transform pocket_panel;                                 //インベントリ内のアイテムスロットを取得

    private void Start()
    {
        pocket_item_panel = transform.Find("pocket_item_panel");
        pocket_panel = transform.Find("Inventory").transform.Find("base_panel").transform.Find("pocket_panel");
    }

    public void SetItemData(ItemData itemData, int slotNum)         //アイテムスロットにアイテムデータをセット
    {
        pocketDatas[slotNum] = itemData;
        pocket_item_panel.GetChild(slotNum).GetChild(0).GetComponent<Image>().sprite = itemData.GetItemSprite();
        useItem = pocket_item_panel.GetChild(slotNum).GetComponent<UseItem>();
        useItem.SetItemSwitch(itemData);
    }

    public void usePocketItem(int panelNum)
    {
        pocket_panel.GetChild(panelNum).GetComponent<PocketItem>().PanelDelete();       //アイテムが使われたら、インベントリ内のアイテム表示を消す
    }


    public void SetItemDelete(int slotNum)
    {
        pocket_item_panel.GetChild(slotNum).GetComponent<UseItem>().PanelDelete();
    }
}
