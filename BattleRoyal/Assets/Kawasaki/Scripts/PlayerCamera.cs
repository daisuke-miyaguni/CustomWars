using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float rotateSpeed;

    void Start()
    {
        player = transform.parent.gameObject;
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // if (Input.GetMouseButton(0))
        // {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotateSpeed,
            Input.GetAxis("Mouse Y") * rotateSpeed,
            0
        );

        transform.RotateAround(player.transform.position, Vector3.up, angle.x);
        transform.RotateAround(player.transform.position, transform.right, angle.y * -1);
        // }
    }
}
