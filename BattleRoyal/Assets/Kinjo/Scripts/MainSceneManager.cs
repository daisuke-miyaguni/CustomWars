using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : Photon.MonoBehaviour {
	[SerializeField]
	private int playerNumber;    //プレイヤー数
	private float elapsedTime;    //経過時間
	[SerializeField]
	private Text elapsedTimeText;    //経過時間を表示するテキストUI
	[SerializeField]
	private Text playerNumberText;    //プレイヤー数を表示するテキストUI
	[SerializeField]
	private int scaleDownStartTime;    	//縮小が始まる時間
	private PhotonView myPhotonView;    //自身のPhotonView
	[SerializeField]
	private GameObject resultPanel;    //リザルトパネルUI
	[SerializeField]
	private Text rankText;    //順位を表示するテキストUI
	[SerializeField]
	private StageManager stageManager;    //ステージ縮小のスクリプト

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
		
		if ((int)elapsedTime == scaleDownStartTime)
		{
			stageManager.ReceveReductionEvent();    //ステージ縮小へ
		}
	}

	//経過時間を表示する
	private void ShowElapsedTime ()
	{
		elapsedTimeText.text = elapsedTime.ToString("f0");
	}

	//プレイヤー数を表示する
	private void ShowPlayerCount ()
	{
		playerNumberText.text = playerNumber.ToString();
	}

	//リザルトの処理を開始する
	public void GoToResult (bool isDisconnected)
	{
		resultPanel.SetActive(true);
		if (isDisconnected)
		{
		    rankText.text = "切断されました\nYour rank is " + playerNumber + " ！";
		} else
		{
			rankText.text = "Your rank is " + playerNumber + " ！";
		}
		myPhotonView.RPC("PlayerDecrease",PhotonTargets.MasterClient);
	}

	//プレイヤー数の更新,マスタークライアントで行う
	[PunRPC]
	private void PlayerDecrease ()
	{
		playerNumber -= 1;
	}

	void OnPhotonPlayerDisconnected()
	{
		GoToResult(true);
	}

}