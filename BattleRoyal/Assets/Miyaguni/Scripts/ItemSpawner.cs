using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Photon.MonoBehaviour
{
	// [SerializeField] Vector3 itempSpawnPos;    // アイテムのスポーンポジション
	[SerializeField] Vector3[] itempBoxSpawnPos;    // アイテムボックスのスポーンポジション
	//int[] itemBoxCount = {0, 1, 2, 3};
	List<int> itemBoxCount = new List<int>{0, 1, 2, 3}; // アイテムボックス番号

	[SerializeField] float itemSpawnWaitTime;	 // アイテムスポーンまでのウェイトタイム
	// [SerializeField] float minRandomPos_X;	// Xポジションランダム化の最小値
    // [SerializeField] float maxRandomPos_X;	// Xポジションランダム化の最大値
    // [SerializeField] float minRandomPos_Z;	// Zポジションランダム化の最大値
    // [SerializeField] float maxRandomPos_Z;	// Zポジションランダム化の最大値

    // [SerializeField] string[] itemName;    // アイテム名
    [SerializeField] string itempBoxName;    // アイテムボックス名

    void Start ()
	{
		if(PhotonNetwork.isMasterClient)
		{
			StartCoroutine(ItemSpawn());
		}
	}

	IEnumerator ItemSpawn()
	{
        // int itemBoxCount = Random.Range(0, itempBoxSpawnPos.Length);    // Itemのランダム化

		// 生成まで時間をもたせる
        yield return new WaitForSeconds(itemSpawnWaitTime);

        for (int i = 0; i < itempBoxSpawnPos.Length - 1; i++)
        {
			int spawnNum = itemBoxCount[Random.Range(0, itemBoxCount.Count)];
			itemBoxCount.Remove(spawnNum);
            GameObject itemBox = PhotonNetwork.Instantiate(itempBoxName,
			itempBoxSpawnPos[spawnNum],
			Quaternion.Euler(Vector3.zero),
			0);
        }

		Destroy(gameObject);
	}
}
