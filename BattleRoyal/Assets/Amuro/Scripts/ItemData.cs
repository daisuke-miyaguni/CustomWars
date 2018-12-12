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
    private int itemPower;                  //アイテム攻撃力
    private PocketStatus.Pocket pocketItem;   //アイテムセットフラグ

    public ItemData(int id, Sprite image, string Name, MyItemStatus.Item itemtype, string Information, int Power, PocketStatus.Pocket pocketitem)
    {
        itemId = id;
        itemSprite = image;
        itemName = Name;
        itemType = itemtype;
        itemInformation = Information;
        itemPower = Power;
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

    public int GetItemPower()
    {
        return itemPower;
    }

    public PocketStatus.Pocket GetItemSet()
    {
        return pocketItem;
    }

}
