using UnityEngine;
using static MathFunctions;

public class HomingMissileBehavior : MonoBehaviour
{
    [Header("Variables")]
    public Vector3 velocity;
    public float maxVelocity;
    public float acceleration;
    public float screenShakeIntensity;
    public float screenShakeDuration;
    public int contactDamage;
    public float lifeTime;
    private float lifeTimer;

    [Header("References")]
    public Transform target;
    public Rigidbody2D rBody;
    public GameObject explosionPrefab;
    public ParticleSystem smokeParticles;

    void Start()
    {
        transform.rotation = GetZRotationFromVector2(velocity);
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime) Explode();

        Vector3 trajectory = (target.position - transform.position);
        velocity += (target.position - transform.position).normalized * acceleration;
        if(velocity.magnitude > maxVelocity)
            velocity = velocity.normalized * maxVelocity;
        transform.rotation = GetZRotationFromVector2(velocity);
        transform.position += velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) //Ground layer
            Explode();
        if (collision.gameObject.layer == 12) //Player layer
        {
            collision.GetComponent<PlayerController>().TakeDamage(transform, contactDamage);
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().Shake(screenShakeIntensity, screenShakeDuration);
        smokeParticles.Stop();
        smokeParticles.transform.parent = null;
        Destroy(gameObject);
    }
}