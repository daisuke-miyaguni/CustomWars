using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxSpawner : Photon.MonoBehaviour
{
    [SerializeField] Vector3[] itemBoxSpawnPos;    // アイテムボックスのスポーンポジション

    [SerializeField] float itemSpawnWaitTime;    // アイテムスポーンまでのウェイトタイム

    [SerializeField] GameObject itemBox;    // アイテムボックス名

    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            StartCoroutine(ItemSpawn());
        }
    }

    IEnumerator ItemSpawn()
    {
        // 生成まで時間をもたせる
        yield return new WaitForSeconds(itemSpawnWaitTime);

        for (int i = 0; i < itemBoxSpawnPos.Length; i++)
        {
            PhotonNetwork.InstantiateSceneObject
            (
                itemBox.name,
                itemBoxSpawnPos[i],
                Quaternion.Euler(new Vector3(0, Random.Range(0.0f, 180.0f), 0)),
                0,
                null
            );
        }

        Destroy(gameObject);
    }
}
