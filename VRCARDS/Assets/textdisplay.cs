using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textdisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<TextMesh>().text = "Player Health 30";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
