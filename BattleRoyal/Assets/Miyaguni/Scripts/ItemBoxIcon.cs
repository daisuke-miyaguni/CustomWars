using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxIcon : MonoBehaviour
{
    private enum TagNames
    {
        PlayerControllerUI,
        Player,
    }

    [SerializeField] Image openIcon;    // 開くを伝えるアイコン

    BoxCollider bc;     // 宝箱のオープン範囲
    CapsuleCollider cc;     // Icon表示範囲

    // 開くIconの表示処理
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == TagNames.Player.ToString())
        {
            // 表示する
            openIcon.enabled = true;
            // Playerを向かせる
            openIcon.transform.rotation = Quaternion.LookRotation(other.gameObject.transform.position);
        }
    }

    // Iconの非表示処理
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagNames.Player.ToString())
        {
            // 非表示にする
            openIcon.enabled = false;
        }
    }

}
