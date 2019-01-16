using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerCamera : MonoBehaviour
{
    // private PlayerController playerController;
    private MobileInputController controller;
    private Camera myCamera;
    private PhotonView myPV;
    private Vector3 myPos;
    // [SerializeField] private Vector2 startPos;
    [SerializeField] private float rotateSpeed;
    private bool isDoubleTapStart;
    private float doubleTapTime;
    private int tapFingerID;
    [SerializeField] private float tappedEnabledTime;

    // private float beforeCameraAngle;
    // private Vector3 beforeDistance;
    // private bool isRotate;
    // [SerializeField] private Touch currentTouch;
    // [SerializeField] private bool isFirstTouch;
    // [SerializeField] private bool isSecondTouch;
    // [SerializeField] private bool isSingleTouch;
    // // [SerializeField] private int beforTouchCount;

    // private Vector2 currentstartPos;
    // [SerializeField] private TouchPhase touchPhase;


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
            myCamera.transform.position = transform.position + new Vector3(0, 3.0f, -5);
            myCamera.transform.localEulerAngles = new Vector3(15, 0, 0);
            myPos = gameObject.transform.position;

            // playerController = this.gameObject.GetComponent<PlayerController>();
            controller = GameObject.Find("RightJoyStick").gameObject.GetComponent<MobileInputController>();

            // beforeCameraAngle = myCamera.transform.rotation.y;
            // beforeDistance = playerController.GetMoveDirection();
        }
    }
    void Update()
    {
        RotateFront();
    }

    void FixedUpdate()
    {
        if (myPV.isMine)
        {
            RotateCamera();
        }
    }

    // void LateUpdate()
    // {
    //     myCamera.transform.position += gameObject.transform.position - myPos;
    //     myPos = gameObject.transform.position;
    // }

    private void RotateFront()
    {
        if (isDoubleTapStart)
        {
            doubleTapTime += Time.deltaTime;
            if (doubleTapTime < tappedEnabledTime)
            {
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId) && Input.GetTouch(i).phase == TouchPhase.Began)
                        {
                            float angleDifference = transform.localEulerAngles.y - myCamera.transform.localEulerAngles.y;
                            myCamera.transform.RotateAround(myPos, Vector3.up, angleDifference);

                            isDoubleTapStart = false;
                            doubleTapTime = 0.0f;
                        }
                    }
                }
            }
            else
            {
                isDoubleTapStart = false;
                doubleTapTime = 0.0f;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId) && Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        tapFingerID = Input.GetTouch(i).fingerId;
                        isDoubleTapStart = true;
                        break;
                    }
                }
            }
        }
    }

    private void RotateCamera()
    {
        myCamera.transform.position += gameObject.transform.position - myPos;
        myPos = gameObject.transform.position;
        if (controller.Coordinate().x == 0)
        {
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

    }
}
