using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity;
    public float speed;
    public float drag;
    public float jumpForce;
    public bool invincible;
    public Animator animator;
    public AnimationCurve animSpeedCurve;

    private MeshRenderer[] meshRenderers;

    private void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        velocity *= drag;

        if(Input.GetKey(KeyCode.W) && velocity.z < 2)
            velocity -= transform.forward * speed;
        if (Input.GetKey(KeyCode.S)  && velocity.z > -2)
            velocity += transform.forward * speed;
        if(Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Jump");
        if (Input.GetKeyDown(KeyCode.X))
            animator.SetTrigger("Hurt");

        if (velocity.sqrMagnitude / 2 > 0.001f)
        {
            animator.speed = animSpeedCurve.Evaluate(velocity.sqrMagnitude / 2);
            animator.SetFloat("RunSpeed", velocity.sqrMagnitude / 2);
        }
        else animator.speed = 1;
        transform.Translate(velocity);

        if (invincible)
            for (int i = 0; i < meshRenderers.Length; i++) meshRenderers[i].enabled = !meshRenderers[i].enabled;
        else for (int i = 0; i < meshRenderers.Length; i++) meshRenderers[i].enabled = true;
    }
    
    public void Jump()
    {
        Debug.Log("Jump");
        velocity.y += jumpForce;
    }

    public void SetInvincible()
    {
        invincible = true;
    }

    public void SetVulnerable()
    {
        invincible = false;
    }
}