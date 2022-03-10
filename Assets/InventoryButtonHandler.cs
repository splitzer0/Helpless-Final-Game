using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateActiveItem()
    {
        GameManager.instance.InventoryUI.transform.GetChild(1).GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        GameManager.instance.InventoryUI.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        GameManager.instance.InventoryUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        GameManager.instance.InventoryUI.transform.GetChild(2).gameObject.SetActive(true);
    }
}
