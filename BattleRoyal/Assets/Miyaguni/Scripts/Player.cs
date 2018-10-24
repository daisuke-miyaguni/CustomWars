using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player:Photon.MonoBehaviour
{
    [SerializeField]
    float jumpForce;

    string advanceController = "Horizontal";
    string sideController = "Vertical";

    [SerializeField]
    string floorTagName;

    PhotonView p_photonView;

    void Start()
    {
        p_photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        // 持ち主でないのなら制御させない
        if (p_photonView.isMine)
        {  
            Move();
        }
    }

    void Move()
    {
        float advance = Input.GetAxis(advanceController);
        float side = Input.GetAxis(sideController);
        transform.Translate(advance / 10.0f, 0, side / 10.0f);
    }

    void OnTriggerStay(Collider other)
    {
        if (!p_photonView.isMine)
            return;
        //Playerのジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && other.gameObject.tag == floorTagName)
        {
            Jump();
        }
    }

    void Jump()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.up * jumpForce);
    }
}
