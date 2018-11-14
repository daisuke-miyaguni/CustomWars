using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustManager : MonoBehaviour
{
    int initChildCount;


    [SerializeField] int maxTrashCounter;

    public int GetMaxTrashCounter()
    {
        return maxTrashCounter + initChildCount;
    }

    // [SerializeField] string itemBoxName;
    [SerializeField] GameObject itemBox;

    public int GetTrashCounter()
	{
		return gameObject.transform.childCount;
	}


	void Start()
	{
		initChildCount = gameObject.transform.childCount;
	}

	void Update()
	{
		TrashChecker();
	}

	void TrashChecker()
	{
		if(gameObject.transform.childCount >= GetMaxTrashCounter())
		{

            Instantiate
            (
                original: itemBox,
                position: new Vector3
                (
                    transform.position.x,
                    2.0f,
                    transform.position.z - 5.0f
                ),
                rotation: Quaternion.Euler(transform.forward)
            );

            // 	PhotonNetwork.Instantiate
            // 	(
            // 		itemBoxName,
            // 		gameObject.transform.position,
            // 		Quaternion.Euler(Vector3.zero),
            // 		0
            // 	);

            Destroy(this);
        }
	}
}
