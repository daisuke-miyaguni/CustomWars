using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterMove : MonoBehaviour
{

    public float maxSpeed;
    public float cornerning;
    public float basePower;
    public Rigidbody myRigid;
    public PhotonView myPV;
    private float movingXaxis;
    private float movingYaxis;
    private bool countStop;
    [SerializeField]
    private Text scoreText;

    // Use this for initialization
    void Start()
    {
        if (myPV.isMine)
        {
            scoreText = GameObject.Find("score").GetComponent<Text>();
        }
        if (!myPV.isMine)
        {
            myRigid.isKinematic = true;
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (myPV.isMine)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                print("Q");
                myPV.RPC("AddScore", PhotonTargets.Others);
            }
        }

        if (!Application.isMobilePlatform)
        {


            if (Input.GetKey(KeyCode.W))
            {
                movingYaxis = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movingYaxis = -1;
            }
            else
            {
                movingYaxis = 0;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movingXaxis = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movingXaxis = -1;
            }
            else
            {
                movingXaxis = 0;
            }
        }

        if (!countStop)
        {
            scoreText.text += Time.deltaTime.ToString("F0");
        }
    }

    void FixedUpdate()
    {
        if (movingYaxis != 0)
        {
            if (myRigid.velocity.magnitude < maxSpeed)
            {
                myRigid.AddForce(transform.TransformDirection(Vector3.forward) *
                basePower * 11f * movingYaxis);
            }
            if (myRigid.angularVelocity.magnitude < (myRigid.velocity.magnitude * 0.3f))
            {
                myRigid.AddTorque(transform.TransformDirection(Vector3.up) * cornerning *
                movingXaxis * -90f);
            }
            else
            {
                myRigid.angularVelocity = (myRigid.velocity.magnitude * 0.3f) * myRigid.angularVelocity.normalized;
            }
        }
    }

    [RPC]
    void AddScore()
    {
        countStop = true;
    }
}
