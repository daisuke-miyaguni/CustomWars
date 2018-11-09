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
    private Button inventoryButton;
    private Button avoidButton;
    private GameObject inventory;

    private const int maxHP = 100;
    private int currentHP = maxHP;
    [SerializeField] private Slider hpSlider;

    // playerステータス
    [SerializeField] private float moveSpeed;
    // [SerializeField] private float playerHP;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject weaponPrefab;
    private MobileInputController controller;

    private List<string> itemList = new List<string>();

    int damage = 10;
    int healing = 2;
    private float weaponPower = 200;

    [SerializeField] private float angleMax;
    [SerializeField] private float angleMin;

    private bool isJump;

    private GameObject playerCamera;
    Vector2 startPos;
    Vector3 targetPos;



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
            // インベントリボタン取得、設定
            inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();
            inventoryButton.onClick.AddListener(this.OpenInventory);
            // 回避ボタン取得、設定
            avoidButton = GameObject.Find("AvoidButton").GetComponent<Button>();
            inventoryButton.onClick.AddListener(this.Avoid);


            // カメラ取得、位置調整
            myCamera = Camera.main;
            // myCamera.transform.parent = transform;
            // myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);

            // playerCamera = GameObject.Find("Camera");
            // playerCamera.transform.parent = transform;
            // playerCamera.transform.position = transform.position;



            //hp初期値設定
            currentHP = maxHP;
            hpSlider.value = currentHP;

            inventory = GameObject.Find("Inventory");
            inventory.SetActive(false);

            isJump = true;


            targetPos = gameObject.transform.position;
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
            // RotateCamera();
        }

        // if (myPV.isMine)
        // {
        //     if (currentHP <= 0)
        //     {
        //         myPV.RPC("Death", PhotonTargets.All);
        //     }
        // }
    }


    // 移動処理
    private void Move()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * GetMoveDirection().z + Camera.main.transform.right * GetMoveDirection().x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        myRB.velocity = moveForward * moveSpeed + new Vector3(0, myRB.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }


        // //ジョイスティックが傾いている方向を向く
        // Vector3 direction = GetMoveDirection();
        // if (GetMoveDirection().x != 0 || GetMoveDirection().z != 0)
        // {
        //     transform.localRotation = Quaternion.LookRotation(direction);
        //     transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        // }


        // myRB.velocity = new Vector3(
        //     GetMoveDirection().x * moveSpeed,
        //     myRB.velocity.y,
        //     GetMoveDirection().z * moveSpeed
        // );
    }

    // 移動方向取得
    private Vector3 GetMoveDirection()
    {
        // float x = controller.Horizontal;
        // float z = controller.Vertical;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        return new Vector3(x, 0, z);
    }

    // カメラ操作
    private void RotateCamera()
    {
        myCamera.transform.position += gameObject.transform.position - targetPos;
        targetPos = gameObject.transform.position;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            myCamera.transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            // myCamera.transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);




            // playerCamera.transform.position = transform.position;


            // if (Input.touchCount == 1)
            // {
            //     //回転
            //     Touch t1 = Input.GetTouch(0);
            //     if (t1.phase == TouchPhase.Began)
            //     {
            //         startPos = t1.position;
            //     }
            //     else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary)
            //     {
            //         float tx = t1.position.x - startPos.x; //横移動量(-1<tx<1)
            //         float ty = t1.position.y - startPos.y; //縦移動量(-1<ty<1)
            //         print(new Vector2(tx, ty).normalized);
            //         Vector2 angle = new Vector2(tx, ty).normalized;


            //         playerCamera.transform.eulerAngles += new Vector3(angle.y, angle.x, 0);


            //         float angle_x = 180f <= playerCamera.transform.eulerAngles.x ? playerCamera.transform.eulerAngles.x - 360 : playerCamera.transform.eulerAngles.x;
            //         playerCamera.transform.eulerAngles = new Vector3(
            //             Mathf.Clamp(angle_x, angleMin, angleMax),
            //             playerCamera.transform.eulerAngles.y,
            //             playerCamera.transform.eulerAngles.z
            //         );


            // if (Input.GetMouseButton(1))
            // {
            //     Vector3 angle = new Vector3(
            //         Input.GetAxis("Mouse X") * rotateSpeed,
            //         Input.GetAxis("Mouse Y") * rotateSpeed,
            //         0
            //     );
            //     print(angle);

            //     playerCamera.transform.eulerAngles += new Vector3(angle.y, angle.x, angle.z);


            //     float angle_x = 180f <= playerCamera.transform.eulerAngles.x ? playerCamera.transform.eulerAngles.x - 360 : playerCamera.transform.eulerAngles.x;
            //     playerCamera.transform.eulerAngles = new Vector3(
            //         Mathf.Clamp(angle_x, angleMin, angleMax),
            //         playerCamera.transform.eulerAngles.y,
            //         playerCamera.transform.eulerAngles.z
            //     );
        }




        // Vector3 angle = new Vector3(
        //     Input.GetAxis("Mouse X") * rotateSpeed,
        //     Input.GetAxis("Mouse Y") * rotateSpeed * -1,
        //     0
        // );

        // myCamera.transform.RotateAround(transform.position, Vector3.up, angle.x);

        // float rotationX = myCamera.transform.rotation.x;
        // if (rotationX < angleMax && rotationX > angleMin)
        // {
        //     myCamera.transform.RotateAround(transform.position, myCamera.transform.right, angle.y);
        // }


    }

    // ジャンプ
    public void Jump()
    {
        // if (myPV.isMine)
        // {
        if (isJump)
        {
            myRB.velocity = new Vector3(GetMoveDirection().x, jumpForce, GetMoveDirection().z);
            isJump = false;
        }
        // }
    }

    // 攻撃入力
    public void OnClickAttack()
    {
        Vector3 posUp = transform.position + new Vector3(0, 2, 0);
        myPV.RPC("Attack", PhotonTargets.All, posUp, weaponPower);
        // if (myPV.isMine)
        // {
        //     Vector3 posUp = transform.position + new Vector3(0, 2, 0);
        //     myPV.RPC("Attack", PhotonTargets.All, posUp, weaponPower);
        // }
    }

    // 攻撃
    [PunRPC]
    private void Attack(Vector3 pos, float power)
    {
        GameObject weapon = Instantiate(weaponPrefab, pos, Quaternion.identity);
        weapon.GetComponent<Rigidbody>().AddForce(Vector3.up * power);
    }



    // ダメージを受ける
    [PunRPC]
    private void TakeDamage(int amount)
    {
        currentHP -= amount;
        hpSlider.value = currentHP;

        if (currentHP <= 0)
        {
            currentHP = 0;
            myPV.RPC("Death", PhotonTargets.Others);
        }
    }

    // 回復
    [PunRPC]
    public void Recover(int amount)
    {
        if (currentHP >= maxHP)
        {
            return;
        }
        currentHP += amount;
        hpSlider.value = currentHP;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (myPV.isMine)
        {
            // 被弾
            if (other.gameObject.tag == "weapon")
            {
                myPV.RPC("TakeDamage", PhotonTargets.All, damage);
            }
            // アイテム取得
            if (other.gameObject.tag == "Item")
            {
                itemList.Add(other.gameObject.name);
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Stage")
            {
                isJump = true;
            }
            // ステージ外判定
            if (other.gameObject.tag == "AreaOut")
            {
                myPV.RPC("Death", PhotonTargets.All);
            }
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



    // カバンを開く
    public void OpenInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }

    }

}
