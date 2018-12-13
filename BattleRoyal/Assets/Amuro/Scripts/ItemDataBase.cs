using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    private ItemData[] itemLists = new ItemData[6];

    void Awake()        //アイテムの作成(アイテム画像、アイテム名、アイテムタイプ、アイテム説明、アイテム攻撃力)
    {
        itemLists[0] = new ItemData(0, Resources.Load("kiyoneko", typeof(Sprite)) as Sprite, "キヨ猫", MyItemStatus.Item.parts1, "猫です。", 10, PocketStatus.Pocket.none);
        itemLists[1] = new ItemData(1, Resources.Load("poke_coin", typeof(Sprite)) as Sprite, "ポケコイン", MyItemStatus.Item.parts2, "コイン", 15, PocketStatus.Pocket.none);
        itemLists[2] = new ItemData(2, Resources.Load("wad", typeof(Sprite)) as Sprite, "ワドルディ", MyItemStatus.Item.parts3, "ワドワド", 20, PocketStatus.Pocket.none);
        itemLists[3] = new ItemData(3, Resources.Load("metamon", typeof(Sprite)) as Sprite, "メタモン", MyItemStatus.Item.mon, "変身", 0, PocketStatus.Pocket.mon_p);
        itemLists[4] = new ItemData(4, Resources.Load("show", typeof(Sprite)) as Sprite, "ボール", MyItemStatus.Item.ball, "ポケボール", 0, PocketStatus.Pocket.ball_p);
        itemLists[5] = new ItemData(5, Resources.Load("riyo_s", typeof(Sprite)) as Sprite, "リヨ", MyItemStatus.Item.riyo, "やばい奴", 0, PocketStatus.Pocket.riyo_p);

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
