using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListShuffle : MonoBehaviour
{

    // [SerializeField] float spawnWaitTime;    // スポーンまでのウェイトタイム

    // [SerializeField] string spawnPlayerName;    // ResorceからスポーンされるPlayerPrefabの名前 
    // PhotonView pView;

    [SerializeField] List<int> spawnPos = new List<int>() { 3, 6, 9 };

    void Start()
    {
        spawnPos = spawnPos.OrderBy(i => Guid.NewGuid()).ToList();
        foreach (int num in spawnPos)
        {
            print(num);
        }
    }
}
