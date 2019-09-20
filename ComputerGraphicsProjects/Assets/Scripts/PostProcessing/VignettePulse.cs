using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette m_Vignette;
    DepthOfField m_DepthOfField;
    ColorGrading m_ColorGrading;

    public float health;
    public float maxHealth;

    public AnimationCurve vignetteIntensity, focusDistance, colorSaturation;
    public float effectScale;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.overrideState = true;

        m_DepthOfField = ScriptableObject.CreateInstance<DepthOfField>();
        m_DepthOfField.enabled.Override(true);
        m_DepthOfField.focusDistance.overrideState = true;

        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);
        m_ColorGrading.saturation.overrideState = true;

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette, m_DepthOfField, m_ColorGrading);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        effectScale = health / maxHealth;
        m_Vignette.intensity.value = vignetteIntensity.Evaluate(effectScale);
        m_DepthOfField.focusDistance.value = focusDistance.Evaluate(effectScale);
        m_ColorGrading.saturation.value = colorSaturation.Evaluate(effectScale);
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
    
}