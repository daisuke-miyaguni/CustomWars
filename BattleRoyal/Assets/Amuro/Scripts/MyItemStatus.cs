using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemStatus : MonoBehaviour
{
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


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item_1" && Input.GetKeyDown(KeyCode.I))
        {
            Parts1TakeUp();
        }

        if (other.gameObject.tag == "Item_2" && Input.GetKeyDown(KeyCode.I))
        {
            Parts2TakeUp();
        }

        if (other.gameObject.tag == "Item_3" && Input.GetKeyDown(KeyCode.I))
        {
            Parts3TakeUp();
        }

        if (other.gameObject.tag == "Item_4" && Input.GetKeyDown(KeyCode.I))
        {
            MonTakeUp();
        }
    }

    void Parts1TakeUp()
    {
        itemFlags[(int)Item.parts1] = true;
    }

    void Parts2TakeUp()
    {
        itemFlags[(int)Item.parts2] = true;
    }

    void Parts3TakeUp()
    {
        itemFlags[(int)Item.parts3] = true;
    }

    void MonTakeUp()
    {
        itemFlags[(int)Item.mon] = true;
    }

    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }
}
