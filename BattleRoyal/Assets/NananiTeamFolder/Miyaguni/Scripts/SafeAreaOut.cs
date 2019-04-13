using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaOut : MonoBehaviour
{
    [SerializeField] string areaTagName;    // 安地範囲のタグ名
    string areaCheckTagName = "AreaCheck";    // 安地範囲のタグ名

    [SerializeField] float deskMoveSpeed;    // 机が寄っていくスピード

    [SerializeField] float destroyTime = 9.0f;    // 削除カウント時間

    GameObject area;    // 安地のゲームオブジェクト

    Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
        gameObject.name = "Desk";
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // 安地が定義されていたら安地から離れる
        if (area != null)
        {
            gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * deskMoveSpeed;
            destroyTime -= Time.deltaTime;
        }

        // 一定の時間離れたら削除
        if (destroyTime < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == areaCheckTagName)
        {
            render.material.color = new Color(1f, 1f, 0f, 1f);
        }

        if (other.gameObject.tag == areaTagName)
        {
            SafeAreaExit();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Dust")
        {
            Destroy(other.gameObject);
        }
    }

    // 安地の外に行った処理
    public void SafeAreaExit()
    {
        render.material.color = new Color(1f, 0f, 0f, 1f);
        // 安地を見つける
        area = GameObject.FindWithTag(areaTagName);
        // 安地を向く
        gameObject.transform.LookAt(area.transform);
    }
}