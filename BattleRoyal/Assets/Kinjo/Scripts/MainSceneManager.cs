using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : Photon.MonoBehaviour {
	private int playerNumber;    //プレイヤー数
	private float elapsedTime;    //経過時間
	[SerializeField]private Text elapsedTimeText;    //経過時間を表示するテキストUI
	[SerializeField]private Text playerNumberText;    //プレイヤー数を表示するテキストUI
	[SerializeField]private float scaleDownStartTime = 5.0f;    	//縮小開始時の経過時間を指定
	private PhotonView myPhotonView;    //自身のPhotonView
	[SerializeField]private GameObject resultPanel;    //リザルトパネルUI
	[SerializeField]private Text rankText;    //順位を表示するテキストUI
	[SerializeField]private StageManager stageManager;    //ステージ縮小のスクリプト
	bool isScaleDownBegan = false;    //縮小が始まったかどうか
	[SerializeField] private int lobbyScene;
	bool resultOn = true;

	void Start ()
	{
		AudioManager.Instance.PlayBGM("game_maoudamashii_1_battle30");
		myPhotonView = GetComponent<PhotonView>();
		resultPanel.SetActive(false);
		playerNumber = PhotonNetwork.playerList.Length;
	}
	
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
            StartCoroutine(ScaleUpdate());
		}
	}

	IEnumerator ScaleUpdate()
	{
        yield return new WaitForSeconds(2.0f);
		scaleDownStartTime = (int)elapsedTime + 5.5f;
		isScaleDownBegan = false;
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
	public void GoToResult (bool isDisconnected)
	{
		if(resultOn == false) return;
		int rank = playerNumber;
		resultPanel.SetActive(true);
		if (isDisconnected)
		{
			rankText.text = "切断されました\nあなたの順位は" + rank + "位";
		}
		else
		{
			rankText.text = "あなたの順位は" + rank + "位です!";
		}

		if (rank == 2)
		{
			myPhotonView.RPC("ShowWinnerResult",PhotonTargets.AllViaServer);
		}

		myPhotonView.RPC("PlayerDecrease",PhotonTargets.MasterClient);

		Destroy(GameObject.Find("PlayerControllerUI"));
		resultOn = false;
	}


	//プレイヤー数の減少を更新,マスタークライアントで行う
	[PunRPC]
	private void PlayerDecrease ()
	{
		playerNumber -= 1;
	}

	[PunRPC]
	private void ShowWinnerResult ()
	{
		Time.timeScale = 0;
		if(resultPanel.activeInHierarchy == false)
		{
		Destroy(GameObject.Find("PlayerControllerUI"));
		resultPanel.SetActive(true);
		rankText.text = "いちいだよ";
		}
	}

	//切断したときリザルトを表示する
	void OnDisconnectedFromPhoton()
	{
		GoToResult(true);
	}

	//ロビーへ移動する
	public void GoToTitle ()
	{
		Time.timeScale = 1;
		PhotonNetwork.LeaveRoom();
		SceneManager.LoadScene(lobbyScene);
	}

	//切断する
	public void Disconnect()
	{
		PhotonNetwork.Disconnect();
	}

}
