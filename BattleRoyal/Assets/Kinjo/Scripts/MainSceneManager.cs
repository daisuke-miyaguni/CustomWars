using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : Photon.MonoBehaviour {
	private int playerNumber;    //プレイヤー数
	private float elapsedTime;    //経過時間
	private PhotonView myPhotonView;    //自身のPhotonView
	private bool isHost = false;    //Hostかどうか
	[SerializeField]
	private Text elapsedTimeText;    //経過時間を表示するテキストUI
	[SerializeField]
	private Text playerNumberText;    //プレイヤー数を表示するテキストUI
	[SerializeField]
	private float scaleDownStartTime;    	//縮小が始まる時間
	void Start ()
	{
		myPhotonView = GetComponent<PhotonView>();
		DetermineIfHost();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isHost)
		{
			CalculateElapsedTime();
			myPhotonView.RPC("ShowPlayerCount",PhotonTargets.AllViaServer);
		}
	}

	//経過時間を計算する
	private void CalculateElapsedTime ()
	{
		elapsedTime += Time.deltaTime;
		
		if ((int)elapsedTime == scaleDownStartTime)
		{
			//ステージ縮小へ
		}

		myPhotonView.RPC("ShowElapsedTime", PhotonTargets.AllViaServer,elapsedTime);
	}

	//ホストかどうかを判別する
	private void DetermineIfHost ()
	{
		if (PhotonNetwork.player.ID == 1)
		{
			isHost = true;
			//RoomInfo[] roomInfo = PhotonNetwork.GetRoomList();
			//if (roomInfo == null||roomInfo.Length == 0) return;

			//
			//playerNumber = roomInfo[0].PlayerCount;    //開始時のゲーム全体のプレイヤー数の同期
			Debug.Log(playerNumber);
			//myPhotonView.RPC("ShowPlayerCount",PhotonTargets.AllViaServer);
		}
	}

	//経過時間を表示する
	[PunRPC]
	private void ShowElapsedTime (float receiveTime)
	{
		elapsedTimeText.text = receiveTime.ToString("f0");
	}

	//プレイヤー数を表示する
	[PunRPC]
	private void ShowPlayerCount ()
	{
		playerNumberText.text = playerNumber.ToString();
	}

	//ゲームオーバーの処理をする（仮組み）
	[PunRPC]
	private void GameOver ()
	{
		playerNumber -= 1;
		playerNumberText.text = "がめおべーら";
	}

	//デバッグ用の自爆ボタン
	public void DebugStart ()
	{
		myPhotonView.RPC("GameOver",PhotonTargets.AllViaServer);
	} 
}
