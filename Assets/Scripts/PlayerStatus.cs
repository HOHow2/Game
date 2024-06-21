using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{


    public static PlayerStatus instance {  get; set; }



    // --- Player Health --- //
    public float currentHealth;
    public float maxHealth;
    public float delayTime;

    // --- Player Mana --- //
    public float currentStamina;
    public float maxStamina;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    
    //Delay Time
    

    IEnumerator Delay()
    {
         
            yield return new WaitForSeconds(2);
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth  =  maxHealth;
        currentStamina = maxStamina;
    }
    

    // Update is called once per frame
    void Update()
    {


        // consuming Stamina
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse0))
        {
            currentStamina -= 10;

        }

        // Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentStamina -= 0.1f;

        }
       

        // max stamina
        if (currentStamina > 500 || currentHealth>100)
        {
            currentStamina = maxStamina;
            currentHealth = maxHealth;
        }

        // minimal Health and stamina
        if (currentHealth < 0)
        {
            currentHealth = 0;

        }
        if (currentStamina < 0)
        {

            currentStamina = 0;
        }
        //
        if(currentStamina < 100)
        {
            currentStamina += 1.5f*Time.deltaTime;
        }

    }
}
