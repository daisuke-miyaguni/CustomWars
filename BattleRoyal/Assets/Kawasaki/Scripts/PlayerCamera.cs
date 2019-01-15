using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerCamera : MonoBehaviour
{
    private PlayerController playerController;
    private MobileInputController controller;
    private Camera myCamera;
    private PhotonView myPV;
    private Vector3 myPos;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private float rotateSpeed;

    // private float beforeCameraAngle;
    // private Vector3 beforeDistance;
    // private bool isRotate;
    [SerializeField] private Touch currentTouch;
    [SerializeField] private bool isFirstTouch;
    [SerializeField] private bool isSecondTouch;
    [SerializeField] private bool isSingleTouch;
    // [SerializeField] private int beforTouchCount;

    private Vector2 currentstartPos;
    [SerializeField] private TouchPhase touchPhase;


    /*指一本のときの一番最初の処理がelseに入っていいる */


    void Awake()
    {
        myPV = GetComponent<PhotonView>();
        // print(startPos);
    }

    void Start()
    {
        if (myPV.isMine)
        {
            myCamera = Camera.main;
            myCamera.transform.position = transform.position + new Vector3(0, 1.5f, -5);
            myPos = gameObject.transform.position;

            playerController = this.gameObject.GetComponent<PlayerController>();
            controller = GameObject.Find("RightJoyStick").gameObject.GetComponent<MobileInputController>();

            // beforeCameraAngle = myCamera.transform.rotation.y;
            // beforeDistance = playerController.GetMoveDirection();
        }
    }

    void FixedUpdate()
    {
        // if (Input.touchCount > 0)
        // {
        //     foreach (Touch t in Input.touches)
        //     {
        //         Debug.Log(t.fingerId);
        //         // if (t.phase == TouchPhase.Began)
        //         // {
        //         //     Debug.Log("fid=" + t.fingerId + " x=" + t.position.x + " y=" + t.position.y);
        //         // }
        //         // else if (t.phase == TouchPhase.Ended){
        //         //     // Debug.Log("fid=" + t.fingerId + " x=" + t.position.x + " y=" + t.position.y);

        //         // }                          
        //     }
        // }
        if (myPV.isMine)
        {
            if(controller.Coordinate().x ==0){
                return;
            }
            else if (controller.Coordinate().x > 0)
            {
                myCamera.transform.RotateAround(myPos, Vector3.up, rotateSpeed * Time.deltaTime);
            }
            else
            {
                myCamera.transform.RotateAround(myPos, Vector3.up, -rotateSpeed * Time.deltaTime);
            }
            // CheckTouch();
        }
    }
    void LateUpdate()
    {
        myCamera.transform.position += gameObject.transform.position - myPos;
        myPos = gameObject.transform.position;
    }

    // private void CheckTouch()
    // {
    //     if (Input.touchCount == 1)
    //     {
    //         // print(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId));
    //         // print(playerController.GetMoveDirection() != Vector3.zero);

    //         if (playerController.GetMoveDirection() == Vector3.zero)
    //         {
    //             print("1");
    //             isSingleTouch = true;
    //             // isFirstTouch = false;
    //             isSecondTouch = false;
    //             for (int i = 0; i < Input.touchCount; i++)
    //             {
    //                 if (Input.touches[i].fingerId == 0)
    //                 {
    //                     currentTouch = Input.touches[i];
    //                 }
    //             }
    //             currentTouch = Input.GetTouch(0);
    //             touchPhase = currentTouch.phase;
    //             // print("else");
    //             // print("direction" + playerController.GetMoveDirection());
    //             RotateCamera(currentTouch);

    //         }

    //     }
    //     // print(currentTouch.fingerId);
    //     else if (Input.touchCount > 1)
    //     {

    //         if (isSingleTouch)  /* (!isSingleTouch)*/
    //         {
    //             print("3");

    //             // isSingleTouch = false;
    //             isFirstTouch = true;
    //             // currentTouch = Input.GetTouch(0);
    //             for (int i = 0; i < Input.touchCount; i++)
    //             {
    //                 if (Input.touches[i].fingerId == currentTouch.fingerId)
    //                 {
    //                     currentTouch = Input.touches[i];
    //                 }
    //             }
    //             RotateCamera(currentTouch);

    //         }
    //         else /*if (isSingleTouch)*/
    //         {
    //             print("2");

    //             isSecondTouch = true;
    //             isSingleTouch = false;
    //             // currentTouch = Input.GetTouch(1);
    //             for (int i = 0; i < Input.touchCount; i++)
    //             {
    //                 if (Input.touches[i].fingerId == 1)
    //                 {
    //                     currentTouch = Input.touches[i];
    //                 }
    //             }

    //             RotateCamera(currentTouch);
    //         }


    //         // print("2本");
    //         // print("Distance" + beforeDistance == playerController.GetMoveDirection().ToString());
    //         // print("rotation" + beforeCameraAngle == myCamera.transform.rotation.y.ToString());



    //         // if (isFirstTouch || beforeDistance == Vector3.zero)
    //         // {
    //         //     if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
    //         //     {
    //         //         // print("1");
    //         //         isFirstTouch = true;
    //         //         isSecondTouch = false;

    //         //         currentTouch = Input.GetTouch(0);
    //         //         RotateCamera(currentTouch);
    //         //     }
    //         // }
    //     }
    //     // beforeDistance = playerController.GetMoveDirection();
    //     // beforTouchCount = Input.touchCount;
    //     // touchPhase = Input.GetTouch(0).phase;
    // }


    // private void RotateCamera(Touch t)
    // {
    //     Touch touch = t;
    //     // print(t.fingerId);
    //     if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
    //     {
    //         print("UI");
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             startPos = touch.position;

    //             // startPos = touch.position;
    //             print("touchstart");
    //             // print("startpos" + startPos);
    //         }
    //         else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    //         {
    //             float x = touch.position.x - startPos.x; //横移動量(-1<tx<1)
    //             float y = touch.position.y - startPos.y; //縦移動量(-1<ty<1)
    //                                                      // print("x=" + x);
    //                                                      // print("y=" + y);
    //                                                      // print("xstartpos=" + startPos.x);
    //                                                      // print("ystartpos=" + startPos.y);

    //             // print("xtouchpos=" + touch.position.x);
    //             // print("ytouchpos=" + touch.position.y);

    //             Vector2 angle = new Vector2(x, y).normalized;
    //             // beforeCameraAngle = myCamera.transform.rotation.y;
    //             if (angle.x > 0)
    //             {
    //                 myCamera.transform.RotateAround(myPos, Vector3.up, rotateSpeed * Time.deltaTime);
    //             }
    //             else
    //             {
    //                 myCamera.transform.RotateAround(myPos, Vector3.up, -rotateSpeed * Time.deltaTime);
    //             }
    //         }
    //         else if (touch.phase == TouchPhase.Ended)
    //         {
    //             // beforeCameraAngle = myCamera.transform.rotation.y;
    //             isSingleTouch = false;
    //             isFirstTouch = false;
    //             isSecondTouch = false;
    //             // print("End");

    //         }
    //     }
    // }



    //    else if (Input.touchCount > 1)
    //         {
    //             if (beforeCameraAngle == myCamera.transform.rotation.y && playerController.GetMoveDirection() == Vector3.zero)
    //             {
    //                 return;
    //             }
    //             else if (beforeCameraAngle != myCamera.transform.rotation.y && playerController.GetMoveDirection() != Vector3.zero)
    //             {
    //                 RotateCamera(currentTouch);
    //             }
    //             else if (beforeCameraAngle == myCamera.transform.rotation.y && playerController.GetMoveDirection() != Vector3.zero)
    //             {
    //                 currentTouch = Input.GetTouch(1);
    //                 RotateCamera(currentTouch);
    //             }
    //             else if (beforeCameraAngle != myCamera.transform.rotation.y && playerController.GetMoveDirection() == Vector3.zero)
    //             {
    //                 currentTouch = Input.GetTouch(0);
    //                 RotateCamera(currentTouch);
    //             }
    //         }




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

}
