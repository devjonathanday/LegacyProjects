using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject visual;
    public GameObject hitEffectPrefab;
    public GameObject destroyEffectPrefab;
    public int health;

    public float hitShakeIntensity;
    public float hitShakeDuration;

    private float shakeIntensity;
    private float shakeTimer;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            visual.transform.position = originalPosition + new Vector3(Random.Range(-shakeIntensity * shakeTimer, shakeIntensity * shakeTimer),
                                              Random.Range(-shakeIntensity * shakeTimer, shakeIntensity * shakeTimer), 0);
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0) shakeTimer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //9 = PlayerAttack layer
        {
            health--;
            if (health <= 0)
            {
                Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            collision.GetComponent<HitEffect>().hitbox.enabled = false;
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            shakeIntensity = hitShakeIntensity;
            shakeTimer = hitShakeDuration;
        }
    }
}
