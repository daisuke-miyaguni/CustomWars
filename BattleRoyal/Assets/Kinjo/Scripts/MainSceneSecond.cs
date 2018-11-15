using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneSecond : Photon.MonoBehaviour {
	[SerializeField]
	private int playerNumber;    //プレイヤー数
	private bool alive = true;
	private float elapsedTime;    //経過時間
	private bool isAlive = true;    //生きているか
	[SerializeField]
	private Text elapsedTimeText;    //経過時間を表示するテキストUI
	[SerializeField]
	private Text playerNumberText;    //プレイヤー数を表示するテキストUI
	[SerializeField]
	private float scaleDownStartTime;    	//縮小開始時の経過時間を指定
	private PhotonView myPhotonView;    //自身のPhotonView
	[SerializeField]
	private GameObject resultPanel;    //リザルトパネルUI
	[SerializeField]
	private Text rankText;    //順位を表示するテキストUI
	[SerializeField]
	private StageManager stageManager;    //ステージ縮小のスクリプト
	bool isScaleDownBegan = false;    //縮小が始まったかどうか
	[SerializeField]
	Text uiText;

	void Start ()
	{
		myPhotonView = GetComponent<PhotonView>();
		resultPanel.SetActive(false);
		playerNumber = PhotonNetwork.playerList.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CalculateElapsedTime();
	}

	//経過時間とプレイヤー数の同期
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            //データの送信
            stream.SendNext(elapsedTime);
            stream.SendNext(playerNumber);
        } else {
            //データの受信
            elapsedTime = (float)stream.ReceiveNext();
            playerNumber = (int)stream.ReceiveNext();
        }
		
		ShowPlayerCount();
		ShowElapsedTime();
    }

	//経過時間を計算する
	private void CalculateElapsedTime ()
	{
		elapsedTime += Time.deltaTime;

		if (elapsedTime >= scaleDownStartTime && !isScaleDownBegan)
		{
			stageManager.ReceveReductionEvent();    //ステージ縮小へ
			isScaleDownBegan = true;
			print("ScaleDown");
		}
	}

	//経過時間を表示する
	private void ShowElapsedTime ()
	{
		if (playerNumber <=1)
		{
			return;
		}
		elapsedTimeText.text = elapsedTime.ToString("f0");
	}

	//プレイヤー数を表示する
	private void ShowPlayerCount ()
	{
		playerNumberText.text = playerNumber.ToString();
	}

	//リザルトの処理を開始する
	public void GoToResult ()
	{
		resultPanel.SetActive(true);
		rankText.text = "Your rank is " + playerNumber + " ！";
		if (playerNumber == 2)
		{
			myPhotonView.RPC("ShowWinnerResult",PhotonTargets.AllViaServer);
		}

		myPhotonView.RPC("PlayerDecrease",PhotonTargets.MasterClient);
	}

	//プレイヤー数の減少を更新,マスタークライアントで行う
	[PunRPC]
	private void PlayerDecrease ()
	{
		playerNumber -= 1;
	}

	//勝利プレイヤーのリザルトを表示する
	[PunRPC]
	private void ShowWinnerResult ()
	{
		if(resultPanel.activeInHierarchy == false)
		{
		resultPanel.SetActive(true);
		rankText.text = "いちいだよ";
		}
	}

	//切断したときリザルトを表示する
	void OnDisconnectedFromPhoton()
	{
		uiText.text = "切断した";
	}

	//切断されたときの処理
	void OnPhotonPlayerDisconnected()
	{
		uiText.text = "切断された";
	}

	//タイトルシーンへ移動する
	public void GoToTitle ()
	{
		//PhotonNetwork.LeaveRoom();
		SceneManager.LoadScene(0);
	}

	//切断する
	public void Disconnect()
	{

		//PhotonNetwork.Disconnect();
	}

}
