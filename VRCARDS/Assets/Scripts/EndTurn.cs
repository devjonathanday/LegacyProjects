using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

    public GameObject controllerLeft, controllerRight;
    public GameObject manager;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (manager.GetComponent<Manager>().playerTurn == true)
        {
            if (controllerLeft.transform.position == this.transform.position || controllerRight.transform.position == this.transform.position)
            {
                if (controllerLeft.GetComponent<SteamVR_TrackedController>().triggerPressed == true || controllerRight.GetComponent<SteamVR_TrackedController>().triggerPressed == true)
                {
                    manager.GetComponent<Manager>().ChangeTurn();
                }
            }
        }
	}
}
