using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{

    static public int movingYaxis;
    static public int movingXaxis;
    static public bool fireWeapon;


    // Use this for initialization
    void Start()
    {
        initializeVariable();

    }

    static public void initializeVariable()
    {
        movingYaxis = 0;
        movingXaxis = 0;
        fireWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
