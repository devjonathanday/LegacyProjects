using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "card" && collision.gameObject.GetComponent<BaseCard>().inField == true)
        {
            collision.gameObject.SendMessage("ActivatePointer", SendMessageOptions.DontRequireReceiver);
        }
    }
}
