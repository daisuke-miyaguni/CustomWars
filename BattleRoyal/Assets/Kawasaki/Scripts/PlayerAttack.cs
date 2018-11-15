using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    private PhotonView myPV;
    private Button attackButton;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject weapon;
    private float weaponPower;

    void Awake()
    {
        // photonview取得
        myPV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (myPV.isMine)
        {
            weapon.SetActive(false);
            // 攻撃ボタン取得、設定
            attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
            attackButton.onClick.AddListener(this.OnClickAttack);
        }
    }

    // 攻撃入力
    public void OnClickAttack()
    {
        myPV.RPC("Attack", PhotonTargets.All, transform.position, weaponPower);
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
}
