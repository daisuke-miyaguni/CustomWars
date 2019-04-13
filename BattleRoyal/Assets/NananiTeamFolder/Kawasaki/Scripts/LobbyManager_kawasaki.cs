using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager_kawasaki : MonoBehaviour
{


    [SerializeField] Text playerCountText;    // プレイヤー数の表示テキスト
    [SerializeField] int maxPlayerList;    // 最大人数
    [SerializeField] int minPlayerList;    // 最小人数
    [SerializeField] float gameStartTime;   // ゲームスタートまでの待機時間
    [SerializeField] PhotonSceneTransitioner photonSceneTransitioner;	// シーン移動者
    [SerializeField] int sendRateSetting = 30;
    [SerializeField] int sendRateOnSerializeSetting = 30;
    string playerCount = "人数: ";    // PlayerCountの文字列
    private CountDown countDown;

    void Awake()
    {
        // Photonに接続
        PhotonNetwork.ConnectUsingSettings("v0.10");
    }

    void Start()
    {
        AudioManager.Instance.PlayBGM("bgm_maoudamashii_neorock79");
        photonSceneTransitioner = GetComponent<PhotonSceneTransitioner>();
        PhotonNetwork.sendRate = sendRateSetting;
        PhotonNetwork.sendRateOnSerialize = sendRateOnSerializeSetting;
        countDown = gameObject.GetComponent<CountDown>();
    }
    void Update()
    {
        if (countDown.isGameStart)
        {
            StartCoroutine(BattleStart());
        }
    }

    void OnReceivedRoomListUpdate()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        RoomOptions roomOptions = new RoomOptions()
        {
            // Roomのリストを取得可能にする
            IsVisible = true,
            // 最大人数を決める
            MaxPlayers = (byte)maxPlayerList,
        };
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }


    // ルームに参加した
    void OnJoinedRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            // 人数更新
            playerCountText.text = playerCount + PhotonNetwork.room.PlayerCount.ToString();

            // 制限した数より上回ってたら切断する
            if (PhotonNetwork.room.PlayerCount > maxPlayerList)
            {
                PhotonNetwork.Disconnect();
            }
        }
    }

    // 他のPlayerが入室してきた
    void OnPhotonPlayerConnected()
    {
        if (PhotonNetwork.inRoom)
        {
            // 人数更新
            playerCountText.text = playerCount + PhotonNetwork.room.PlayerCount.ToString();
        }
    }

    // 他のPlayerが退室した、切断した
    void OnPhotonPlayerDisconnected()
    {
        if (PhotonNetwork.inRoom)
        {
            // 人数更新
            playerCountText.text = playerCount + PhotonNetwork.room.PlayerCount.ToString();
        }

    }

    // バトルを開始する
    public IEnumerator BattleStart()
    {
        // 部屋を見えなくする
        PhotonNetwork.room.IsVisible = false;
        // ゲームスタートまでのウェイトタイム待ってその後処理する
        yield return new WaitForSeconds(gameStartTime);
        // この処理内で人数が最少以上であればゲームスタートシーンを呼び出す
        if (PhotonNetwork.playerList.Length >= minPlayerList && PhotonNetwork.isMasterClient)
        {
            // SceneTrasitionerを参照してシーン遷移
            photonSceneTransitioner.ReceveMoveScene();
        }
    }
}
