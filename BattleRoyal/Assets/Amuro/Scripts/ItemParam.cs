using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParam : MonoBehaviour
{
    private ItemData itemData;

    private ItemDataBase itemDataBase;

    public enum items
    {
        Cap,
        Correction,
        Ruler,
        Recovery1,   
        Recovery2,
        Glue,
    }

    public items item = items.Cap;

    private int itemId;

    // rivate MyItemStatus.Item items;

    Rigidbody rb;
    PhotonView ipPV;
    PhotonTransformView ipPTV;

    // Use this for initialization
    void Start()
    {
        int itemNum = (int)item;

        itemId = itemNum;

        rb = GetComponent<Rigidbody>();
        ipPV = GetComponent<PhotonView>();
        ipPTV = GetComponent<PhotonTransformView>();
    }

    // public MyItemStatus.Item GetItems()
    // {
    //     return items;
    // }

    public int GetItemId()
    {
        return itemId;
    }

    // Update is called once per frame
    void Update()
    {
        if (ipPV.isMine)
        {
            Vector3 velocity = rb.velocity;
            ipPTV.SetSynchronizedValues(speed: velocity, turnSpeed: 0);
        }
    }
}
