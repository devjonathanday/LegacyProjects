using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialModifier : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Texture2D[] albedoMaps;
    public Color albedoColor;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = albedoMaps[0];
        meshRenderer.material.color = albedoColor;
    }
    
    void Update()
    {
        meshRenderer.material.color = albedoColor;

        if (Input.GetKey(KeyCode.P))
            meshRenderer.material.mainTexture = albedoMaps[Random.Range(0, albedoMaps.Length)];
    }
}
