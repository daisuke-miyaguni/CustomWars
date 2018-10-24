using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Photon.MonoBehaviour
{
	[SerializeField] Button joinButton;
    string roomName = "Room";

    int maxPlayerList = 10;

	void Start()
	{
		Connect();
	}

	void Connect()
	{
		PhotonNetwork.ConnectUsingSettings(null);
	}

    //ルーム一覧が取れると
    void OnReceivedRoomListUpdate()
    {
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
		{
			PhotonNetwork.CreateRoom(roomName);
        }
		else
		{
			joinButton.interactable = true;
		}
    }

	public void JoinRoom()
	{
        PhotonNetwork.JoinRoom(roomName);
    }

	void OnJoinedRoom()
	{
		if(PhotonNetwork.playerList.Length > maxPlayerList)
		{
			PhotonNetwork.Disconnect();
		}
		joinButton.interactable = false;
	}
}
