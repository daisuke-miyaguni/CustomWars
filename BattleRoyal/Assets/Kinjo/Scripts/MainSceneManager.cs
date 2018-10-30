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
		}
	}

	//経過時間を処理する
	private void CalculateElapsedTime ()
	{
		elapsedTime += Time.deltaTime;
		myPhotonView.RPC("ShowElapsedTime", PhotonTargets.AllViaServer,elapsedTime);
	}

	//ホストかどうかを判別する
	private void DetermineIfHost ()
	{
		if (PhotonNetwork.player.ID == 1)
		{
			isHost = true;
		}
	}

	//経過時間を表示する
	[PunRPC]
	private void ShowElapsedTime (float receiveTime)
	{
		elapsedTimeText.text = receiveTime.ToString();
	}
}
