using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Photon.MonoBehaviour
{
    [SerializeField]
    Vector3[] startPos;
    
    string playerNumberText = "Number: ";

    [SerializeField]
    Text playerNumber;

    [SerializeField]
    Text playerMaster;

    void Start()
    {
        MasterClientChecker();
    }

    void MasterClientChecker()
    {
        playerMaster.text = PhotonNetwork.isMasterClient.ToString();
    }

    public void Spawn()
    {
        int myNum = PhotonNetwork.player.ID;
        GameObject player = PhotonNetwork.Instantiate("Cube", startPos[myNum-1], Quaternion.Euler(Vector3.zero), 0);
        playerNumber.text = playerNumberText + myNum.ToString();
    }
}