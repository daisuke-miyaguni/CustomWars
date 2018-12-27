using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PhotonView myPV;
    private PhotonTransformView myPTV;
    private Rigidbody myRB;
    private Camera myCamera;
    private Text hpText;
    private GameObject inventory;
    PlayerUIController playerUIController;

    private const int maxHP = 100;
    [SerializeField] private int currentHP = maxHP;
    private Slider hpSlider;
    [SerializeField] private GameObject otherHpBar;
    [SerializeField] private Slider otherHpBarSlider;

    private GameObject itemBox;

    [SerializeField] private GameObject weapon;
    CapsuleCollider weaponCollider;
    WeaponManager myWM;

    // playerステータス
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    //[SerializeField] private float attackStayTime = 0.2f;
    //[SerializeField] private float attackTime = 0.1f;

    private MobileInputController controller;

    private List<string> itemList = new List<string>();

    private Animator animator;

    private enum GameObjectTags
    {
        weapon,
        Desk,
        Item,
        ItemBox,
        AreaOut
    }

    int idleState = Animator.StringToHash("Base Layer.idle");

    int[] jumpState = new int[2]
    {
        Animator.StringToHash("Base Layer.jump_idle"),
        Animator.StringToHash("Base Layer.jump_run"),
    };

    int attackState = Animator.StringToHash("Base Layer.attack1");

    int parryState = Animator.StringToHash("Base Layer.parry");

    int runState = Animator.StringToHash("Base Layer.run");
 
    //int[] atkState = new int[3] {
    //                            Animator.StringToHash("Base Layer.attack1"),
    //                            Animator.StringToHash("Base Layer.attack2"),
    //                            Animator.StringToHash("Base Layer.attack")
    //                           };

    // player parry情報
    [SerializeField] private GameObject parry;
    private SphereCollider parryCollider;
    //[SerializeField] private float parryStayTime = 0.3f;
    //[SerializeField] private float parryTime = 0.5f;
    [SerializeField] private float wasparryedDamage = 15;
    private bool parryCollision = false;

    [SerializeField] private float angleMax;
    [SerializeField] private float angleMin;

    private bool isJump;

    private GameObject playerCamera;
    Vector2 startPos;

    // アイテム周り
    private MyItemStatus myItemStatus;

    public MyItemStatus GetMyItemStatus()
    {
        return myItemStatus;
    }

    //追加
    public int GetPlayerHp()
    {
        return currentHP;
    }

    Vector3 weaponPos;

    void Awake()
    {
        // photonview取得
        myPV = GetComponent<PhotonView>();

    }

    void Start()
    {   
        // アニメーター取得
        animator = GetComponent<Animator>();
        // parry当たり判定設定、コライダー取得
        parryCollider = parry.GetComponent<SphereCollider>();
        parryCollider.enabled = false;
        // 武器の初期位置
        weaponPos = weapon.transform.localPosition;
        // WeaponManagerの取得
        myWM = weapon.GetComponent<WeaponManager>();
        weaponCollider = weapon.GetComponent<CapsuleCollider>();

        if (myPV.isMine)
        {
            // Player の向きを中央にする
            // transform.LookAt(Vector3.zero);
            // myItemStatus取得
            myItemStatus = GetComponent<MyItemStatus>();
            // photontransformview取得
            myPTV = GetComponent<PhotonTransformView>();
            playerUIController = GameObject.FindWithTag("PlayerControllerUI").gameObject.GetComponent<PlayerUIController>();
            playerUIController.SetPlayerController(this);
            // rigidbody取得
            myRB = this.gameObject.GetComponent<Rigidbody>();
            // 左スティック取得
            controller = GameObject.Find("LeftJoyStick").gameObject.GetComponent<MobileInputController>();
         
            //hp初期値設定
            currentHP = maxHP;
            hpSlider = playerUIController.GetHPSlider();
            hpSlider.value = currentHP;
            hpText = GameObject.Find("HPText").gameObject.GetComponent<Text>();
            hpText.text = "HP: " + currentHP.ToString();
            otherHpBar.SetActive(false);
            myPV.RPC("Hpbar", PhotonTargets.OthersBuffered);
            // ジャンプ判定の初期化
            isJump = true;
        }
 
    }

    [PunRPC]
    void Hpber()
    {
        otherHpBar.SetActive(true);
        otherHpBarSlider.value = currentHP;
    }


    void FixedUpdate()
    {
        if (myPV.isMine)
        {
            // 位置補完
            Vector3 velocity = myRB.velocity;
            myPTV.SetSynchronizedValues(speed: velocity, turnSpeed: 0);
            // 移動処理の読み込み
            Move();
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
        myRB.velocity += (moveForward * moveSpeed)/* + new Vector3(0, myRB.velocity.y, 0)*/ ;

        if (moveForward == Vector3.zero)
        {
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("run", true);
        }

        weapon.transform.localPosition = weaponPos;
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);


        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    // 移動方向取得
    public Vector3 GetMoveDirection()
    {
        float x = controller.Horizontal;
        float z = controller.Vertical;

        return new Vector3(x, 0, z);
    }

    // ジャンプ
    public void Jump()
    {
        // パリィ状態か攻撃状態のときはジャンプを呼ばない
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == parryState
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackState)
        {
            return;
        }
        if (isJump)
        {
            // ジャンプ中にジャンプボタンを押せなくする
            playerUIController.jumpButton.interactable = false;
            // ジャンプアニメーション同期処理の呼び出し
            myPV.RPC("SyncJumpAnim", PhotonTargets.AllViaServer);
            // 武器の位置を初期化
            weapon.transform.localPosition = weaponPos;
            // 武器の角度を初期化
            weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            // ジャンプを不可能にする
            isJump = false;
        }
    }

    // ジャンプアニメーションの同期
    [PunRPC]
    void SyncJumpAnim()
    {
        // ジャンプアニメーションの再生
        animator.SetTrigger("jump");
    }

    // 攻撃入力
    public void OnClickAttack()
    {
        // 現在のアニメーション状態の取得
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        // アイドル状態か、走り状態か、パリィ状態の時にのみ攻撃ボタンで攻撃を呼ぶ
        if (currentState.fullPathHash == idleState
        || currentState.fullPathHash == runState
        || currentState.fullPathHash == parryState)
        {
            myPV.RPC("CallAttack", PhotonTargets.AllViaServer);
        }
        else
        {
            return;
        }
    }

    // 攻撃処理を呼び出す
    [PunRPC]
    private void CallAttack()
    {
        animator.SetFloat("Speed", myWM.GetWeaponSpeed());
        animator.SetTrigger("attack1");
        // 武器の位置の初期化
        weapon.transform.localPosition = weaponPos;
        // 武器の角度の初期化
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void OnWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    private void OffWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

    // ダメージを受ける
    [PunRPC]
    private void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            currentHP = 0;
            myPV.RPC("Death", PhotonTargets.AllViaServer);
        }

        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
        }

        hpSlider.value = currentHP;
        otherHpBarSlider.value = currentHP;
    }

    public void CallRecover(int heal)
    {
        myPV.RPC("Recover", PhotonTargets.AllViaServer, heal);
    }

    // 回復
    [PunRPC]
    private void Recover(int amount)
    {
        //ここで使えない処理をすると、アイテム使用時にアイテムが消えるだけになってしまうのでコメントアウトしてます。
        /*if (currentHP >= maxHP)       
        {
            return;
        }*/

        currentHP += amount;

        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }

        hpSlider.value = currentHP;
        otherHpBarSlider.value = currentHP;
        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (myPV.isMine)
        {
            // 着地
            if (other.gameObject.layer == 14 || other.gameObject.layer == 15)
            {
                isJump = true;
                playerUIController.jumpButton.interactable = true;
                weapon.transform.localPosition = weaponPos;
                weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myPV.isMine)
        {
            // 被弾
            if (other.gameObject.tag == GameObjectTags.weapon.ToString()
            && other.gameObject != weapon)
            {
                WeaponManager wm = other.gameObject.GetComponent<WeaponManager>();
                int damage = Mathf.CeilToInt(wm.GetWeaponPower() * myWM.GetWeaponDefense());
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                myPV.RPC("TakeDamage", PhotonTargets.AllViaServer, damage);
            }
            // アイテム取得
            if (other.gameObject.tag == GameObjectTags.Item.ToString())
            {
                itemList.Add(other.gameObject.name);
            }
            // ステージ外判定
            if (other.gameObject.tag == GameObjectTags.AreaOut.ToString())
            {
                myPV.RPC("Death", PhotonTargets.AllViaServer);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myPV.isMine)
        {
            if (other.gameObject.tag == GameObjectTags.ItemBox.ToString())
            {
                itemBox = other.gameObject;
                playerUIController.openButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myPV.isMine)
        {
            if (other.gameObject.tag == GameObjectTags.ItemBox.ToString())
            {
                itemBox = null;
                playerUIController.openButton.gameObject.SetActive(false);
            }
        }
    }

    public void OnClickOpenButton()
    {
        if (myPV.isMine)
        {
            playerUIController.openButton.gameObject.SetActive(false);
        }
        itemBox.transform.root.gameObject.GetComponent<ItemBox>().OpenOnClick();
        itemBox = null;
    }

    // 死亡
    [PunRPC]
    private void Death()
    {
        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
            hpSlider.value = currentHP;
            MainSceneManager mainSceneManager = GameObject.Find("MainManager").GetComponent<MainSceneManager>();
            mainSceneManager.GoToResult(false);
            Destroy(GameObject.FindWithTag("PlayerControllerUI"));
        }
        Destroy(gameObject);
    }

    public void ParryClick()
    {
        // 攻撃状態とジャンプ状態のときに押せなくする
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackState
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == jumpState[0]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == jumpState[1])
        {
            return;
        }
        if (myPV.isMine)
        {
            playerUIController.parryButton.interactable = false;
            myPV.RPC("CallParry", PhotonTargets.AllViaServer);
        }
    }

    [PunRPC]
    void CallParry()
    {
        animator.SetTrigger("parry");
        // 武器の位置の初期化
        weapon.transform.localPosition = weaponPos;
        // 武器の角度の初期化
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void OnParry()
    {
        parryCollider.enabled = true;
        playerUIController.parryButton.interactable = false;
    }

    void OffParry()
    {
        parryCollider.enabled = false;
        playerUIController.parryButton.interactable = true;
    }

    public void CallWasparryed()
    {
        myPV.RPC("Wasparryed", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    void Wasparryed()
    {
        animator.SetTrigger("desprate");
        currentHP -= Mathf.CeilToInt(wasparryedDamage);

        if (currentHP <= 0)
        {
            currentHP = 0;
            myPV.RPC("Death", PhotonTargets.AllViaServer);
        }

        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
            animator.SetTrigger("desperate");
            // 武器の位置の初期化
            weapon.transform.localPosition = weaponPos;
            // 武器の角度の初期化
            weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }

        hpSlider.value = currentHP;
        otherHpBarSlider.value = currentHP;
    }

    void desperating()
    {
        playerUIController.attackButton.interactable = false;
        playerUIController.jumpButton.interactable = false;
        playerUIController.parryButton.interactable = false;
    }

    void desperated()
    {
        playerUIController.attackButton.interactable = true;
        playerUIController.jumpButton.interactable = true;
        playerUIController.parryButton.interactable = true;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(parryCollision);
        }
        else
        {
            parryCollision = (bool)stream.ReceiveNext();
            parryCollider.enabled = parryCollision;
        }
    }
}