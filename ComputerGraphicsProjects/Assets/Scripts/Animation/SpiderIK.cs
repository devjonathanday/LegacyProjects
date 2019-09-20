using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIK : MonoBehaviour
{
    [System.Serializable]
    public class Leg
    {
        public Transform baseJoint, bendJoint, jointEnd;
        public Vector3 currentTarget, targetOffset;
        [HideInInspector] public Vector3 desiredTarget;
        public float firstLimbLength, secondLimbLength;
    }

    public Leg[] legs;
    public float minTargetDistance;
    public float maxTargetDistance;
    public float legMovementLerp;
    public bool calculateBoneLengths;
    public bool IKActive;

    public float moveSpeed;
    public Vector3 velocity;
    public float drag;

    void Start()
    {
        if (calculateBoneLengths)
        {
            for (int i = 0; i < legs.Length; i++)
            {
                legs[i].firstLimbLength = Vector3.Distance(legs[i].baseJoint.position, legs[i].bendJoint.position);
                legs[i].secondLimbLength = Vector3.Distance(legs[i].bendJoint.position, legs[i].jointEnd.position);
                legs[i].desiredTarget = legs[i].currentTarget;
            }
        }
    }

    void Update()
    {
        if (IKActive)
        {
            for (int i = 0; i < legs.Length; i++)
            {
                //Reposition the leg's target if it is far enough away
                float legTargetDistance = Vector3.Distance(legs[i].baseJoint.position, legs[i].currentTarget);
                if (legTargetDistance > maxTargetDistance || legTargetDistance < minTargetDistance)
                    legs[i].desiredTarget = transform.position + legs[i].targetOffset;

                legs[i].currentTarget = Vector3.Lerp(legs[i].currentTarget, legs[i].desiredTarget, legMovementLerp);
                //Rotate the leg to snap to its target
                legs[i].baseJoint.LookAt(legs[i].currentTarget);
                legs[i].baseJoint.Rotate(transform.right,
                    GetMissingAngle(Vector3.Distance(legs[i].currentTarget, legs[i].baseJoint.position),
                    legs[i].secondLimbLength, legs[i].firstLimbLength));
                legs[i].bendJoint.LookAt(legs[i].currentTarget);
            }
        }

        if (Input.GetKey(KeyCode.W)) velocity.y += moveSpeed;
        if (Input.GetKey(KeyCode.A)) velocity.x -= moveSpeed;
        if (Input.GetKey(KeyCode.S)) velocity.y -= moveSpeed;
        if (Input.GetKey(KeyCode.D)) velocity.x += moveSpeed;
        velocity *= drag;
        transform.Translate(new Vector3(-velocity.x, 0, -velocity.y));
    }

    float GetMissingAngle(float a, float b, float c)
    {
        return Mathf.Rad2Deg * Mathf.Acos((c * c + a * a - b * b) / (2 * c * a));
    }
}