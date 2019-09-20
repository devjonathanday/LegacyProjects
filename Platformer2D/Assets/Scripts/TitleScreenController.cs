using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public Transform[] BGImages;
    public float[] scrollSpeed;
    public bool scrollRight;

    void Update()
    {
        for(int i = 0; i < BGImages.Length; i++)
        {
            if (scrollRight) BGImages[i].position += Vector3.right * scrollSpeed[i];
            else BGImages[i].position -= Vector3.right * scrollSpeed[i];
        }
    }
}