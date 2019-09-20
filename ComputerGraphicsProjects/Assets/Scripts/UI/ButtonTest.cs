using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public Button button;
    void Start()
    {
        button.onClick.AddListener(delegate { Ping(); });
    }

    void Ping()
    {
        Debug.Log("Ping");
    }
}