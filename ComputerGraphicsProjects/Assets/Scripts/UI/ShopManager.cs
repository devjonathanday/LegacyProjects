using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject defaultButton;
    public RectTransform contentBox;
    public List<GameObject> listItems = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) AddNewItem();
        if (Input.GetKeyDown(KeyCode.R)) RefreshList();
    }

    void AddNewItem()
    {
        GameObject newButton = Instantiate(defaultButton, contentBox);
        listItems.Add(newButton);
        RefreshList();
    }

    void RefreshList()
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            RectTransform rect = listItems[i].GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(-280, -140 - (i * 120));
            rect.offsetMax = new Vector2(280, -40 - (i * 120));
            listItems[i].GetComponentInChildren<Text>().text = "Item " + (i+1) + "\n$" + ((i+1) * 10);
            contentBox.sizeDelta = new Vector2(0, 50 + (listItems.Count * 120));
        }
    }
}