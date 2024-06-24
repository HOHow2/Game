using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalBotState : MonoBehaviour
{
    public static GlobalBotState Instance { get; set; }
    public float resourceHealth, resourceMaxHealth;
    private GameObject BotHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }


    }

    void Update()
    {


    }

}
