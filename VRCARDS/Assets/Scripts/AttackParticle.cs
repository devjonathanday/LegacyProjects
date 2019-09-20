using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    public Rigidbody body;
    public float speed;
    public GameObject target;
    public Transform destroySpot;
    public float timer;
    public float timeToDestroy;

    public void Fly(GameObject src, GameObject trg)
    {
        transform.LookAt(trg.transform.position);
        body.velocity = speed * transform.forward;
        target = trg;
        destroySpot = trg.transform;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, destroySpot.position) < 0.1f)
        {
            Destroy(this.gameObject);
        }

        //timer += Time.deltaTime;
        //if (target = null)
        //{
        //    if (timer >= timeToDestroy)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DestroyParticle")
        {
            Destroy(this.gameObject);
        }
    }

}