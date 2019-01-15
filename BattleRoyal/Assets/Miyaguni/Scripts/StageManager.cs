using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] SphereCollider[] sphereCollider;    // 安地範囲

    [SerializeField] GameObject safeArea;

    [SerializeField] float safeAreaPosX;
    [SerializeField] float safeAreaPosZ;

    float initPosX;
    float initPosZ;

    [SerializeField] float reducingSpeed;    // 縮小スピード
    [SerializeField] float collisionOnTime;    // 判定スタートまでの時間

    [SerializeField] float[] reductionScales;    // 安地の大きさ

    [SerializeField] string deskTag;    // 机のタグ名

    [SerializeField] int reductionCount;    // 範囲番号

    // 範囲番号ゲッター
    public int GetReductionCount()
    {
        return reductionCount;
    }

    PhotonView photonView;

    void Start()
    {
        // 範囲番号の初期化
        reductionCount = 0;

        sphereCollider[0] = GetComponent<SphereCollider>();
        sphereCollider[0].enabled = false;

        sphereCollider[1] = safeArea.GetComponent<SphereCollider>();

        photonView = GetComponent<PhotonView>();

        if (PhotonNetwork.isMasterClient)
        {
            initPosX = Random.Range(-safeAreaPosX, safeAreaPosX);
            initPosZ = Random.Range(-safeAreaPosZ, safeAreaPosZ);

            photonView.RPC("InitPosition", PhotonTargets.AllViaServer, initPosX, initPosZ);
        }
        Invoke("TriggerOn", collisionOnTime);
    }

    [PunRPC]
    void InitPosition(float x, float z)
    {
        // 安地の位置をランダムで決める
        gameObject.transform.position = new Vector3(x, 0.0f, z);
        safeArea.transform.position = gameObject.transform.position;
    }

    // 判定の初期化(指定秒後に安地判定)
    void TriggerOn()
    {
        sphereCollider[0].enabled = true;
    }

    void Update()
    {
        ScaleChecker();
    }

    // 安地範囲のチェック
    void ScaleChecker()
    {
        if (reductionCount > reductionScales.Length)
        {
            return;
        }
        // 指定より大きいと縮小が始まる
        if (sphereCollider[0].radius > reductionScales[reductionCount])
        {
            Reduction();
        }
        else
        {
            if (reductionCount + 1 == reductionScales.Length)
            {
                return;
            }
            sphereCollider[1].radius = reductionScales[reductionCount + 1];
        }
    }

    // 縮小処理
    void Reduction()
    {
        sphereCollider[0].radius -= reducingSpeed * Time.deltaTime;
    }

    // 安地縮小を呼び出す
    public void ReceveReductionEvent()
    {
        if (reductionCount < reductionScales.Length - 1)
        {
            reductionCount++;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag != "Player" || other.gameObject.layer != 15)
        {
            Destroy(other.gameObject);
        }
    }
}
