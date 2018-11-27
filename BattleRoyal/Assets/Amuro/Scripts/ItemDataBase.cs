using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    private ItemData[] itemLists = new ItemData[6];

    void Awake()        //アイテムの作成(アイテム画像、アイテム名、アイテムタイプ、アイテム説明、アイテム攻撃力)
    {
        itemLists[0] = new ItemData(Resources.Load("kiyoneko", typeof(Sprite)) as Sprite, "キヨ猫", MyItemStatus.Item.parts1, "猫です。", 10);
        itemLists[1] = new ItemData(Resources.Load("poke_coin", typeof(Sprite)) as Sprite, "ポケコイン", MyItemStatus.Item.parts2, "コイン", 15);
        itemLists[2] = new ItemData(Resources.Load("wad", typeof(Sprite)) as Sprite, "ワドルディ", MyItemStatus.Item.parts3, "ワドワド", 20);
        itemLists[3] = new ItemData(Resources.Load("metamon", typeof(Sprite)) as Sprite, "メタモン", MyItemStatus.Item.mon, "変身", 0);
        itemLists[4] = new ItemData(Resources.Load("show", typeof(Sprite)) as Sprite, "ボール", MyItemStatus.Item.ball, "ポケボール", 0);
        itemLists[5] = new ItemData(Resources.Load("riyo", typeof(Sprite)) as Sprite, "リヨ", MyItemStatus.Item.riyo, "やばい奴", 0);

    }

    public ItemData[] GetItemData()
    {
        return itemLists;
    }

    public int GetItemTotal()
    {
        return itemLists.Length;
    }
}
