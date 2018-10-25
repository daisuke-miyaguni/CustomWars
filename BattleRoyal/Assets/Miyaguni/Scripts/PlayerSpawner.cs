using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Photon.MonoBehaviour
{
    [SerializeField] float minStartPos_X;
    [SerializeField] float minStartPos_Z;
    [SerializeField] float maxStartPos_X;
    [SerializeField] float maxStartPos_Z;

    [SerializeField] float spawnWaitTime;

    [SerializeField] string spawnPlayerName;
    // PhotonView pView;

    void Start()
    {
        // pView = GetComponent<PhotonView>();
        Spawn();
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnWaitTime);
        float spawnX = Random.Range(minStartPos_X, maxStartPos_X);
        float spawnZ = Random.Range(minStartPos_Z, maxStartPos_Z);

        GameObject player = PhotonNetwork.Instantiate(spawnPlayerName, new Vector3(spawnX, 1.0f, spawnZ), Quaternion.Euler(Vector3.zero), 0);
    }
}