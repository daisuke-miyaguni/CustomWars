using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainGameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;


    void Start(){
           if (!PhotonNetwork.connected)
        {                         //Photonに接続できていなければ
            PhotonNetwork.ConnectUsingSettings(null);   //Photonに接続する
            // Debug.Log("Photonに接続しました。");
        }

    }
    
    //Auto-JoinLobbyにチェックを入れているとPhotonに接続後OnJoinLobby()が呼ばれる。
    public void OnJoinedLobby()
    {
        // Debug.Log("ロビーに入りました。");
        //Randomで部屋を選び、部屋に入る（部屋が無ければOnPhotonRandomJoinFailedが呼ばれる）
        PhotonNetwork.JoinRandomRoom();
    }

    //JoinRandomRoomが失敗したときに呼ばれる
    public void OnPhotonRandomJoinFailed()
    {
        // Debug.Log("ルームの入室に失敗しました。");
        //TestRoomという名前の部屋を作成して、部屋に入る
        PhotonNetwork.CreateRoom("TestRoom");
    }

    //部屋に入った時に呼ばれる
    public void OnJoinedRoom()
    {
        // Debug.Log("ルームに入りました。");

        if (!PhotonNetwork.connected)
        {
            SceneManager.LoadScene("Lobby_kawsaki");
            return;
        }
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(1, 6), 1f, 0f), Quaternion.identity, 0);
    }
}
