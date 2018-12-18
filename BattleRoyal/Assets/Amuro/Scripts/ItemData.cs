using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : Object
{
    private int itemId;                     //アイテムID
    private Sprite itemSprite;              //アイテム画像
    private string itemName;                //アイテム名 
    private MyItemStatus.Item itemType;     //アイテムタイプ
    private string itemInformation;         //アイテム説明
    private float itemPower;                  //アイテム攻撃力
    private float itemSpeed;                  //アイテム攻撃速度
    private float itemDef;                    //アイテム防御力
    private PocketStatus.Pocket pocketItem;   //アイテムセットフラグ

    public ItemData(int id, Sprite image, string Name, MyItemStatus.Item itemtype, string Information, float Power, float Speed, float Defence, PocketStatus.Pocket pocketitem)
    {
        itemId = id;
        itemSprite = image;
        itemName = Name;
        itemType = itemtype;
        itemInformation = Information;
        itemPower = Power;
        itemSpeed = Speed;
        itemDef = Defence;
        pocketItem = pocketitem;
    }

    public int GetItemId()
    {
        return itemId;
    }
    public Sprite GetItemSprite()
    {
        return itemSprite;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public MyItemStatus.Item GetItemType()
    {
        return itemType;
    }

    public string GetItemInformation()
    {
        return itemInformation;
    }

    public float GetItemPower()
    {
        return itemPower;
    }

    public float GetItemSpeed()
    {
        return itemSpeed;
    }

    public float GetItemDefence()
    {
        return itemDef;
    }

    public PocketStatus.Pocket GetItemSet()
    {
        return pocketItem;
    }

}
