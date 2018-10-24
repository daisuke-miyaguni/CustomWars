using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
	[SerializeField] string sceneName;    // 移動するシーン名
	PhotonView photonView;    // PhotonViewの定義

	void Start()
	{
		photonView = GetComponent<PhotonView>();
	}

	// シーン遷移することを受け取る
	public void ReceveMoveScene()
	{
		photonView.RPC("MoveScene", PhotonTargets.All);
	}

	// 同期処理呼び出し
	[PunRPC]
	void MoveScene()
	{
        SceneManager.LoadScene(sceneName);
    }
}
