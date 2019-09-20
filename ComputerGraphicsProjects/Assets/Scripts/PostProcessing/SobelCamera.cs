using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SobelCamera : MonoBehaviour
{
    float tempShadowDistance;
    
    void OnPreRender()
    {
        tempShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }
    void OnPostRender()
    {
        QualitySettings.shadowDistance = tempShadowDistance;
    }
}