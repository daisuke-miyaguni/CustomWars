using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private GameObject itemObject;

    private CreateSlotScript createSlot;

    PhotonView myItemPV;

    public GameObject getButton;

    public enum Item
    {
        parts1,
        parts2,
        parts3,
        riyo,
        ball,
        mon
    };

    [SerializeField]
    private bool[] itemFlags = new bool[6];                   //　アイテムを持っているかどうかのフラグ

    [SerializeField]
    private int[] itemCount = new int[6];

    void Awake()
    {
        myItemPV = GetComponent<PhotonView>();
    }
    
    void Start()
    {
        getButton = GameObject.FindWithTag("PlayerControllerUI").gameObject.transform.Find("getButton").gameObject;
        // getButton.GetComponent<Button>();
        if (getButton.activeSelf)
        {
            getButton.SetActive(false);

        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            itemObject = other.gameObject;

            if (myItemPV.isMine)
            {
                getButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            itemObject = null;
            if (myItemPV.isMine)
            {
                getButton.SetActive(false);
            }

        }
    }

    public void OnGetButton()
    {
        if (myItemPV.isMine)
        {
            ItemParam param = itemObject.GetComponent<ItemParam>();
            var id = param.GetItemId();

            itemFlags[id] = true;
            itemCount[id] += 1;
        }
        myItemPV.RPC("WasgetItem", PhotonTargets.AllViaServer);
    }

    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }

    public int GetItemCount(int count)
    {
        return itemCount[count];
    }

    public void SetItemFlag(int id, bool hoge)
    {
        itemFlags[id] = hoge;
    }

    public void SetItemCount(int id, int amount)
    {
        itemCount[id] += amount;
    }

    [PunRPC]
    void WasgetItem()
    {
        Destroy(itemObject);
        if (myItemPV.isMine)
        {
            getButton.SetActive(false);
        }
    }

}
