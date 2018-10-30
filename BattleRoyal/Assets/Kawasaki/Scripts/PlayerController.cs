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
    [SerializeField] private Slider hpSlider;

    // [SerializeField] private GameObject playerUIPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerHP;
    [SerializeField] private float rotateSpeed;

    int damage = -1;
    int healing = 2;
    [SerializeField] private GameObject weaponPrefab;
    private float weaponPower = 200;

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

            //HPバー
            // GameObject myUI = Instantiate(playerUIPrefab, transform.position, Quaternion.identity);
            // myUI.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
            // hpSlider = myUI.GetComponentInChildren<Slider>();
            hpSlider.value = playerHP;
        }

    }

    void Update()
    {
        if (myPV.isMine)
        {
            Move();

            // ChangeHP();
            // RotateCamera();

            if (playerHP <= 0)
            {
                myPV.RPC("Death", PhotonTargets.All);
            }

            if (Input.GetMouseButtonDown(1))
            {
                myPV.RPC("ChangeHP", PhotonTargets.All, damage);
            }

            if (Input.GetMouseButtonDown(0))
            {
                myPV.RPC("ChangeHP", PhotonTargets.All, healing);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 posUp = transform.position+new Vector3(0,2,0);
                myPV.RPC("Attack", PhotonTargets.All, posUp, weaponPower);
            }
        }
    }
    // 移動処理
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




    [PunRPC]
    private void ChangeHP(int value)
    {
        playerHP += value;
        hpSlider.value = playerHP;
    }


    public void OnTriggerEnter(Collider other)
    {
        // 被弾
        if (myPV.isMine && other.gameObject.tag == "weapon")
        {
            myPV.RPC("ChangeHP", PhotonTargets.All, damage);
        }
    }



    // 死亡
    [PunRPC]
    private void Death()
    {
        Destroy(gameObject);
        // PhotonNetwork.Disconnect();
        // SceneManager.LoadScene("hoge");

    }

    // 回避
    public void Avoid()
    {
    }

    // 攻撃
    [PunRPC]
    private void Attack(Vector3 pos, float power)
    {
        GameObject weapon = Instantiate(weaponPrefab, pos, Quaternion.identity);
        weapon.GetComponent<Rigidbody>().AddForce(Vector3.up * power);
    }

    // 回復
    public void Recover()
    {
    }
}
