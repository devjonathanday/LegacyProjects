using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public bool pickedUp;
    public event Action OnDown;
    public GameObject rightController;
    [SerializeField] private SteamVR_TrackedObject m_InteractiveItem;
    
    // Use this for initialization
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp == true)
        {
            transform.position = rightController.transform.position;
        }
    }

}