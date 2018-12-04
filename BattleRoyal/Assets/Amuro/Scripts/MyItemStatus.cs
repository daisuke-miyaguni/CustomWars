using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private ItemParam param;

    public enum Item
    {
        parts1,
        parts2,
        parts3,
        mon,
        ball,
        riyo
    };

    [SerializeField]
    public static bool[] itemFlags = new bool[6];                   //　アイテムを持っているかどうかのフラグ

    void Start()
    {
        getButton.GetComponent<Button>();
        getButton.SetActive(false);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item" /* && Input.GetKeyDown(KeyCode.I) */)
        {
            param = other.gameObject.GetComponent<ItemParam>();

            getButton.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        param = null;
        getButton.SetActive(false);
        
    }

    public void OnGetButton()
    {
        var type = param.GetItems();

        itemFlags[(int)type] = true;

        getButton.SetActive(false);

        Destroy(param.gameObject);
    }



    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item]; 
    } 
}
