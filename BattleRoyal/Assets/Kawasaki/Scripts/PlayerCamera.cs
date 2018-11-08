using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera myCamera;
    [SerializeField] private float rotateSpeed;

    void Start()
    {
        // カメラ取得、位置調整
        myCamera = Camera.main;
        myCamera.transform.parent = transform;
        myCamera.transform.position = transform.position + new Vector3(0, 0.8f, -5);
    }

    void Update()
    {
        RotateCamera();

    }

    private void RotateCamera()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotateSpeed,
            Input.GetAxis("Mouse Y") * rotateSpeed,
            0
        );

        myCamera.transform.RotateAround(transform.position, Vector3.up, angle.x);
        myCamera.transform.RotateAround(transform.position, myCamera.transform.right, angle.y * -1);
    }
}
