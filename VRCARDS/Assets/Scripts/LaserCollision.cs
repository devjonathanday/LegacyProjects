using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{

    public GameObject source;
    public GameObject target;
    public GameObject manager;
    public bool selectFirst, selectSecond;
    public LayerMask cardLayer, enemyLayer;
    public GameObject LaserLine;
    public float waitTime = 2;
    public float timer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(manager.GetComponent<Manager>().combat)
        {
            LaserLine.GetComponent<LineRenderer>().enabled = true;
        }
        if (!manager.GetComponent<Manager>().combat)
        {
            LaserLine.GetComponent<LineRenderer>().enabled = false;
        }
        timer += Time.deltaTime;
        if (manager.GetComponent<Manager>().combat == true)
        {
            selectTargets();
        }
    }

    public void selectTargets()
    {
        //Debug.Log("working");
        //LaserLine.GetComponent<LineRenderer>().enabled = true;
        if (selectFirst == false)
        {
            LaserLine.GetComponent<LineRenderer>().endColor = Color.red;
            LaserLine.GetComponent<LineRenderer>().startColor = Color.red;
            if (this.GetComponent<SteamVR_TrackedController>().triggerPressed == true)
            {
                //RaycastHit hit;
                //Ray ray = Physics.Raycast(this.transform.position, new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, this.transform.forward, out hit, Mathf.Infinity, cardLayer))
                {
                    Debug.DrawRay(transform.position, this.transform.forward * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                    selectFirst = true;
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
                if (selectFirst == true && hit.collider.GetComponent<BaseCard>().canAttack == true)
                {
                    source = hit.collider.gameObject;
                    //source.GetComponent<BaseCard>().canAttack = false;
                    timer = 0;
                    Debug.Log("setti spaghetti");
                }
            }
        }
        else if (selectFirst == true)
        {
            LaserLine.GetComponent<LineRenderer>().endColor = Color.yellow;
            LaserLine.GetComponent<LineRenderer>().startColor = Color.yellow;
            if (timer >= waitTime)
                if (this.GetComponent<SteamVR_TrackedController>().triggerPressed == true)
                {
                    //RaycastHit hit;
                    //Ray ray = Physics.Raycast(this.transform.position, new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(transform.position, this.transform.forward, out hit, Mathf.Infinity, cardLayer))
                    {
                        Debug.DrawRay(transform.position, this.transform.forward * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                    }
                    else if (Physics.Raycast(transform.position, this.transform.forward, out hit, Mathf.Infinity, enemyLayer))
                    {
                        Debug.DrawRay(transform.position, this.transform.forward * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                        Debug.Log("Did not Hit");
                    }
                        target = hit.collider.gameObject;

                }
        }
        if (target != source)
        {
            manager.GetComponent<Manager>().TakeDamage(target, source);
            source.GetComponent<BaseCard>().canAttack = false;
            manager.GetComponent<Manager>().combat = false;
            Debug.Log("Take that PHATTY damage");
        }
        else if(target == source)
        {
            source = null;
            target = null;
            selectFirst = false;

        }
        source = null;
        target = null;
        selectFirst = false;
    }
    public void Attack()
    {
        
    }
}
