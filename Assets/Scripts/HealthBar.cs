using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private  Slider slider;


    public GameObject playerStatus;

    private float currentHealth, maxHealth;


    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerStatus.GetComponent<PlayerStatus>().currentHealth;
        maxHealth = playerStatus.GetComponent<PlayerStatus>().maxHealth;
        float fillvalue = currentHealth / maxHealth;
        slider.value = fillvalue;



    }
}
