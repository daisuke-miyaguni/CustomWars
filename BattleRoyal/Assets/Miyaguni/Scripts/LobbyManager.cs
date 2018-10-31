using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Photon.MonoBehaviour
{

	[SerializeField] Button joinButton;    // 参加ボタン
    [SerializeField] Button startButton;    // 参加ボタン
	[SerializeField] Text playerCountText;    // プレイヤー数の表示テキスト
	[SerializeField] Text playerStateText;	  // プレイヤーの状態表示テキスト

    [SerializeField] int maxPlayerList;	   // 最大人数
	[SerializeField] int minPlayerList;	   // 最小人数

	[SerializeField] float gameStartTime;	// ゲームスタートまでの待機時間

	[SerializeField] SceneTransitioner sceneTransitioner;	// シーン移動者

    string roomName = "Room";    // ルーム名
	string playerCount = "PlayerCount: ";    // PlayerCountの文字列
	string playerStateHost = "Host";    // PlayerがHostのときの文字列
	string playerStateGuest = "Guest";		// PlayerがGuestのときの文字列

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
				// Roomのリストを取得可能にする
				isVisible = true,
				// 最大人数を決める
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
		// 人数更新
        playerCountText.text = playerCount + PhotonNetwork.playerList.Length.ToString();
		// ホストかゲストかを判定して可視化
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

	// 他のPlayerが入室してきた
	void OnPhotonPlayerConnected()
	{
		// 人数更新
        playerCountText.text = playerCount + PhotonNetwork.playerList.Length.ToString();

		// Host以外この先の処理を行わない
        if(!PhotonNetwork.isMasterClient)
		{
			return;
		}

		// 上限になったら強制スタート
		if(PhotonNetwork.playerList.Length == maxPlayerList)
		{
			StartCoroutine(BattleStart());
		}// 最小人数に達したらスタートできるようにする
		else if(PhotonNetwork.playerList.Length >= minPlayerList)
		{
			startButton.interactable = true;
        }
	}

	// 他のPlayerが退室した、切断した
	void OnPhotonPlayerDisconnected()
	{
		// 人数更新
		playerCountText.text = playerCount + PhotonNetwork.playerList.Length.ToString();
		// ホストかゲストか識別更新して可視化
        if (PhotonNetwork.isMasterClient)
        {
            playerStateText.text = playerStateHost;
			// 人数によってButtonを押せるか押せないか
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

	// Battleをスタートした
	public void BattleStartOnClick()
	{
        StartCoroutine(BattleStart());
    }

    // バトルを開始する
    IEnumerator BattleStart()
	{
		// スタートボタンを押せないようにする
		startButton.interactable = false;
		// ゲームスタートまでのウェイトタイム待ってその後処理する
		yield return new WaitForSeconds(gameStartTime);
		// この処理内で人数が最少以上であればゲームスタートシーンを呼び出す
        if (PhotonNetwork.playerList.Length >= minPlayerList && PhotonNetwork.isMasterClient)
        {
            // SceneTrasitionerを参照してシーン遷移
            sceneTransitioner.ReceveMoveScene();
        }
	}
}
