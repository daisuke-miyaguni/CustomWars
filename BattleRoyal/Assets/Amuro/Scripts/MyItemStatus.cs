using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private ItemParam param;
    public GameObject getButton;                //Inve内のgetButtonをアタッチ

    public enum Item
    {
        parts1,
        parts2,
        parts3,
        mon,
        ball,
        riyo
    };

    //　アイテムを持っているかどうかのフラグ
    [SerializeField]
    public static bool[] itemFlags = new bool[6];

    // Use this for initialization
    void Start()
    {
        getButton.GetComponent<Button>();
        getButton.SetActive(false);
    }

    /* private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            param = other.gameObject.GetComponent<ItemParam>();
            var type = param.GetItems();
            Debug.Log(type);
        }
    } */


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item" /* && Input.GetKeyDown(KeyCode.I) */)
        {
            param = other.gameObject.GetComponent<ItemParam>();
            // var type = param.GetItems();
            getButton.SetActive(true);
            
            /*itemFlags[(int)type] = true;

            print(type); */

            //Parts1TakeUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        param = null;
        getButton.SetActive(false);
        
    }

    public void OnClick()
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
