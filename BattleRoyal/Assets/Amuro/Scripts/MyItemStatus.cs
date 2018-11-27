using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private ItemParam param;
    GameObject getButton;                //Inve内のgetButtonをアタッチ

    // PhotonView photonView;
    PhotonView myItemPV;

    public void SetMyItemPV(PhotonView pv)
    {
        myItemPV = pv;
    }

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
        // myItemPV = gameObject.GetComponent<PhotonView>();
        // getButton.GetComponent<Button>();
        getButton = GameObject.Find("getButton");
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


    private void OnTriggerStay(Collider other)
    {
        // if (myItemPV)
        // {
        if (other.gameObject.tag == "Item" /* && Input.GetKeyDown(KeyCode.I) */)
        {
            param = other.gameObject.GetComponent<ItemParam>();

            // var type = param.GetItems();
            getButton.SetActive(true);

            /*itemFlags[(int)type] = true;

            print(type); */

            //Parts1TakeUp();
            // }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // if (myItemPV)
        // {
        param = null;
        getButton.SetActive(false);
        // }
    }

    public void OnGetButton()
    {
        // if (myItemPV)
        // {
        // myItemPV = GetComponent<PhotonView>();
        myItemPV.RPC("WasgetItem", PhotonTargets.AllViaServer);

        var type = param.GetItems();

        itemFlags[(int)type] = true;


        // Destroy(param.gameObject);
        // }
    }

    [PunRPC]
    void WasgetItem()
    {
        Destroy(param.gameObject);
        if(myItemPV.isMine)
        {
            getButton.SetActive(false);
        }
    }



    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }
}
