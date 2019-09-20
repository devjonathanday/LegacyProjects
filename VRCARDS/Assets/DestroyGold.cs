using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGold : MonoBehaviour
{

    //public Rigidbody body;
    //public GameObject target;

    public float timer;
    public float timeToDestroy;

    // Use this for initialization
    //public void DestroyGoldParticle(GameObject trg)
    //{
    //    target = trg;
    //}
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}