using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Collider2D interactableAreaCollider;
    public GameObject interactButton;
    public string playerTag = "Player";
    public int totalAllowedTimes = -1;
    public int timesInteracted = 0;
    public Vector3 interactButtonOffset = new Vector3(0, 1, 0);

    private GameObject dialogueObject;
    public bool isInArea;

    public GameObject player = null;

    void Update()
    {
        // Detect when the E arrow key is pressed down
        if (isInArea && Input.GetKeyDown(KeyCode.E) && (totalAllowedTimes == -1 || (totalAllowedTimes != -1 && timesInteracted < totalAllowedTimes)))
        {
            Interact(player);
            timesInteracted++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            isInArea = true;
            player = collision.gameObject;
            // Show dialog box
            if (interactButton)
            {
                dialogueObject = Instantiate(interactButton, this.transform.position + interactButtonOffset, this.transform.rotation);
                dialogueObject.transform.parent = this.transform;
            }

            // if (collision.gameObject.GetComponent<HealthManager>())
            // {
            //     if (!collision.gameObject.GetComponent<HealthManager>().dead && collision.gameObject.GetComponent<HealthManager>().healthPoints < collision.gameObject.GetComponent<HealthManager>().healthBar.maxValue)
            //     {
            //         if(effect != null)
            //         {
            //            Instantiate(effect, transform.position, transform.rotation);
            //         }
                    
            //         if (hitSfx != null)
            //         {

            //             AudioSource.PlayClipAtPoint(hitSfx, transform.position);

            //         }
            //         collision.gameObject.GetComponent<HealthManager>().Heal(amt);
            //         Destroy(gameObject);
            //     }
            // }
        }
        else { }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = null;
            isInArea = false;
            if (dialogueObject)
            {
                Destroy(dialogueObject);
            }
        }
    }

    public virtual void Interact(GameObject go)
    {
        // Overwrite
        Debug.Log("Interacting with " + transform.name + " as " + go.transform.name);
    }
}
