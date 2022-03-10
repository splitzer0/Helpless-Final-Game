using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    //Refrences
    public static GameManager instance;
    public Transform Player;
    public GameObject FlashLight;
    public GameObject PlayerInventory;
    public GameObject InventoryUI;

    public Transform Enemy;

    //events
    public enum EventToTrigger { none, FlashLightCollected};


    //event bools
    public bool flashlightPickedUp;


    //UIControllers
    public bool InventoryOpen;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerItemEvent(EventToTrigger trigger)
    {
        if(trigger == EventToTrigger.FlashLightCollected)
        {
            flashlightPickedUp = true;
        }
    }

    public void ToggleInventory()
    {
        if (InventoryOpen)
        {
            InventoryOpen = false;
            InventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            InventoryUI.transform.GetChild(1).GetComponent<Image>().sprite = null;
            InventoryUI.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0f);
            InventoryUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            InventoryUI.transform.GetChild(2).gameObject.SetActive(false);

        } 
        else if (!InventoryOpen)
        {
            InventoryOpen = true;
            InventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
