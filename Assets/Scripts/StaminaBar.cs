using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    private Slider slider;


    public GameObject playerStatus;

    private float currentStamina, maxStamina;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
   
    // Update is called once per frame
    void Update()
    {
        currentStamina = playerStatus.GetComponent<PlayerStatus>().currentStamina;
        maxStamina = playerStatus.GetComponent<PlayerStatus>().maxStamina;
        float fillvalue = currentStamina/ maxStamina;
        slider.value = fillvalue;


    }
}
