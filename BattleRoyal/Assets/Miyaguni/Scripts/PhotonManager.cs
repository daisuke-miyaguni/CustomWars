using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour {
	PhotonView pView;

	void Start(){
		Connect();
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings("v1.0");
	}

	void OnJoinedLobby(){
		Debug.Log("Join Lobby");
    }

    //ルーム一覧が取れると
    void OnReceivedRoomListUpdate(){
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0) {
            Debug.Log("not find room");
			CreateRoom();
        } else {
			JoinRoom();
        }  
    }

	//ルーム作成
    public void CreateRoom() {
        PhotonNetwork.CreateRoom("Room");
		Debug.Log("Creat Room");
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom("Room");
    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom() {
        // Roomに参加しているプレイヤー情報を配列で取得.
        PhotonPlayer[] player = PhotonNetwork.playerList;
    }
}