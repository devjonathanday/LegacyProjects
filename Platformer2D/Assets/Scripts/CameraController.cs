using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform focus;
    public float xLerp;
    public float yLerp;
    public float zDepth;

    private float shakeIntensity;
    private float shakeTimer;
    private Vector3 screenShake;

    private Vector3 desiredPosition;
    public Vector2 worldBoundsX;
    public Vector2 worldBoundsY;
    public Vector2 offset;

    public RawImage gameOverFade;
    public GameManager GM;

    void Start()
    {
        if (!GM) GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        //Check if the camera has a focus assigned
        if (focus)
        {
            //Clamp the new position
            desiredPosition.x = Mathf.Clamp(focus.position.x + offset.x, worldBoundsX.x, worldBoundsX.y);
            desiredPosition.y = Mathf.Clamp(focus.position.y + offset.y, worldBoundsY.x, worldBoundsY.y);
            desiredPosition.z = zDepth;
            //Lerp to the desired position
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, desiredPosition.x, xLerp),
                                             Mathf.Lerp(transform.position.y, desiredPosition.y, yLerp), zDepth);
        }
        //Screen shake
        if (shakeTimer > 0)
        {
            transform.position += new Vector3(Random.Range(-shakeIntensity * shakeTimer, shakeIntensity * shakeTimer),
                                              Random.Range(-shakeIntensity * shakeTimer, shakeIntensity * shakeTimer), 0);
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0) shakeTimer = 0;
        }
    }
    public void Shake(float intensity, float duration)
    {
        shakeIntensity = Mathf.Max(shakeIntensity, intensity);
        shakeTimer = Mathf.Max(shakeTimer, duration);
    }
    public IEnumerator GameOverFade(int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            gameOverFade.color = Color.black * ((float)i / duration);
            yield return null;
        }
    }
}