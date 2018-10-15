using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player:Photon.MonoBehaviour {      
    public float move = 100f;
    public float rotate = 10f;
    public float JumpForce = 10f;

    bool change;
    float fleezTime;
    float gameTime = 60.0f;

    PhotonView p_photonView;

    [SerializeField]
    GameObject childFlea;
    [SerializeField]
    GameObject[] Text;

    public static int[] playerPoint = { 0, 0 };

    IEnumerator Start() {
        p_photonView = GetComponent<PhotonView>();
        if (p_photonView.isMine) {
            //UIの設定
            Text[0] = GameObject.Find("1P Point Text");
            Text[1] = GameObject.Find("2P Point Text");
            Text[2] = GameObject.FindWithTag("TimeText");
            //カメラをプレイヤーの子どもにする
            Camera.main.transform.parent = gameObject.transform; 
            //0.3秒後に処理
            yield return new WaitForSeconds(0.3f);
            //初期ノミの設定
            if (PhotonNetwork.isMasterClient) {
            }                                                                                                                                
            Debug.Log(PhotonNetwork.player.ID + " ");
        }
    }

    void Update() {
        // 持ち主でないのなら制御させない
        if (p_photonView.isMine) {  
            Camera.main.transform.position = gameObject.transform.Find("CameraPos").transform.position;
            Camera.main.transform.rotation = gameObject.transform.Find("CameraPos").transform.rotation;

            //Playerにノミがある時ない時の操作
            float x = Input.GetAxis("Vertical");
            float z = Input.GetAxis("Horizontal");
            transform.Rotate(0, z * rotate, 0);
            if (childFlea.activeSelf) {
                transform.Translate(0, 0, x * move * 1.2f);
            } else {
                transform.Translate(0, 0, x * move);
            }
        }
    }

    private void FixedUpdate() {
        fleezTime -= Time.deltaTime;

        gameTime = Mathf.Clamp(gameTime, 0.0f, gameTime);

        if (p_photonView.isMine) {
            Text[0].GetComponent<Text>().text = playerPoint[0].ToString();
            Text[1].GetComponent<Text>().text = playerPoint[1].ToString();
            Text[2].GetComponent<Text>().text = gameTime.ToString("F1");
        }

        if (gameTime <= 0) {
            Pointer();
        }
    }   

    void Pointer() {
        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && fleezTime <= 0) {
        }

        if (other.gameObject.tag == "Item" && p_photonView.isMine) {
        }
    }

    void OnTriggerStay(Collider other) {
        if (!p_photonView.isMine)
            return;
        //Playerのジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && other.gameObject.tag == "Floor") {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(0, 1 * JumpForce, 0);
        }
    }
}
