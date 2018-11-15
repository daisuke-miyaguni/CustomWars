using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParam : MonoBehaviour                      //アイテムプレファブにつけるやつ
{
    private ItemData itemData;

    private ItemDataBase itemDataBase;

    public int itemNum;

    private MyItemStatus.Item items;

	// Use this for initialization
	void Start ()
    {
        switch (itemNum)                                    //インスペクターで数字を入れてアイテムタイプを取得
        {
            case 0:
                items = MyItemStatus.Item.parts1;
                print(items);
                break;

            case 1:
                items = MyItemStatus.Item.parts2;
                print(items);
                break;

            case 2:
                items = MyItemStatus.Item.parts3;
                print(items);
                break;

            case 3:
                items = MyItemStatus.Item.mon;
                print(items);
                break;

            default:
                break;
        }
	}

    public MyItemStatus.Item GetItems()
    {
        return items;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
