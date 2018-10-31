using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Photon.MonoBehaviour
{
    [SerializeField] float minStartPos_X;    // Playerの一番小さいX軸初期位置
    [SerializeField] float minStartPos_Z;    // Playerの一番小さいZ軸初期位置
    [SerializeField] float maxStartPos_X;    // Playerの一番大きいX軸初期位置
    [SerializeField] float maxStartPos_Z;    // Playerの一番大きいZ軸初期位置

    [SerializeField] float spawnWaitTime;    // スポーンまでのウェイトタイム

    [SerializeField] string spawnPlayerName;    // ResorceからスポーンされるPlayerPrefabの名前 
    // PhotonView pView;

    void Start()
    {
        // pView = GetComponent<PhotonView>();
        Spawn();
        StartCoroutine(Spawn());
    }

    // スポーン処理
    public IEnumerator Spawn()
    {
        // スポーンウェイトタイム待ってから処理に入る
        yield return new WaitForSeconds(spawnWaitTime);

        // X初期位置をランダムで決める
        float spawnX = Random.Range(minStartPos_X, maxStartPos_X);
        // X初期位置をランダムで決める
        float spawnZ = Random.Range(minStartPos_Z, maxStartPos_Z);

        // Playerをスポーン
        GameObject player = PhotonNetwork.Instantiate(spawnPlayerName, new Vector3(spawnX, 1.0f, spawnZ), Quaternion.Euler(Vector3.zero), 0);
    }
}