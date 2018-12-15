﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerCamera : MonoBehaviour
{
    private PlayerController playerController;
    private Camera myCamera;
    private PhotonView myPV;
    private Vector3 myPos;
    private Vector2 startPos;
    [SerializeField] private float rotateSpeed;


    void Awake()
    {
        myPV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (myPV.isMine)
        {
            myCamera = Camera.main;
            myCamera.transform.position = transform.position + new Vector3(0, 1.5f, -5);
            myPos = gameObject.transform.position;

            playerController = this.gameObject.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (myPV.isMine)
        {
            CheckTouch();
        }
    }

    private void CheckTouch()
    {
        myCamera.transform.position += gameObject.transform.position - myPos;
        myPos = gameObject.transform.position;

        if (Input.touchCount == 1 && playerController.GetMoveDirection() == Vector3.zero)
        {
            print("a");
            RotateCamera(Input.GetTouch(0));
        }
        if (Input.touchCount == 2 && playerController.GetMoveDirection() != Vector3.zero)
        {
            print("b");
            for (int i = 0; i < Input.touchCount; i++)
            {
                RotateCamera2(Input.GetTouch(i));
            }
            // for (int i = 0; i < Input.touchCount; i++)
            // {
            //     if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
            //     {
            //         print(i);
            //         RotateCamera(Input.GetTouch(i));
            //     }
            // }
        }
    }

    //     for (int i = 0; i < Input.touchCount; i++)
    //     {
    //         Touch touch = Input.GetTouch(i);
    //         if (playerController.GetMoveDirection() != Vector3.zero &&
    //             !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
    //         {
    //             return;
    //         }
    //     }









    //     if (Input.touchCount > 1 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
    //     {
    //         Touch touch = Input.GetTouch(1);
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             startPos = touch.position;
    //         }
    //         else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    //         {
    //             float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
    //             float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
    //             Vector2 angle = new Vector2(x, y).normalized;
    //             myCamera.transform.RotateAround(myPos, Vector3.up, angle.x * rotateSpeed);
    //         }
    //     }
    //     else if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             startPos = touch.position;
    //         }
    //         else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    //         {
    //             float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
    //             float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
    //             Vector2 angle = new Vector2(x, y).normalized;
    //             myCamera.transform.RotateAround(myPos, Vector3.up, angle.x * rotateSpeed);
    //         }
    //     }
    // }






    

    private void RotateCamera2(Touch t)
    {
        Touch touch = t;
        if (touch.phase == TouchPhase.Began && playerController.GetMoveDirection() != Vector3.zero)
        {
            return;
        }
        else
        {
            startPos = touch.position;
        }

        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
            float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
            Vector2 angle = new Vector2(x, y).normalized;
            myCamera.transform.RotateAround(myPos, Vector3.up, angle.x * rotateSpeed);

        }
        else
        {

        }

    }
    private void RotateCamera(Touch t)
    {
        Touch touch = t;
        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
            float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
            Vector2 angle = new Vector2(x, y).normalized;
            myCamera.transform.RotateAround(myPos, Vector3.up, angle.x * rotateSpeed);

        }
        else
        {

        }

    }
}




//         if (playerController.GetMoveDirection().x + playerController.GetMoveDirection().z != 0 &&
//             !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
//         {
//             return;
//         }
//         else if (Input.touchCount > 1 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
//         {
//             Touch touch = Input.GetTouch(1);
//             if (touch.phase == TouchPhase.Began)
//             {
//                 startPos = touch.position;
//             }
//             else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
//             {
//                 float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
//                 float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
//                 Vector2 angle = new Vector2(x, y).normalized;
//                 myCamera.transform.RotateAround(myPos, Vector3.up, angle.x * rotateSpeed);
//             }
//         }
//         else if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
//         {
//             Touch touch = Input.GetTouch(0);
//             if (touch.phase == TouchPhase.Began)
//             {
//                 startPos = touch.position;
//             }
//             else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
//             {
//                 float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
//                 float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
//                 Vector2 angle = new Vector2(x, y).normalized;
//                 myCamera.transform.RotateAround(myPos, Vector3.up, angle.x * rotateSpeed);
//             }
//         }
//     }
// }




// // マウスの右クリックを押している間
// if (Input.GetMouseButton(1))
// {
//     // マウスの移動量
//     float mouseInputX = Input.GetAxis("Mouse X");
//     float mouseInputY = Input.GetAxis("Mouse Y");
//     // targetの位置のY軸を中心に、回転（公転）する
//     myCamera.transform.RotateAround(myPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
//     // myCamera.transform.RotateAround(myPos, myCamera.transform.right, mouseInputY * Time.deltaTime * 200f);

// }


// playerCamera.transform.position = transform.position;


// if (Input.touchCount == 1)
// {
//     //回転
//     Touch t1 = Input.GetTouch(0);
//     if (t1.phase == TouchPhase.Began)
//     {
//         startPos = t1.position;
//     }
//     else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary)
//     {
//         float tx = t1.position.x - startPos.x; //横移動量(-1<tx<1)
//         float ty = t1.position.y - startPos.y; //縦移動量(-1<ty<1)
//         print(new Vector2(tx, ty).normalized);
//         Vector2 angle = new Vector2(tx, ty).normalized;


//         playerCamera.transform.eulerAngles += new Vector3(angle.y, angle.x, 0);


//         float angle_x = 180f <= playerCamera.transform.eulerAngles.x ? playerCamera.transform.eulerAngles.x - 360 : playerCamera.transform.eulerAngles.x;
//         playerCamera.transform.eulerAngles = new Vector3(
//             Mathf.Clamp(angle_x, angleMin, angleMax),
//             playerCamera.transform.eulerAngles.y,
//             playerCamera.transform.eulerAngles.z
//         );


// if (Input.GetMouseButton(1))
// {
//     Vector3 angle = new Vector3(
//         Input.GetAxis("Mouse X") * rotateSpeed,
//         Input.GetAxis("Mouse Y") * rotateSpeed,
//         0
//     );
//     print(angle);

//     playerCamera.transform.eulerAngles += new Vector3(angle.y, angle.x, angle.z);


//     float angle_x = 180f <= playerCamera.transform.eulerAngles.x ? playerCamera.transform.eulerAngles.x - 360 : playerCamera.transform.eulerAngles.x;
//     playerCamera.transform.eulerAngles = new Vector3(
//         Mathf.Clamp(angle_x, angleMin, angleMax),
//         playerCamera.transform.eulerAngles.y,
//         playerCamera.transform.eulerAngles.z
//     );




// Vector3 angle = new Vector3(
//     Input.GetAxis("Mouse X") * rotateSpeed,
//     Input.GetAxis("Mouse Y") * rotateSpeed * -1,
//     0
// );

// myCamera.transform.RotateAround(transform.position, Vector3.up, angle.x);

// float rotationX = myCamera.transform.rotation.x;
// if (rotationX < angleMax && rotationX > angleMin)
// {
//     myCamera.transform.RotateAround(transform.position, myCamera.transform.right, angle.y);
// }


