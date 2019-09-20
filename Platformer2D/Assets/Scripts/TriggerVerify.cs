using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVerify : MonoBehaviour
{
    public bool isColliding;
    public LayerMask activators;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(activators == (activators | (1 << collision.gameObject.layer))) isColliding = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (activators == (activators | (1 << collision.gameObject.layer))) isColliding = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}