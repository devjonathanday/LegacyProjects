using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("References - Self")]
    public SpriteRenderer[] sr;

    [Header("Generic Enemy Variables")]
    public int health;
    public int contactDamage;
    public float screenShakeIntensity;
    public float screenShakeDuration;

    [Header("References - Effects")]
    public GameObject damageEffect;
    public GameObject deathExplosionPrefab;

    [Header("Visuals")]
    public bool invincible;
    public float invincibilityDuration;
    public float invincibilityTimer;
    public bool rendering;
    public Color rendererColor;

    public void BaseEnemyUpdate()
    {
        //Invincibility frames
        if (invincible)
        {
            rendering = !rendering;
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration) invincible = false;
        }
        else rendering = true;

        for (int i = 0; i < sr.Length; i++)
        {
            if (rendering) sr[i].color = rendererColor;
            else sr[i].color = Color.clear;
        }
    }

    public void Death()
    {
        Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().Shake(screenShakeIntensity, screenShakeDuration);
        Destroy(gameObject);
        //Spawn loot or something idk
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //9 = PlayerAttack layer
        {
            HitEffect hit = collision.GetComponent<HitEffect>();
            health -= hit.damage;
            hit.hitbox.enabled = false;
            if (health <= 0) Death();
            else
            {
                invincible = true;
                invincibilityTimer = 0;
                Instantiate(damageEffect, transform.position, Quaternion.identity);
            }
        }
    }
}