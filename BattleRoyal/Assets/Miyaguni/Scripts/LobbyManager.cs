using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Photon.MonoBehaviour
{

	[SerializeField] Button joinButton;    // 参加ボタン
    [SerializeField] Button startButton;    // 参加ボタン
	[SerializeField] Text playerCountText;
	[SerializeField] Text playerStateText;

    [SerializeField] int maxPlayerList;	   // 最大人数
	[SerializeField] int minPlayerList;	   // 最小人数

	[SerializeField] float gameStartTime;	// ゲームスタートまでの待機時間

	[SerializeField] SceneTransitioner sceneTransitioner;	// シーン移動者

    string roomName = "Room";	// ルーム名
	string playerCount = "PlayerCount: ";
	string playerStateHost = "Host";
	string playerStateGuest = "Guest";

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
			PhotonNetwork.CreateRoom(roomName, new RoomOptions()
			{
				isVisible = true,
				maxPlayers = 4,
			},null);
        }
		else
		{
			// 入室ボタンを押せるようにする
			joinButton.interactable = true;
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
        playerCountText.text = playerCount + PhotonNetwork.playerList.Length.ToString();
		// ホストならスタートボタンを可視化する
		if(PhotonNetwork.isMasterClient)
		{
			playerStateText.text = playerStateHost;
        }
		else
		{
        	// 入室ボタンを押せないようにする
        	joinButton.interactable = false;
			playerStateText.text = playerStateGuest;
		}
        // 制限した数より上回ってたら切断する
        if(PhotonNetwork.playerList.Length > maxPlayerList)
		{
			PhotonNetwork.Disconnect();
		}
	}

	void OnPhotonPlayerConnected()
	{
        playerCountText.text = playerCount + PhotonNetwork.playerList.Length.ToString();
        if(!PhotonNetwork.isMasterClient)
		{
			return;
		}

		// 上限になったら強制スタート
		if(PhotonNetwork.playerList.Length == maxPlayerList)
		{
			StartCoroutine(BattleStart());
		}
		else if(PhotonNetwork.playerList.Length >= minPlayerList)
		{
			startButton.interactable = true;
        }
	}

	void OnPhotonPlayerDisconnected()
	{
		playerCountText.text = playerCount + PhotonNetwork.playerList.Length.ToString();
        if (PhotonNetwork.isMasterClient)
        {
            playerStateText.text = playerStateHost;
            if (PhotonNetwork.playerList.Length >= minPlayerList)
            {
                startButton.interactable = true;
            }
			else
			{
				startButton.interactable = false;
			}
        }
        else
        {
            // 入室ボタンを押せないようにする
            joinButton.interactable = false;
            playerStateText.text = playerStateGuest;
        }      
	}

	public void BattleStartOnClick()
	{
        StartCoroutine(BattleStart());
    }

    // バトルを開始する
    IEnumerator BattleStart()
	{
		startButton.interactable = false;
		yield return new WaitForSeconds(gameStartTime);
        if (PhotonNetwork.playerList.Length >= minPlayerList && PhotonNetwork.isMasterClient)
        {
            // SceneTrasitionerを参照してシーン遷移
            sceneTransitioner.ReceveMoveScene();
        }
	}
}
