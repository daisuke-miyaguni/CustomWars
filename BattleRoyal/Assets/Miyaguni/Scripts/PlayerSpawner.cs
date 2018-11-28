using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Photon.MonoBehaviour
{
    [SerializeField] float spawnWaitTime;    // スポーンまでのウェイトタイム

    [SerializeField] GameObject spawnPlayer;    // ResorceからスポーンされるPlayerPrefabの名前 

    [SerializeField] Vector3[] spawnPos = new Vector3[] { };

    void Awake()
    {
        if (PhotonNetwork.isMasterClient)
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
        StartCoroutine(Spawn());
    }

    // スポーン処理
    public IEnumerator Spawn()
    {
        // スポーンウェイトタイム待ってから処理に入る
        yield return new WaitForSeconds(spawnWaitTime);
        // Playerをスポーン
        PhotonNetwork.Instantiate(spawnPlayer.name, spawnPos[PhotonNetwork.player.ID % 4], Quaternion.Euler(transform.TransformDirection(Vector3.zero)), 0);
        // for (int playerNum = 0; playerNum < PhotonNetwork.playerList.Length; playerNum++)
        // {
        //     if (PhotonNetwork.player.ID % 4 == playerNum)
        //     {
        //         // Playerをスポーン
        //         PhotonNetwork.Instantiate(spawnPlayer.name, spawnPos[playerNum], Quaternion.Euler(transform.TransformDirection(Vector3.zero)), 0);
        //     }
        // }

        Destroy(gameObject);
    }
}