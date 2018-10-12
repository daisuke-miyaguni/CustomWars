using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PhotonNetwork.ConnectUsingSettings(null);

    }

	void OnJoinedLobby(){
        PhotonNetwork.JoinRandomRoom();
    }

	void OnPhotonRandomJoinFailed(){
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom(){
        GameObject myPlayer = PhotonNetwork.Instantiate("Character/Cube",
        new Vector3(0f, 0f, 0f), Quaternion.identity, 0);

    }
}
