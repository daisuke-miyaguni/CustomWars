using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private ItemParam param;
    public GameObject getButton;                //Inve内のgetButtonをアタッチ

    PhotonView photonView;
    PhotonView myItemPV;

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
        myItemPV = GetComponent<PhotonView>();
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


    private void OnTriggerEnter(Collider other)
    {
        if (myItemPV)
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

    }

    private void OnTriggerExit(Collider other)
    {
        if (myItemPV)
        {
            param = null;
            getButton.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (myItemPV)
        {
            var type = param.GetItems();

            itemFlags[(int)type] = true;

            getButton.SetActive(false);
            photonView = param.GetComponent<PhotonView>();

            // Destroy(param.gameObject);
            photonView.RPC("WasgetItem", PhotonTargets.AllViaServer);
        }
    }

    [PunRPC]
    void WasgetItem()
    {
        Destroy(param);
    }



    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }
}
