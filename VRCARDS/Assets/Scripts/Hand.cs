using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Hand : MonoBehaviour {

	 public GameObject[] myHand = new GameObject[6]; // List that holds all my ten cards
     public Transform start;  //Location where to start adding my cards
     public GameObject hand;
     //public Transform HandDeck; //The hand panel reference
     public float howManyAdded; // How many cards I added so far
     public float gapFromOneItemToTheNextOne; //the gap I need between each card
 
     void Start()
     {
         howManyAdded = 0.0f;
         gapFromOneItemToTheNextOne = .5f;
     }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        FitCards();
    //        howManyAdded = 0;
    //    }
    //}

    //public void FitCards()
    // {
    //    //if (myHand.Length == 0) //if list is null, stop function
    //    //    return;
    //     for(int i=0; i < this.GetComponent<Manager>().playerHand.Count; i++)
    //    {
    //        //myHand[i].gameObject.GetComponent<Rigidbody>().useGravity = false;
    //        //this.GetComponent<Manager>().playerHand[i].gameObject.transform.position = new Vector3(start.position.x + (gapFromOneItemToTheNextOne * (i / 2)), 0 , 0);
    //        this.GetComponent<Manager>().playerHand[i].gameObject.transform.position = start.transform.position; //new Vector3(hand.transform.position.x + (gapFromOneItemToTheNextOne * (i / 2)), 0, 0);
    //        if(i > 0)
    //        {
    //             this.GetComponent<Manager>().playerHand[i].gameObject.transform.position += new Vector3((howManyAdded * gapFromOneItemToTheNextOne) / 2, 0, 0);
    //        }
    //        howManyAdded++;
    //        Debug.Log("FitCards");
    //    }
       

    //    //Image img = items [0]; //Reference to first image in my list

    //    //img.transform.position = start.position; //relocating my card to the Start Position
    //    //img.transform.position += new Vector3 (( howManyAdded*gapFromOneItemToTheNextOne), 0, 0); // Moving my card 1f to the right
    //    //img.transform.SetParents(HandDeck); //Setting my card parent to be the Hand Panel

    //    //items.RemoveAt (0);
    //    //howManyAdded++;
    //}
}
