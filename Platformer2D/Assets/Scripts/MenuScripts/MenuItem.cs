using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public RawImage image;
    public Texture2D focusedTexture;
    public Texture2D unfocusedTexture;
    public bool focused;

    void Update()
    {
        if (focused) image.texture = focusedTexture;
        else image.texture = unfocusedTexture;
    }
}
