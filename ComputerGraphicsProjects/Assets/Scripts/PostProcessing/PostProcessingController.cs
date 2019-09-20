using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignettePulse : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    public float intensity;
    public float decay;
    public bool pulse;
    public float pulseIntensity;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void Update()
    {
        if(pulse)
        {
            Pulse(pulseIntensity);
            pulse = false;
        }
        m_Vignette.intensity.value = intensity;

        if (intensity > 0) intensity -= decay;
        if (intensity <= 0) intensity = 0;
    }

    void Pulse(float newIntensity)
    {
        intensity = newIntensity;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
    
}