using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

	public void InputAttack(){
        // playerController.InputAttack();

    }
}
