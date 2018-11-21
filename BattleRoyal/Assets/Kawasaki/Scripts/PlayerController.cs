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
    // private Button attackButton;
    // private Button jumpButton;
    // private Button itemButton;
    // private Button inventoryButton;
    // private Button avoidButton;
    // private Button parryButton;
    private Text hpText;
    private GameObject inventory;
    PlayerUIController playerUIController;


    private const int maxHP = 100;
    [SerializeField] private int currentHP = maxHP;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject otherHpBar;

    // playerステータス
    [SerializeField] private float moveSpeed;
    // [SerializeField] private float playerHP;
    // [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpForce;

    private MobileInputController controller;

    private List<string> itemList = new List<string>();

    private Animator animator;

    int damage = 10;
    int healing = 2;
    private float weaponPower = 200;
    BoxCollider weaponCollider;

    // player parry情報
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private bool parryState;
    [SerializeField] private float parryTime = 0.5f;
    [SerializeField] private int wasparryedDamage = 15;
    [SerializeField] private float rebelliousTime = 0.3f;

    [SerializeField] private float angleMax;
    [SerializeField] private float angleMin;

    private bool isJump;

    private GameObject playerCamera;
    Vector2 startPos;
    Vector3 targetPos;

    // アイテム周り
    private MyItemStatus myItemStatus;

    public MyItemStatus GetMyItemStatus()
    {
        return myItemStatus;
    }

    void Awake()
    {
        // photonview取得
        myPV = GetComponent<PhotonView>();
        // myItemStatus取得
        myItemStatus = GetComponent<MyItemStatus>();
    }

    void Start()
    {
        sphereCollider = gameObject.transform.Find("ParryScale").GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        parryState = sphereCollider.enabled;
        weaponCollider = weapon.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        animator = weapon.GetComponent<Animator>();

        if (myPV.isMine)
        {
            playerUIController = GameObject.Find("PlayerControllerUI").GetComponent<PlayerUIController>();
            playerUIController.SetPlayerController(this);
            // rigidbody取得
            myRB = GetComponent<Rigidbody>();
            // 左スティック取得
            controller = GameObject.Find("LeftJoyStick").GetComponent<MobileInputController>();
            // // 攻撃ボタン取得、設定
            // attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
            // attackButton.onClick.AddListener(this.OnClickAttack);
            // // ジャンプボタン取得、設定
            // jumpButton = GameObject.Find("JumpButton").GetComponent<Button>();
            // jumpButton.onClick.AddListener(this.Jump);
            // // インベントリボタン取得、設定
            // inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();
            // inventoryButton.onClick.AddListener(this.OpenInventory);
            // // 回避ボタン取得、設定
            // avoidButton = GameObject.Find("AvoidButton").GetComponent<Button>();
            // avoidButton.onClick.AddListener(this.Avoid);
            // // パリイボタン取得、設定
            // parryButton = GameObject.Find("ParryButton").GetComponent<Button>();
            // parryButton.onClick.AddListener(this.ParryClick);

            // カメラ取得、位置調整
            // myCamera = Camera.main;
            // myCamera.transform.parent = transform;
            // myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);

            // playerCamera = GameObject.Find("Camera");
            // playerCamera.transform.parent = transform;
            // playerCamera.transform.position = transform.position;

            //hp初期値設定
            currentHP = maxHP;
            hpSlider.value = currentHP;
            hpText = GameObject.Find("HPText").GetComponent<Text>();
            hpText.text = "HP: " + currentHP.ToString();
            otherHpBar.SetActive(false);
            myPV.RPC("Hpbar", PhotonTargets.OthersBuffered);

            isJump = true;

            targetPos = gameObject.transform.position;
        }
    }

    [PunRPC]
    void Hpber()
    {
        otherHpBar.SetActive(true);
    }

    void FixedUpdate()
    {
        if (myPV.isMine)
        {
            Move();
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        sphereCollider.enabled = parryState;

        if (stream.isWriting)
        {
            stream.SendNext(parryState);
        }
        else
        {
            parryState = (bool)stream.ReceiveNext();
        }

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
    public Vector3 GetMoveDirection()
    {
        float x = controller.Horizontal;
        float z = controller.Vertical;
        // float x = Input.GetAxis("Horizontal");
        // float z = Input.GetAxis("Vertical");

        return new Vector3(x, 0, z);
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
        myPV.RPC("CallAttack", PhotonTargets.AllViaServer/*, posUp, weaponPower */);
        // if (myPV.isMine)
        // {
        //     Vector3 posUp = transform.position + new Vector3(0, 2, 0);
        //     myPV.RPC("Attack", PhotonTargets.All, posUp, weaponPower);
        // }
    }

    // 攻撃
    [PunRPC]
    private void CallAttack(/*Vector3 pos, float power */)
    {
        // GameObject weapon = Instantiate(weaponPrefab, pos, Quaternion.identity);
        // weapon.GetComponent<Rigidbody>().AddForce(Vector3.up * power);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        weaponCollider.enabled = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(parryTime);
        weaponCollider.enabled = false;
        animator.ResetTrigger("Attack");
    }

    // ダメージを受ける
    [PunRPC]
    private void TakeDamage(int amount)
    {
        currentHP -= amount;
        hpSlider.value = currentHP;

        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            myPV.RPC("Death", PhotonTargets.AllViaServer);
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

    private void OnCollisionEnter(Collision other)
    {
        if (myPV.isMine)
        {
            // 着地
            if (other.gameObject.tag == "Stage")
            {
                isJump = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myPV.isMine)
        {
            // 被弾
            if (other.gameObject.tag == "weapon"
            && other.gameObject != weapon)
            {
                myPV.RPC("TakeDamage", PhotonTargets.AllViaServer, damage);
            }
            // アイテム取得
            if (other.gameObject.tag == "Item")
            {
                itemList.Add(other.gameObject.name);
                Destroy(other.gameObject);
            }
            // ステージ外判定
            if (other.gameObject.tag == "AreaOut")
            {
                myPV.RPC("Death", PhotonTargets.AllViaServer);
            }
        }
    }

    // 死亡
    [PunRPC]
    private void Death()
    {
        Destroy(gameObject);
        // PhotonNetwork.Disconnect();
        if (myPV.isMine)
        {
            // SceneManager.LoadScene(3);
            MainSceneManager mainSceneManager = GameObject.Find("MainManager").GetComponent<MainSceneManager>();
            mainSceneManager.GoToResult(1);
        }
    }

    // // 回避
    // public void Avoid()
    // {
    // }



    public void ParryClick()
    {
        if (myPV.isMine)
        {
            playerUIController.parryButton.interactable = false;
            myPV.RPC("CallParry", PhotonTargets.AllViaServer);
        }
    }

    [PunRPC]
    void CallParry()
    {
        StartCoroutine(Parrying());
    }

    IEnumerator Parrying()
    {
        sphereCollider.enabled = true;
        parryState = true;
        yield return new WaitForSeconds(parryTime);
        sphereCollider.enabled = false;
        parryState = false;
        playerUIController.parryButton.interactable = true;
    }

    public void CallWasparryed()
    {
        myPV.RPC("Wasparryed", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    void Wasparryed()
    {
        currentHP -= wasparryedDamage;
        hpSlider.value = currentHP;
        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
            StartCoroutine(Rebellious());
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            myPV.RPC("Death", PhotonTargets.Others);
        }
    }

    IEnumerator Rebellious()
    {
        // Parryの操作制御
        playerUIController.attackButton.interactable = false;
        // playerUIController.avoidButton.interactable = false;
        playerUIController.jumpButton.interactable = false;
        playerUIController.parryButton.interactable = false;
        yield return new WaitForSeconds(rebelliousTime);
        playerUIController.attackButton.interactable = true;
        // playerUIController.avoidButton.interactable = true;
        playerUIController.jumpButton.interactable = true;
        playerUIController.parryButton.interactable = true;
    }
}