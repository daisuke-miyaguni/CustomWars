using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{

    // [SerializeField] LobbyManager lobbyManager;
    [SerializeField] float maxCount;
    [SerializeField] Text countDownText;	  // カウントダウンの表示テキスト
    [SerializeField] float leftTime = 0;
    public bool isGameStart = false;
    // public bool isCountStart = false;

    void Start()
    {
        leftTime = maxCount;
    }

    void Update()
    {
        if (PhotonNetwork.inRoom)
        {
            if (PhotonNetwork.room.PlayerCount == 1)
            {
                isGameStart = false;
                countDownText.text = "マッチング中";
                leftTime = 15;
            }
            if (PhotonNetwork.room.PlayerCount > 1)
            {
                leftTime -= Time.deltaTime;
            }

            if (leftTime <= 1)
            {
                isGameStart = true;
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(leftTime);
        }
        else
        {
            //データの受信
            this.leftTime = (float)stream.ReceiveNext();
        }
        countDownText.text = "開始まで\n" + leftTime.ToString("F0");

    }
}
