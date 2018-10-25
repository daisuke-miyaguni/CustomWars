using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : Photon.MonoBehaviour {
	private int playerNumber;    //プレイヤー数
	private float elapsedTime;    //経過時間
	private bool isHost = false;    //ホストかどうか
	private PhotonView myPhotonView = null;
	[SerializeField]
	private Text elapsedTimeText;    //経過時間の文字

	void Start ()
	{
		myPhotonView = GetComponent<PhotonView>();
		DetermineIfHost();
	}
	
	// Update is called once per frame
	void Update () {
		if (isHost)
		{
			CalculateElapsedTime();
		}
	}

	private void DetermineIfHost ()
	{
		if (PhotonNetwork.player.ID == 0)
		{
			isHost = true;
		}
	}

	//経過時間の計算（ホストのみ）
	private void CalculateElapsedTime ()
	{
		elapsedTime += Time.deltaTime;
		myPhotonView.RPC("ShowElapsedTime",PhotonTargets.AllViaServer);
	}

	[PunRPC]
	private void ShowElapsedTime ()
	{
		elapsedTimeText.text = (elapsedTime/60).ToString() + ":" + ((int)elapsedTime).ToString("D2");
	}
}
