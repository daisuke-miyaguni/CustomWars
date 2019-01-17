using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PhotonView myPV;
    //private PhotonTransformView myPTV;
    private Rigidbody myRB;
    private Camera myCamera;
    private Text hpText;
    private GameObject inventory;
    PlayerUIController playerUIController;

    Vector3 velocity;

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

    private MobileInputController controller;

    private List<string> itemList = new List<string>();

    private Animator animator;

    [SerializeField] private float firstComboEffectiveTime = 0.7f;
    [SerializeField] private float secondComboEffectiveTime = 0.5f;

    int[] jumpStates = new int[2]
    {
        Animator.StringToHash("Base Layer.jump_idle"),
        Animator.StringToHash("Base Layer.jump_run"),
    };

    int[] attackStates = new int[3]
    {
        Animator.StringToHash("Base Layer.attack1"),
        Animator.StringToHash("Base Layer.attack2"),
        Animator.StringToHash("Base Layer.attack3")
    };

    int parryState = Animator.StringToHash("Base Layer.parry");

    // player parry情報
    [SerializeField] private GameObject parry;
    private SphereCollider parryCollider;
    [SerializeField] private float wasparryedDamage = 15.0f;
    private bool parryCollision = false;
    private bool parring = false;

    private bool jumping = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask bookLayer;

    private bool desperate = false;

    [SerializeField] private float angleMax;
    [SerializeField] private float angleMin;

    private GameObject playerCamera;
    Vector2 startPos;

    // アイテム周り
    private MyItemStatus myItemStatus;

    private ItemSpawner itemSpawner;

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
        itemSpawner = GameObject.FindWithTag("ItemSpawner").gameObject.GetComponent<ItemSpawner>();

        Debug.Log(itemSpawner);


        if (myPV.isMine)
        {
            // myItemStatus取得
            myItemStatus = GetComponent<MyItemStatus>();
            // photontransformview取得
            //myPTV = GetComponent<PhotonTransformView>();
            playerUIController = GameObject.FindWithTag("PlayerControllerUI").gameObject.GetComponent<PlayerUIController>();
            playerUIController.SetPlayerController(this);
            // rigidbody取得
            myRB = this.gameObject.GetComponent<Rigidbody>();
            // 左スティック取得
            controller = GameObject.Find("LeftJoyStick").gameObject.GetComponent<MobileInputController>();

            parring = false;

            //hp初期値設定
            currentHP = maxHP;
            hpSlider = playerUIController.GetHPSlider();
            hpSlider.value = currentHP;
            hpText = GameObject.Find("HPText").gameObject.GetComponent<Text>();
            hpText.text = "HP: " + currentHP.ToString();
            otherHpBar.SetActive(false);
            myPV.RPC("Hpbar", PhotonTargets.OthersBuffered);
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
            //// 位置補間
            //velocity = myRB.velocity;
            //myPTV.SetSynchronizedValues(speed: velocity, turnSpeed: 0);
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
        myRB.velocity += (moveForward * moveSpeed);

        if (moveForward == Vector3.zero)
        {
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("run", true);
        }

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

        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.jump_idle")
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[0]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[1]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[2]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == parryState)
        {
            return Vector3.zero;
        }

        return new Vector3(x, 0, z);
    }

    private void Update()
    {
        if(myPV.isMine)
        {
            JumpCheck();
        }
    }

    void JumpCheck()
    {
        // ジャンプ状態の更新
        jumping = Physics.Linecast(
            transform.position + (transform.up * 1.25f),
            transform.position - (transform.up * 0.03f),
            groundLayer
        );

        if (!jumping)
        {
            playerUIController.attackMiyaguniButton.interactable = false;
            playerUIController.parryMiyaguniButton.interactable = false;
            playerUIController.jumpMiyaguniButton.interactable = false;
        }
        else
        {
            playerUIController.attackMiyaguniButton.interactable = true;
            playerUIController.parryMiyaguniButton.interactable = true;
            playerUIController.jumpMiyaguniButton.interactable = true;
        }
    }

    // ジャンプ
    public void Jump()
    {
        // パリィ状態か攻撃状態のときはジャンプを呼ばない
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == parryState
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[0]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[1]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[2]
        || !jumping
        || desperate)
        {
            return;
        }

        // ジャンプアニメーション同期処理の呼び出し
        myPV.RPC("SyncJumpAnim", PhotonTargets.AllViaServer);
        AudioManager.Instance.PlaySE("highspeed-movement1");
    }

    // ジャンプアニメーションの同期
    [PunRPC]
    void SyncJumpAnim()
    {
        // ジャンプアニメーションの再生
        animator.SetTrigger("jump");
        // 武器の位置を初期化
        weapon.transform.localPosition = weaponPos;
        // 武器の角度を初期化
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    // 攻撃入力
    public void OnClickAttack()
    {
        if (animator.IsInTransition(0))
        {
            return;
        }
        // 現在のアニメーション状態の取得
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        // アイドル状態か、走り状態か、パリィ状態の時にのみ攻撃ボタンで攻撃を呼ぶ
        if (currentState.IsName("idle")
        || currentState.IsName("run")
        || currentState.IsName("parry")
        || currentState.IsName("attack1")
        || currentState.IsName("attack2"))
        {
            if (myPV.isMine)
            {
                playerUIController.jumpMiyaguniButton.interactable = false;
                playerUIController.parryMiyaguniButton.interactable = false;
            }
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
        // 現在のアニメーション状態の取得
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName("attack1") && currentState.normalizedTime < firstComboEffectiveTime)
        {
            animator.SetTrigger("attack2");
        }
        else if (currentState.IsName("attack2") && currentState.normalizedTime < secondComboEffectiveTime)
        {
            animator.SetTrigger("attack3");
        }
        else if (!currentState.IsName("attack1") && !currentState.IsName("attack2"))
        {
            animator.SetTrigger("attack1");
        }
        // 武器の位置の初期化
        weapon.transform.localPosition = weaponPos;
        // 武器の角度の初期化
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void OnWeaponCollider()
    {
        weaponCollider.enabled = true;
        if(myPV.isMine)
        {
            playerUIController.jumpMiyaguniButton.interactable = true;
            playerUIController.parryMiyaguniButton.interactable = true;
        }
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
            hpSlider.value = currentHP;
        }
        else
        {
            otherHpBarSlider.value = currentHP;
        }
    }

    public void CallRecover(int heal)
    {
        AudioManager.Instance.PlaySE("magic-status-cure1");
        myPV.RPC("Recover", PhotonTargets.AllViaServer, heal);
    }

    // 回復
    [PunRPC]
    private void Recover(int amount)
    {
        currentHP += amount;

        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }

        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
            hpSlider.value = currentHP;
        }
        else
        {
            otherHpBarSlider.value = currentHP;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myPV.isMine)
        {
            switch(other.gameObject.tag)
            {
                // 被弾
                case "weapon":
                    if(other.gameObject != weapon && !parring)
                    {
                        WeaponManager wm = other.gameObject.GetComponent<WeaponManager>();
                        int damage = Mathf.CeilToInt(wm.GetWeaponPower() * myWM.GetWeaponDefense());
                        other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                        myPV.RPC("TakeDamage", PhotonTargets.AllViaServer, damage);
                    }
                    break;
                // アイテム
                case "Item":
                    itemList.Add(other.gameObject.name);
                    break;
                // ステージ外処理
                case "AreaOut":
                    myPV.RPC("Death", PhotonTargets.AllViaServer);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myPV.isMine)
        {
            if (other.gameObject.tag == "ItemBox")
            {
                itemBox = other.gameObject;
                playerUIController.openMiyaguniButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myPV.isMine)
        {
            switch(other.gameObject.tag)
            {
                case "ItemBox":
                    itemBox = null;
                    playerUIController.openMiyaguniButton.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public void OnClickOpenButton()
    {
        if (myPV.isMine)
        {
            playerUIController.openMiyaguniButton.gameObject.SetActive(false);
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
            MainSceneManager mainSceneManager = GameObject.FindWithTag("GameController").GetComponent<MainSceneManager>();
            mainSceneManager.GoToResult(false);
            itemSpawner.CallItemSpawn(this.gameObject, gameObject.transform.position, 0);       //死亡時にアイテムを落とす
            Destroy(GameObject.Find("PlayerControllerUI"));
        }
        Destroy(gameObject);
    }

    public void ParryClick()
    {
        // 攻撃状態とジャンプ状態のときに押せなくする
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[0]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[1]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackStates[2]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == jumpStates[0]
        || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == jumpStates[1]
        || parring
        || !jumping
        || desperate)
        {
            return;
        }
        if (myPV.isMine)
        {
            playerUIController.jumpMiyaguniButton.interactable = false;
            playerUIController.attackMiyaguniButton.interactable = false;
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
        parring = true;
        if(myPV.isMine)
        {
            playerUIController.jumpMiyaguniButton.interactable = true;
            playerUIController.attackMiyaguniButton.interactable = true;
        }
    }

    void OffParry()
    {
        parryCollider.enabled = false;
        parring = false;
    }

    public void CallWasparryed()
    {
        myPV.RPC("Wasparryed", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    void Wasparryed()
    {
        animator.SetTrigger("desperate");
        currentHP -= Mathf.CeilToInt(wasparryedDamage);

        if (currentHP <= 0)
        {
            currentHP = 0;
            myPV.RPC("Death", PhotonTargets.AllViaServer);
        }

        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
            hpSlider.value = currentHP;
            // 武器の位置の初期化
            weapon.transform.localPosition = weaponPos;
            // 武器の角度の初期化
            weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            otherHpBarSlider.value = currentHP;
        }
    }

    void desperating()
    {
        weaponCollider.enabled = false;
        desperate = true;
    }

    void desperated()
    {
        desperate = false;
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