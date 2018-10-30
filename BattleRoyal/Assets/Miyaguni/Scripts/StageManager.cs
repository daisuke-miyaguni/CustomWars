using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
	SphereCollider sphereCollider;
	[SerializeField] float reducingSpeed;
    [SerializeField] float collisionOnTime;
    [SerializeField] float[] reductionScales;
	[SerializeField] string destroyObjectTag;
    // reduction
	int reductionCount;



	void Start()
	{
		reductionCount = 0;
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.enabled = false;
		gameObject.transform.position = new Vector3(Random.Range(-60f, 60f), 0.0f, Random.Range(-60f, 60f));
		
		Invoke("TriggerOn", collisionOnTime);
	}

	void TriggerOn()
	{
		sphereCollider.enabled = true;
	}
	
	void Update()
	{
		if(sphereCollider.radius > reductionScales[reductionCount])
		{
			Reduction();
		}

		if(Input.GetMouseButtonDown(0)){
			ReceveReductionEvent();
		}
	}
	void Reduction()
	{
		sphereCollider.radius -= reducingSpeed * Time.deltaTime;
	}

	public void ReceveReductionEvent()
	{
		reductionCount++;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == destroyObjectTag)
		{
			other.gameObject.SetActive(false);
		}
	}
}
