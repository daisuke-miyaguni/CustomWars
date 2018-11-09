using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera myCamera;
    private PhotonView myPV;
    private Vector3 myPos;

    void Awake()
    {
        myPV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (myPV.isMine)
        {
            myCamera = Camera.main;
            myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);
            myPos = gameObject.transform.position;
        }
        // // カメラ取得、位置調整
        // myCamera = Camera.main;
        // myCamera.transform.parent = transform;
        // myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);
    }

    void Update()
    {
        if (myPV.isMine)
            RotateCamera();
    }

    private void RotateCamera()
    {
        myCamera.transform.position += gameObject.transform.position - myPos;
        myPos = gameObject.transform.position;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            myCamera.transform.RotateAround(myPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // myCamera.transform.RotateAround(myPos, myCamera.transform.right, mouseInputY * Time.deltaTime * 200f);

        }
    }
}
