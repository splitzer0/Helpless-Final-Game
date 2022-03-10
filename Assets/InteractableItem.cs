using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableItem : MonoBehaviour
{
    public enum InteractionType { none, EndDoor, Item, Key_Item};
    public InteractionType ObjectInteractionType;

    public GameManager.EventToTrigger TriggerEvent;

    public Sprite InventoryImage;
    [TextArea()]
    public string Description;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndingDoor()
    {
        SceneManager.LoadScene(1);
    }

    public void ItemInteraction()
    {
        transform.parent = GameManager.instance.PlayerInventory.transform;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        Debug.Log("Item Added To Inventory");
    }

    public void KeyItemInteraction()
    {
        Debug.Log("Event Triggered: " + TriggerEvent);
        GameManager.instance.TriggerItemEvent(TriggerEvent);
        ItemInteraction();
    }

    public void Interact()
    {
        if (ObjectInteractionType == InteractionType.none)
        {

        }
        else if (ObjectInteractionType == InteractionType.EndDoor)
        {
            EndingDoor();
        }
        else if (ObjectInteractionType == InteractionType.Item)
        {
            ItemInteraction();
        }
        else if (ObjectInteractionType == InteractionType.Key_Item)
        {
            KeyItemInteraction();
        }
    }
}
