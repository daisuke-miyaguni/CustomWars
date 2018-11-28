using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
	[SerializeField] string deskTag;    // 机のタグ
    [SerializeField] string dustTag;	// 机のタグ
    [SerializeField] string weaponTag;    // 武器のタグ

    [SerializeField] float power;	  // ゴミにかかるパワー
    [SerializeField] float weaponPower;	   // ゴミにかかる武器のパワー

    Rigidbody rb;	  // 重力

    void Start()
    {
        this.gameObject.name = "Trash";
    }
	
	// 物質が当たったときの処理
	void OnCollisionEnter(Collision other)
	{
		float trashSpeed = power;	  // ゴミが飛んでくスピード
		if(other.gameObject.tag != deskTag && other.gameObject.tag != dustTag)
		{
			rb = GetComponent<Rigidbody>();

			// 当たった方向と逆に力を加えて飛んでいく
            gameObject.transform.LookAt(other.gameObject.transform);
            rb.AddForce(transform.forward * (-trashSpeed), ForceMode.Impulse);
        }
	}

    // 物質が当たったときの処理
    void OnTriggerEnter(Collider other)
	{
        float trashSpeed = power;     // ゴミが飛んでくスピード
        if (other.gameObject.tag != deskTag && other.gameObject.tag != dustTag)
        {
            rb = GetComponent<Rigidbody>();

            // 武器のときスピードを更新する
            if (other.gameObject.tag == weaponTag)
            {
                trashSpeed = power * weaponPower;
            }

            // 当たった方向と逆に力を加えて飛んでいく
            gameObject.transform.LookAt(other.gameObject.transform);
            rb.AddForce(transform.forward * (-trashSpeed), ForceMode.Impulse);
        }
	}
}
