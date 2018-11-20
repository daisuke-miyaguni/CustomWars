using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Photon.MonoBehaviour
{
    // [SerializeField] float minStartPos_X;    // Playerの一番小さいX軸初期位置
    // [SerializeField] float minStartPos_Z;    // Playerの一番小さいZ軸初期位置
    // [SerializeField] float maxStartPos_X;    // Playerの一番大きいX軸初期位置
    // [SerializeField] float maxStartPos_Z;    // Playerの一番大きいZ軸初期位置

    [SerializeField] float spawnWaitTime;    // スポーンまでのウェイトタイム

    [SerializeField] string spawnPlayerName;    // ResorceからスポーンされるPlayerPrefabの名前 
    // PhotonView pView;

    [SerializeField] Vector3[] spawnPos = new Vector3[]{};

    void Awake()
    {
        if(PhotonNetwork.isMasterClient)
        {
            Vector3[] initPos = spawnPos.OrderBy(i => Guid.NewGuid()).ToArray();
            PhotonView pv = GetComponent<PhotonView>();
            pv.RPC("ShuffleSpawnPos", PhotonTargets.AllViaServer, initPos);
        }
    }

    [PunRPC]
    void ShuffleSpawnPos(Vector3[] initSpawnPos)
    {
        spawnPos = initSpawnPos;
    }

    void Start()
    {
        // pView = GetComponent<PhotonView>();
        // Spawn();
        StartCoroutine(Spawn());
    }

    // スポーン処理
    public IEnumerator Spawn()
    {
        // スポーンウェイトタイム待ってから処理に入る
        yield return new WaitForSeconds(spawnWaitTime);

        // // X初期位置をランダムで決める
        // float spawnX = Random.Range(minStartPos_X, maxStartPos_X);
        // // X初期位置をランダムで決める
        // float spawnZ = Random.Range(minStartPos_Z, maxStartPos_Z);

        for (int playerNum = 0; playerNum < PhotonNetwork.playerList.Length; playerNum++)
        {
            if (PhotonNetwork.player.ID == (playerNum + 1))
            {
                // Playerをスポーン
                PhotonNetwork.Instantiate(spawnPlayerName, spawnPos[playerNum], Quaternion.Euler(transform.TransformDirection(Vector3.zero)), 0);
            }
        }
    }
}