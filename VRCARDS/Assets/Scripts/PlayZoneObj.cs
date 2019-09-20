 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneObj : MonoBehaviour
{
    public bool EnterField;
    public bool AttackZone;
    public bool EndTurn;
    public bool firstScene;
    public string sceneName;

    public GameObject manager;
    public GameObject controllerLeft, controllerRight;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Card")
        {
            if (EnterField)
            {
                collision.gameObject.SendMessage("EnterField", SendMessageOptions.DontRequireReceiver);
                //for(int i = 0; i < manager.GetComponent<Manager>().playerHand.Count; i++)
                //{
                //    if(collision.gameObject == manager.GetComponent<Manager>().playerHand[i])
                //    {
                //        manager.GetComponent<Manager>().playerHand.Remove(manager.GetComponent<Manager>().playerHand[i]);
                //    }
                //}
                //manager.GetComponent<Manager>().playerHand.Remove(manager.GetComponent<Manager>().playerHand[collision.gameObject.GetComponent<BaseCard>().myIndex]);
            }
            if (AttackZone)
            {
                manager.GetComponent<Manager>().combat = true; //Activates the entire combat function in LaserCollision as well
            }

            if(firstScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
            //if (EndTurn)
            //{
            //    manager.SendMessage("EnterField", SendMessageOptions.DontRequireReceiver);
            //}
        }
    }

    void Update()
    {
        //if (EndTurn)
        //{
        //    Debug.Log("End turn is true");
        //    if (manager.GetComponent<Manager>().playerTurn == true)
        //    {
        //        Debug.Log("your turn");
        //        if (Vector3.Distance(controllerLeft.transform.position, this.transform.position) < .5 || Vector3.Distance(controllerRight.transform.position, this.transform.position) < .5)
        //        {
        //            Debug.Log("inside the red thing");
        //            if (controllerLeft.GetComponent<SteamVR_TrackedController>().triggerPressed == true || controllerRight.GetComponent<SteamVR_TrackedController>().triggerPressed == true)
        //            {
        //                Debug.Log("clicking in the red thing");
        //                manager.GetComponent<Manager>().ChangeTurn();
        //            }
        //        }
        //    }
        //}
        //if (controllerRight.GetComponent<SteamVR_TrackedController>().triggerPressed == true)
        //{
        //    //Debug.Log("trigger right pressed ok with steamvr");
        //    manager.GetComponent<Manager>().ChangeTurn();
        //}
        //if (controllerLeft.GetComponent<SteamVR_TrackedController>().triggerPressed == true)
        //{
        //    //Debug.Log("trigger left pressed ok with steamvr");
        //    manager.GetComponent<Manager>().ChangeTurn();
        //}
    }
}