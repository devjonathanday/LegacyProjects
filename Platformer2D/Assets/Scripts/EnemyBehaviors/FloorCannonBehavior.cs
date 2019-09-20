using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathFunctions;
public class FloorCannonBehavior : EnemyBehavior
{
    [Header("Gameplay Variables")]
    public Vector2 turretAngleLimits;
    public float currentAimAngle;
    public float turretLerp;
    public float aimDuration;
    public float turretTimer;
    public float fireOffset;
    public float missileVelocity;

    [Header("References - Self")]
    public Transform eye;
    public Transform turretAnchor;
    public ParticleSystem fireEffect;
    public AudioSource fireSound;

    [Header("References - Other")]
    public Transform player;
    public GameObject missilePrefab;

    void Start()
    {
        currentAimAngle = Random.Range(turretAngleLimits.x, turretAngleLimits.y);
    }

    void Update()
    {
        BaseEnemyUpdate();

        if (sr[0].isVisible)
        {
            Vector3 aim = player.position - turretAnchor.position;
            eye.rotation =  GetZRotationFromVector2(aim);
            turretAnchor.rotation = Quaternion.Lerp(turretAnchor.rotation, Quaternion.AngleAxis(currentAimAngle, Vector3.forward),
                                                    turretLerp);
            if (turretTimer >= aimDuration)
            {
                Shoot();
                currentAimAngle = Random.Range(turretAngleLimits.x, turretAngleLimits.y);
                turretTimer = 0;
            }
            else turretTimer += Time.deltaTime;
        }
    }

    void Shoot()
    {
        fireEffect.Play();
        fireSound.PlayOneShot(fireSound.clip);
        HomingMissileBehavior newMissile = Instantiate(missilePrefab,
                                                       turretAnchor.position + turretAnchor.right * fireOffset,
                                                       Quaternion.identity).GetComponent<HomingMissileBehavior>();
        newMissile.target = player;
        newMissile.velocity = turretAnchor.right * missileVelocity;
    }
}