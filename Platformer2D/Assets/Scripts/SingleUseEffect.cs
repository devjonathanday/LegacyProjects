using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUseEffect : MonoBehaviour
{
    public float duration;
    public float timer;

    void Update()
    {
        if(timer >= duration) Destroy(gameObject);
        timer += Time.deltaTime;
    }
}