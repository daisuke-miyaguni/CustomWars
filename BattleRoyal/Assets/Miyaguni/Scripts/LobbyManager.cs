using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Photon.MonoBehaviour
{

	[SerializeField] Button[] buttons;    // ボタンたち

    [SerializeField] int maxPlayerList;	   // 最大人数
	[SerializeField] int minPlayerList;	   // 最小人数

	[SerializeField] float gameStartTime;	// ゲームスタートまでの待機時間

	[SerializeField] SceneTransitioner sceneTransitioner;	// シーン移動者

    string roomName = "Room";	// ルーム名

	void Start()
	{
		sceneTransitioner = GetComponent<SceneTransitioner>();
		Connect();
	}

	// Photonに接続
	void Connect()
	{
		PhotonNetwork.ConnectUsingSettings(null);
	}

    //ルーム一覧が取れると
    void OnReceivedRoomListUpdate()
    {
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();    // ルームのリスト
        if (rooms.Length == 0)
		{
			// 部屋を作る
			PhotonNetwork.CreateRoom(roomName);
        }
		else
		{
			// 入室ボタンを押せるようにする
			buttons[0].interactable = true;
		}
    }

	// ルームに参加する
	public void JoinRoom()
	{
        PhotonNetwork.JoinRoom(roomName);
    }

	// ルームに参加した
	void OnJoinedRoom()
	{	
		// 入室ボタンを押せないようにする
        buttons[0].interactable = false;
        // 制限した数より上回ってたら切断する
        if(PhotonNetwork.playerList.Length > maxPlayerList)
		{
			PhotonNetwork.Disconnect();
		}
	}

	void OnPhotonPlayerConnected()
	{
		if(!PhotonNetwork.isMasterClient)
		{
			return;
		}

		// 10人になったら強制スタート
		if(PhotonNetwork.playerList.Length == maxPlayerList)
		{
			StartCoroutine(BattleStart());
		}
		else if(PhotonNetwork.playerList.Length >= minPlayerList)
		{
            buttons[1].interactable = true;
        }
	}

    // バトルを開始する
    IEnumerator BattleStart()
	{
		buttons[1].interactable = false;
		yield return new WaitForSeconds(gameStartTime);
        if (PhotonNetwork.playerList.Length >= minPlayerList && PhotonNetwork.isMasterClient)
        {
            // SceneTrasitionerを参照してシーン遷移
            sceneTransitioner.ReceveMoveScene();
        }
	}
}
