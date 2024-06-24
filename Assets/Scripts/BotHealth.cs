using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class BotHealth : MonoBehaviour
{
    private Slider slider;
    private float currentHealth, maxHealth;
    public GameObject globalBotState;

    private void Awake()
    {
        slider = GetComponent<Slider>();

    }

    private void Update()
    {
        currentHealth = globalBotState.GetComponent<GlobalBotState>().resourceHealth;
        maxHealth = globalBotState.GetComponent<GlobalBotState>().resourceMaxHealth;
        float fillvalue = currentHealth / maxHealth;
        slider.value = fillvalue;
    }

}
