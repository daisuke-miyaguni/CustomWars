using System.Collections.Generic;
using UnityEngine;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private ItemParam param;

    public enum Item
    {
        parts1,
        parts2,
        parts3,
        mon
    };

    //　アイテムを持っているかどうかのフラグ
    [SerializeField]
    public static bool[] itemFlags = new bool[4];

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            param = other.gameObject.GetComponent<ItemParam>();
            var type = param.GetItems();
            Debug.Log(type);
        }
    }


    private void OnTriggerStay(Collider other)                  //アイテムプレファブのアイテムタイプを取得して、アイテム所持情報に反映させる
    {
        if (other.gameObject.tag == "Item" && Input.GetKeyDown(KeyCode.I))
        {
            param = other.gameObject.GetComponent<ItemParam>();
            var type = param.GetItems();
            itemFlags[(int)type] = true;
        }
    }


    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }
}
