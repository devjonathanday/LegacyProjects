using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDimmer : MonoBehaviour
{
    private Light light;

    public bool illuminated;
    public float maxIntensity;
    public float increment;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    void Update()
    {
        if (illuminated && light.intensity < maxIntensity)
            light.intensity += increment;
        else if (!illuminated && light.intensity > 0)
            light.intensity -= increment;
    }
}