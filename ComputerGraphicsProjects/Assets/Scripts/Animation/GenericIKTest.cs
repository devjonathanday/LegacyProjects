using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericIKTest : MonoBehaviour
{
    public Transform rightShoulder, rightElbow, rightHand;
    public Transform rightHandObject;
    public float armLength, forearmLength;
    public bool calculateBoneLengths;
    public bool IKActive;
    private float shoulderAngle;

    void Start()
    {
        if(calculateBoneLengths)
        {
            armLength = Vector3.Distance(rightShoulder.position, rightElbow.position);
            forearmLength = Vector3.Distance(rightElbow.position, rightHand.position);
        }
    }

    void Update()
    {
        if (IKActive)
        {
            rightShoulder.LookAt(rightHandObject);
            rightShoulder.Rotate(transform.right, -GetMissingAngle(Vector3.Distance(rightHandObject.position, rightShoulder.position), forearmLength, armLength));
            rightElbow.LookAt(rightHandObject);
        }
    }

    float GetMissingAngle(float a, float b, float c)
    {
        if (2 * c * a != 0)
            return Mathf.Rad2Deg * Mathf.Acos((c * c + a * a - b * b) / (2 * c * a));
        else return 0;
    }
}