using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : Object
{
    private Sprite itemSprite;              //アイテム画像
    private string itemName;                //アイテム名 
    private MyItemStatus.Item itemType;     //アイテムタイプ
    private string itemInformation;         //アイテム説明
    private int itemPower;                  //アイテム攻撃力

    public ItemData(Sprite image, string Name, MyItemStatus.Item itemtype, string Information, int Power)           //アイテムデータの基盤
    {
        itemSprite = image;
        itemName = Name;
        itemType = itemtype;
        itemInformation = Information;
        itemPower = Power;
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
	
}
