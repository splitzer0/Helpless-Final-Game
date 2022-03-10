using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryToUI : MonoBehaviour
{
    public GameObject InventoryItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (GameManager.instance.InventoryUI.transform.GetChild(0).childCount - 1 < i)
            {
                GameObject NewInventoryItem = Instantiate(InventoryItem, GameManager.instance.InventoryUI.transform.GetChild(0));
                NewInventoryItem.GetComponent<Image>().sprite = transform.GetChild(i).GetComponent<InteractableItem>().InventoryImage;
                NewInventoryItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = transform.GetChild(i).name;
                NewInventoryItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = transform.GetChild(i).GetComponent<InteractableItem>().Description;
            }
        }
    }
}
