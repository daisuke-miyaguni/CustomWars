using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonSceneTransitioner : MonoBehaviour
{
	[SerializeField] int sceneNum;    // 移動するシーン名
	PhotonView photonView;    // PhotonViewの定義

	void Start()
	{
		photonView = GetComponent<PhotonView>();
	}

	// シーン遷移することを受け取る
	public void ReceveMoveScene()
	{
		photonView.RPC("MoveScene", PhotonTargets.AllViaServer);
	}

	// 同期処理呼び出し
	[PunRPC]
	void MoveScene()
	{
        SceneManager.LoadScene(sceneNum);
    }
}
