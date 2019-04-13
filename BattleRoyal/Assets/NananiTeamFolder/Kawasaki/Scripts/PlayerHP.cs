using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{

    private const int maxHP = 100;
    private int currentHP = maxHP;
    [SerializeField] private Slider hpSlider;

    void Start()
    {
        hpSlider.value = currentHP;
    }

    void Update()
    {

    }
    
	private void TakeDamage(int amount){
        currentHP -= amount;

		if(currentHP<=0){
			// death
		}
    }
}
