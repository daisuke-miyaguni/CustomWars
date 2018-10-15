using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManeger : Photon.MonoBehaviour {
    [SerializeField]
    Vector3[] startPos;

    IEnumerator Start(){
        //PhotonView pView = GetComponent<PhotonView>();
        yield return new WaitForSeconds(0.3f);

        int myNum = PhotonNetwork.player.ID;
        PhotonNetwork.Instantiate("Cat", startPos[myNum-1], Quaternion.Euler(Vector3.zero), 0);
    }
}