using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Photon.MonoBehaviour
{
    [SerializeField] float spawnWaitTime;    // スポーンまでのウェイトタイム

    [SerializeField] String[] spawnPlayer;    // ResorceからスポーンされるPlayerPrefabの名前 

    [SerializeField] Vector3[] spawnPos = new Vector3[] { };

    PhotonView pv;

    void Awake()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Vector3[] initPos = spawnPos.OrderBy(i => Guid.NewGuid()).ToArray();
            pv = GetComponent<PhotonView>();
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
        PhotonNetwork.Instantiate(spawnPlayer[PhotonNetwork.player.ID % 4], spawnPos[PhotonNetwork.player.ID % 4], Quaternion.Euler(transform.TransformDirection(Vector3.zero)), 0);
        Destroy(this.gameObject);
    }
}