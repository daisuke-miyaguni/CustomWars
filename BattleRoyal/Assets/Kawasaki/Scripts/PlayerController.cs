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
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject otherHpBar;

    [SerializeField] private GameObject itemBox;

    WeaponManager myWM;

    // playerステータス
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float attackStayTime = 0.2f;
    [SerializeField] private float attackTime = 0.1f;

    private MobileInputController controller;

    private List<string> itemList = new List<string>();

    private Animator animator;

    private enum PlayerAnimatorController
    {
        player_pencil,
        player_eraser,
        player_ruler
    }


    private enum PlayerAnimatorParameters
    {
        run,
        jump,
        parry,
        desprate,
        attack
    }

    private enum GameObjectTags
    {
        weapon,
        Desk,
        Item,
        ItemBox,
        AreaOut
    }

    CapsuleCollider weaponCollider;

    // player parry情報
    [SerializeField] private SphereCollider parryCollider;
    [SerializeField] private float parryStayTime = 0.3f;
    [SerializeField] private float parryTime = 0.5f;
    [SerializeField] private float wasparryedDamage = 15;
    [SerializeField] private float rebelliousTime = 0.3f;
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
        parryCollider = parryCollider.GetComponent<SphereCollider>();
        parryCollider.enabled = false;
        // weaponCollider = weapon.GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        weaponCollider = weapon.GetComponent<CapsuleCollider>();

        if (myPV.isMine)
        {
            // myItemStatus取得
            myItemStatus = GetComponent<MyItemStatus>();
            // photontransformview取得
            myPTV = GetComponent<PhotonTransformView>();
            playerUIController = GameObject.Find("PlayerControllerUI").GetComponent<PlayerUIController>();
            playerUIController.SetPlayerController(this);
            // rigidbody取得
            myRB = this.gameObject.GetComponent<Rigidbody>();
            // 左スティック取得
            controller = GameObject.Find("LeftJoyStick").gameObject.GetComponent<MobileInputController>();

            //hp初期値設定
            currentHP = maxHP;
            hpSlider = playerUIController.GetHPSlider();
            hpSlider.value = currentHP;
            hpText = GameObject.Find("HPText").GetComponent<Text>();
            hpText.text = "HP: " + currentHP.ToString();
            otherHpBar.SetActive(false);
            // myPV.RPC("Hpbar", PhotonTargets.OthersBuffered);

            isJump = true;

            weaponPos = weapon.transform.localPosition;

            transform.LookAt(Vector3.zero);

            myWM = weapon.GetComponent<WeaponManager>();
        }
    }

    // [PunRPC]
    // void Hpber()
    // {
    //     otherHpBar.SetActive(true);
    // }


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
            animator.SetBool(PlayerAnimatorParameters.run.ToString(), false);
        }
        else
        {
            animator.SetBool(PlayerAnimatorParameters.run.ToString(), true);
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
        if (isJump)
        {
            playerUIController.jumpButton.interactable = false;
            animator.SetTrigger(PlayerAnimatorParameters.jump.ToString());
            weapon.transform.localPosition = weaponPos;
            weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            isJump = false;
        }
    }

    // 攻撃入力
    public void OnClickAttack()
    {
        animator.SetFloat("Speed", myWM.GetWeaponSpeed());
        myPV.RPC("CallAttack", PhotonTargets.AllViaServer);
    }

    // 攻撃
    [PunRPC]
    private void CallAttack()
    {
        StartCoroutine(Attack(PlayerAnimatorParameters.attack.ToString()));
    }

    IEnumerator Attack(string animParam)
    {
        bool finish = false;
        AnimatorStateInfo currentAnimState = animator.GetCurrentAnimatorStateInfo(0);
        while (!finish)
        {
            if(currentAnimState.IsName(animParam))
            {
                weaponCollider.enabled = true;
                myWM.weaponCollision = true;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                weaponCollider.enabled = false;
                myWM.weaponCollision = false;
                finish = true;
            }
            weapon.transform.localPosition = weaponPos;
            weapon.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        }
        //yield return new WaitForSeconds(attackStayTime);
        //animator.SetTrigger(PlayerAnimatorParameters.attack.ToString());
        //weaponCollider.enabled = true;
        //myWM.weaponCollision = true;
        //playerUIController.attackButton.interactable = false;
        //weapon.transform.localPosition = weaponPos;
        //weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        //yield return new WaitForSeconds(attackTime);
        //weaponCollider.enabled = false;
        //myWM.weaponCollision = false;
        //playerUIController.attackButton.interactable = true;
        //weapon.transform.localPosition = weaponPos;
        //weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
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
        Destroy(gameObject);
        if (myPV.isMine)
        {
            hpText.text = "HP: " + currentHP.ToString();
            hpSlider.value = currentHP;
            MainSceneManager mainSceneManager = GameObject.Find("MainManager").GetComponent<MainSceneManager>();
            mainSceneManager.GoToResult(1);
        }
    }

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
        animator.SetTrigger(PlayerAnimatorParameters.parry.ToString());
        yield return new WaitForSeconds(parryStayTime);
        parryCollider.enabled = true;
        parryCollision = true;
        playerUIController.parryButton.interactable = false;
        weapon.transform.localPosition = weaponPos;
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        yield return new WaitForSeconds(parryTime);
        parryCollider.enabled = false;
        parryCollision = false;
        playerUIController.parryButton.interactable = true;
        weapon.transform.localPosition = weaponPos;
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    public void CallWasparryed()
    {
        myPV.RPC("Wasparryed", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    void Wasparryed()
    {
        animator.SetTrigger(PlayerAnimatorParameters.desprate.ToString());
        currentHP -= Mathf.CeilToInt(wasparryedDamage);
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
        playerUIController.jumpButton.interactable = false;
        playerUIController.parryButton.interactable = false;
        weapon.transform.localPosition = weaponPos;
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        yield return new WaitForSeconds(rebelliousTime);
        playerUIController.attackButton.interactable = true;
        playerUIController.jumpButton.interactable = true;
        playerUIController.parryButton.interactable = true;
        weapon.transform.localPosition = weaponPos;
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
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