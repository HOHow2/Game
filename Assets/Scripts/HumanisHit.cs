using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class HumanisHit : MonoBehaviour
{

    public bool playerInRange;
    public bool canBeHit;
    public float BotHealth;
    private float BotMaxHealth = 1000;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        BotHealth = BotMaxHealth;
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider Item)
    {
        if (Item.CompareTag("Player"))
        {
            playerInRange = true;
        }

    }

    private void OnTriggerExit(Collider Item)
    {
        if (Item.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    public void GetHit()
    {
        SoundManager.instance.Hitting();
        Gizmos.color = Color.red;
        BotHealth -= 59;
        PlayerStatus.instance.currentStamina -= 5; // consuming stamina
        if (BotHealth < 0)
        {
            SoundManager.instance.botdied();
            BotIsDead();
        }

    }

    public IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.7f);


    }
    void BotIsDead()
    {
        Destroy(gameObject);
        canBeHit = false;
        SelectionManager.Instance.selectedBot = null;
        SelectionManager.Instance.BotState.gameObject.SetActive(false);


    }


    // Update is called once per frame
    void Update()
    {
        if (canBeHit)
        {
            GlobalBotState.Instance.resourceHealth = BotHealth;
            GlobalBotState.Instance.resourceMaxHealth = BotMaxHealth;

        }

    }
}
