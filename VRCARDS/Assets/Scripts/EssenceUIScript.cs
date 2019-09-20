using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceUIScript : MonoBehaviour
{
    public GameObject essence1, essence2, essence3, essence4, essence5, essence6, essence7, essence8, essence9, essence10;
    public GameObject GameManagerObj;
    public Manager managerScript;

    public Material empty;
    public Material full;

    void Start()
    {
        managerScript = GameManagerObj.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(managerScript.playerEssence)
        {
            case 0:
                essence1.GetComponent<MeshRenderer>().material = empty;
                essence2.GetComponent<MeshRenderer>().material = empty;
                essence3.GetComponent<MeshRenderer>().material = empty;
                essence4.GetComponent<MeshRenderer>().material = empty;
                essence5.GetComponent<MeshRenderer>().material = empty;
                essence6.GetComponent<MeshRenderer>().material = empty;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 1:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = empty;
                essence3.GetComponent<MeshRenderer>().material = empty;
                essence4.GetComponent<MeshRenderer>().material = empty;
                essence5.GetComponent<MeshRenderer>().material = empty;
                essence6.GetComponent<MeshRenderer>().material = empty;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 2:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = empty;
                essence4.GetComponent<MeshRenderer>().material = empty;
                essence5.GetComponent<MeshRenderer>().material = empty;
                essence6.GetComponent<MeshRenderer>().material = empty;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 3:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = empty;
                essence5.GetComponent<MeshRenderer>().material = empty;
                essence6.GetComponent<MeshRenderer>().material = empty;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 4:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = empty;
                essence6.GetComponent<MeshRenderer>().material = empty;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 5:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = full;
                essence6.GetComponent<MeshRenderer>().material = empty;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 6:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = full;
                essence6.GetComponent<MeshRenderer>().material = full;
                essence7.GetComponent<MeshRenderer>().material = empty;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 7:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = full;
                essence6.GetComponent<MeshRenderer>().material = full;
                essence7.GetComponent<MeshRenderer>().material = full;
                essence8.GetComponent<MeshRenderer>().material = empty;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 8:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = full;
                essence6.GetComponent<MeshRenderer>().material = full;
                essence7.GetComponent<MeshRenderer>().material = full;
                essence8.GetComponent<MeshRenderer>().material = full;
                essence9.GetComponent<MeshRenderer>().material = empty;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 9:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = full;
                essence6.GetComponent<MeshRenderer>().material = full;
                essence7.GetComponent<MeshRenderer>().material = full;
                essence8.GetComponent<MeshRenderer>().material = full;
                essence9.GetComponent<MeshRenderer>().material = full;
                essence10.GetComponent<MeshRenderer>().material = empty;
                break;
            case 10:
                essence1.GetComponent<MeshRenderer>().material = full;
                essence2.GetComponent<MeshRenderer>().material = full;
                essence3.GetComponent<MeshRenderer>().material = full;
                essence4.GetComponent<MeshRenderer>().material = full;
                essence5.GetComponent<MeshRenderer>().material = full;
                essence6.GetComponent<MeshRenderer>().material = full;
                essence7.GetComponent<MeshRenderer>().material = full;
                essence8.GetComponent<MeshRenderer>().material = full;
                essence9.GetComponent<MeshRenderer>().material = full;
                essence10.GetComponent<MeshRenderer>().material = full;
                break;
        }
    }
}