using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
	SphereCollider sphereCollider;    // 安地範囲

	[SerializeField] float reducingSpeed;    // 縮小スピード
    [SerializeField] float collisionOnTime;    // 判定スタートまでの時間

    [SerializeField] float[] reductionScales;    // 安地の大きさ

	[SerializeField] string deskTag;    // 机のタグ名

	string player = "Player";    // playerのタグ名

	int reductionCount;    // 範囲番号

	PhotonView photonView;

	void Start()
	{
		// 範囲番号の初期化
		reductionCount = 0;

		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.enabled = false;

		photonView = GetComponent<PhotonView>();
		if(PhotonNetwork.isMasterClient)
		{
            photonView.RPC("InitPosition", PhotonTargets.AllViaServer);
        }
		Invoke("TriggerOn", collisionOnTime);
	}

	void InitPosition()
	{
        // 安地の位置をランダムで決める
        gameObject.transform.position = new Vector3(Random.Range(-60f, 60f), 0.0f, Random.Range(-60f, 60f));
		Destroy(photonView.GetComponent<PhotonView>());
	}

	// 判定の初期化(指定秒後に安地判定)
	void TriggerOn()
	{
		sphereCollider.enabled = true;
	}
	
	void Update()
	{
		ScaleChecker();

		// テスト用
		if(Input.GetMouseButtonDown(0))
		{
			ReceveReductionEvent();
		}
	}

	// 安地範囲のチェック
	void ScaleChecker()
	{
		// 指定より大きいと縮小が始まる
        if (sphereCollider.radius > reductionScales[reductionCount])
        {
            Reduction();
        }
	}

	// 縮小処理
	void Reduction()
	{
		sphereCollider.radius -= reducingSpeed * Time.deltaTime;
	}

	// 安地縮小を呼び出す
	public void ReceveReductionEvent()
	{
		reductionCount++;
	}

	// 安地外オブジェクト処理
	void OnTriggerExit(Collider other)
	{
		// 机が安地から出たら机の安地外処理を呼び出す
		if(other.gameObject.tag == deskTag)
		{
			other.gameObject.GetComponent<SafeAreaOut>().SafeAreaExit();
		}

		// Playerが安地に間に合わなかったらPlayerの安地外処理を呼び出す
		if(other.gameObject.tag == player)
		{
			Debug.Log("エリアに間に合わなかった");
			// other.gameObject.GetComponent<PlayerController>().コールデス();
		}
	}
}
