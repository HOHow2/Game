using IL3DN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{


    public bool playerInRange;
    public bool canBechopped;
    public float treeHealth;
    private float treeMaxHealth = 200;
    public Animator animator;
   

    // Start is called before the first frame update
    void Start()
    {
        treeHealth = treeMaxHealth;
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
        SoundManager.instance.Cutting();
        animator.SetTrigger("Shake");
        treeHealth -= 18;
        PlayerStatus.instance.currentStamina -= 5; // consuming stamina
        if (treeHealth < 0)
        {
            SoundManager.instance.Dropping();
            TreeIsDead();
        }

    }

    public IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.6f);
        
        
    }
    void TreeIsDead()
    {
        Vector3 treePostion = transform.position;
        Destroy(transform.parent.gameObject);
        canBechopped = false;
        SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"), new Vector3(treePostion.x, treePostion.y-2, treePostion.z), Quaternion.Euler(0,0,0));

    }


    // Update is called once per frame
    void Update()
    {
        if (canBechopped)
        {
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;

        }

        if (treeHealth<0)
        {
            treeHealth = 0;
        }
      
    }
}
