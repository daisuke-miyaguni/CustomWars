using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private PhotonView myPV;
    private Rigidbody myRB;
    private Camera myCamera;
    private Slider hpSlider;

    [SerializeField] private GameObject playerUIPrefab;
    private GameObject playerUIGO;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float playerHP;
    [SerializeField] private float rotateSpeed;

    void Start()
    {
        myPV = GetComponent<PhotonView>();
        myRB = GetComponent<Rigidbody>();

        if (myPV.isMine)
        {
            // カメラ
            myCamera = Camera.main;
            myCamera.transform.parent = transform;
            myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);

            playerUIGO = Instantiate(playerUIPrefab);
            playerUIGO.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);





            // HPバー
            // GameObject myUI = Instantiate(playerUIPrefab,transform.position,Quaternion.identity);
            // myUI.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
            // hpSlider = myUI.GetComponent<Slider>();
            // hpSlider.value = playerHP;
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playerHP);
            //stream.SendNext(this.ChatText);
        }
        else
        {
            playerHP = (int)stream.ReceiveNext();
            //this.ChatText = (string)stream.ReceiveNext();
        }
    }

    void Update()
        {
            if (myPV.isMine)
            {
                // HPManagement();
                Move();
                RotateCamera();

                if (playerHP <= 0)
                {
                    myPV.RPC("Die", PhotonTargets.All);
                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                myPV.RPC("DecreaseLife", PhotonTargets.All);
            }
        }

        private void HPManagement()
        {
            hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            hpSlider.transform.position = transform.position;
            hpSlider.value = playerHP;
        }

        private void RotateCamera()
        {
            // if (Input.GetMouseButton(0))
            // {
            Vector3 angle = new Vector3(
                Input.GetAxis("Mouse X") * rotateSpeed,
                Input.GetAxis("Mouse Y") * rotateSpeed,
                0
            );

            myCamera.transform.RotateAround(transform.position, Vector3.up, angle.x);
            myCamera.transform.RotateAround(transform.position, myCamera.transform.right, angle.y * -1);
            // }
        }

        private void Move()
        {
            myRB.velocity = GetMoveDirection() * moveSpeed;
        }

        private Vector3 GetMoveDirection()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            return new Vector3(x, 0, z);
        }

        // 回避
        public void Avoid()
        {
        }

        // 攻撃
        public void Attack()
        {
        }

        // 回復
        public void Recover()
        {
        }


        public void OnCollisionEnter(Collision other)
        {
            // 被弾
            if (other.gameObject.tag == "weapon")
            {
                myPV.RPC("DecreaseLife", PhotonTargets.All);
            }
        }

    [PunRPC]
    private void DecreaseLife()
    {
        playerHP -= 1;
    }

    // 死亡
    [PunRPC]
    private void Die()
    {
        // Destroy(gameObject);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("hoge");

    }
}
