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
    private Button attackButton;
    private Button jumpButton;
    private Button itemButton;
    [SerializeField] private Slider hpSlider;

    // playerステータス
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerHP;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private MobileInputController controller;

    int damage = -1;
    int healing = 2;
    private float weaponPower = 200;


    void Awake()
    {
        // photonview取得
        myPV = GetComponent<PhotonView>();

    }

    void Start()
    {

        if (myPV.isMine)
        {
            // rigidbody取得
            myRB = GetComponent<Rigidbody>();
            // 左スティック取得
            controller = GameObject.Find("LeftJoyStick").GetComponent<MobileInputController>();
            // 攻撃ボタン取得、設定
            attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
            attackButton.onClick.AddListener(this.OnClickAttack);
            // ジャンプボタン取得、設定
            jumpButton = GameObject.Find("JumpButton").GetComponent<Button>();
            jumpButton.onClick.AddListener(this.Jump);

            // カメラ取得、位置調整
            myCamera = Camera.main;
            myCamera.transform.parent = transform;
            myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);
            //hp初期値設定
            hpSlider.value = playerHP;
        }

    }
    void FixedUpdate()
    {
        if (myPV.isMine)
        {
            Move();
        }
    }

    void Update()
    {
        if (myPV.isMine)
        {
            if (playerHP <= 0)
            {
                myPV.RPC("Death", PhotonTargets.All);
            }
        }
    }


    // 移動処理
    private void Move()
    {
        if (GetMoveDirection().x != 0 || GetMoveDirection().z != 0)
        {
            myRB.velocity = new Vector3(GetMoveDirection().x * moveSpeed,
            myRB.velocity.y,
            GetMoveDirection().z * jumpForce);
        }
    }

    private Vector3 GetMoveDirection()
    {
        float x = controller.Horizontal;
        float z = controller.Vertical;

        return new Vector3(x, 0, z);
    }

    // ジャンプ
    public void Jump()
    {
        if (myPV.isMine)
        {
            myRB.velocity = new Vector3(GetMoveDirection().x, jumpForce, GetMoveDirection().z);
            // myRB.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
        }
    }

    // 攻撃入力
    public void OnClickAttack()
    {
        if (myPV.isMine)
        {
            Vector3 posUp = transform.position + new Vector3(0, 2, 0);
            myPV.RPC("Attack", PhotonTargets.All, posUp, weaponPower);
        }
    }

    // 攻撃
    [PunRPC]
    private void Attack(Vector3 pos, float power)
    {
        GameObject weapon = Instantiate(weaponPrefab, pos, Quaternion.identity);
        weapon.GetComponent<Rigidbody>().AddForce(Vector3.up * power);
    }

    private void RotateCamera()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotateSpeed,
            Input.GetAxis("Mouse Y") * rotateSpeed,
            0
        );

        myCamera.transform.RotateAround(transform.position, Vector3.up, angle.x);
        myCamera.transform.RotateAround(transform.position, myCamera.transform.right, angle.y * -1);
    }

    // hp変更
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

    // 回復
    public void Recover()
    {
    }

    // カバンを開く
    public void OpenBag()
    {

    }

}
