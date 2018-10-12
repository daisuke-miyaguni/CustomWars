using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

	public float maxSpeed;
    public float cornerning;
    public float basePower;
    public Rigidbody myRigid;
    public PhotonView myPV;
    // public Camera myCamera;
    private float movingXaxis;
    private float movingYaxis;

    // Use this for initialization
    void Start () {
		if(!myPV.isMine){
            myRigid.isKinematic = true;
            // myCamera.transform.gameObject.SetActive(false);
            Destroy(this);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        
		if(!Application.isMobilePlatform){
            if(Input.GetKey(KeyCode.Q)){
                myPV.RPC("GameEnd", PhotonTargets.Others);
            }

			if(Input.GetKey(KeyCode.W)){
                movingYaxis = 1;
            }else if(Input.GetKey(KeyCode.S)){
                movingYaxis = -1;
            }else{
                movingYaxis = 0;
            }

			if(Input.GetKey(KeyCode.D)){
                movingXaxis = 1;
            }
			else if(Input.GetKey(KeyCode.A)){
				movingXaxis = -1;
            }else{
                movingXaxis = 0;
            }
		}
	}

	void FixedUpdate(){
		if(movingYaxis != 0){
			if(myRigid.velocity.magnitude < maxSpeed){
                myRigid.AddForce(transform.TransformDirection(Vector3.forward) *
                basePower * 11f * movingYaxis);
            }
			if(myRigid.angularVelocity.magnitude < (myRigid.velocity.magnitude * 0.3f)){
                myRigid.AddTorque(transform.TransformDirection(Vector3.up) * cornerning *
                movingXaxis * -90f);
            }else{
                myRigid.angularVelocity = (myRigid.velocity.magnitude * 0.3f) * myRigid.angularVelocity.normalized;
            }
        }
	}

    [RPC]
    void GameEnd(){
        Application.Quit();

    }
}
