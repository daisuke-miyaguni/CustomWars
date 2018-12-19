using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    private ItemData[] itemLists = new ItemData[6];

    [SerializeField] public float power = 3.0f;
    [SerializeField] public float speed = 1.0f;
    [SerializeField] public float defense = 1.0f;
    [SerializeField] public float recoveryPower = 15.0f;


    void Awake()        //アイテムの作成(アイテム画像、アイテム名、アイテムタイプ、アイテム説明、アイテム攻撃力、アイテム防御力、使用アイテム判別)
    {
        itemLists[0] = new ItemData(0, Resources.Load("Cap",  typeof(Sprite)) as Sprite, "キャップ", MyItemStatus.Item.parts1, "攻撃力UP",   power, 0.0f, 0.0f,   PocketStatus.Pocket.none);
        itemLists[1] = new ItemData(1, Resources.Load("Correction", typeof(Sprite)) as Sprite, "修正液",   MyItemStatus.Item.parts2, "攻撃速度UP", 0.0f, speed, 0.0f,   PocketStatus.Pocket.none);
        itemLists[2] = new ItemData(2, Resources.Load("Ruler",       typeof(Sprite)) as Sprite, "三角定規", MyItemStatus.Item.parts3, "防御UP",     0.0f, 0.0f, defense, PocketStatus.Pocket.none);
        itemLists[3] = new ItemData(3, Resources.Load("metamon",   typeof(Sprite)) as Sprite, "メタモン", MyItemStatus.Item.mon,    "変身",       0.0f, 0.0f, 0.0f,    PocketStatus.Pocket.mon_p);
        itemLists[4] = new ItemData(4, Resources.Load("show",      typeof(Sprite)) as Sprite, "ボール",   MyItemStatus.Item.ball,   "ポケボール", 0.0f, 0.0f, 0.0f,    PocketStatus.Pocket.ball_p);
        itemLists[5] = new ItemData(5, Resources.Load("RecoveryBox",    typeof(Sprite)) as Sprite, "救急箱",   MyItemStatus.Item.riyo,   "回復",       recoveryPower, 0.0f, 0.0f,    PocketStatus.Pocket.riyo_p);

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
