using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Header("Variables")]
    public float duration;
    public int damage;
    private float timer;
    
    [Header("References")]
    public Animator animator;
    public Collider2D hitbox;
    public AudioSource hitSound;

    private void Start()
    {
        if (!hitbox) hitbox = GetComponent<Collider2D>();
        if (!hitSound) hitSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer >= duration) hitbox.enabled = false;
        else timer += Time.deltaTime;
    }

    public void Play()
    {
        timer = 0;
        animator.SetTrigger("Activate");
        hitbox.enabled = true;
        hitSound.PlayOneShot(hitSound.clip);
    }
}