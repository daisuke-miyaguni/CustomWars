using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustManager : MonoBehaviour
{
    int initChildCount;
    [SerializeField] float itemBoxInstantePosY = -0.55f;


    [SerializeField] int maxTrashCounter;

    PhotonView dustPV;

    public int GetMaxTrashCounter()
    {
        return maxTrashCounter + initChildCount;
    }

    // [SerializeField] string itemBoxName;
    [SerializeField] GameObject itemBox;

    public int GetTrashCounter()
    {
        return gameObject.transform.childCount;
    }


    void Start()
    {
        initChildCount = gameObject.transform.childCount;
        dustPV = GetComponent<PhotonView>();
    }

    void Update()
    {
        TrashChecker();
    }

    void TrashChecker()
    {
        if (gameObject.transform.childCount >= GetMaxTrashCounter())
        {
            dustPV.RPC("FullTrashs", PhotonTargets.MasterClient);
        }
    }

    [PunRPC]
    void FullTrashs()
    {
        PhotonNetwork.InstantiateSceneObject
        (
            itemBox.name,
            new Vector3(transform.position.x, itemBoxInstantePosY, transform.position.z - 5.0f),
            Quaternion.Euler(new Vector3(0, Random.Range(0.0f, 180.0f), 0)),
            0,
            null
        );
        foreach (Transform i in gameObject.transform.Find("Trash"))
        {
            i.transform.parent = null;
        }
        Destroy(this);
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Desk")
        {
            Destroy(gameObject);
        }
    }
}
