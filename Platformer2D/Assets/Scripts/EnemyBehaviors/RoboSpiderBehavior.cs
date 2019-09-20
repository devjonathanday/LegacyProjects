using System.Collections;
using UnityEngine;

public class RoboSpiderBehavior : EnemyBehavior
{
    [Header("Movement Variables")]
    public float walkSpeed;
    public float rotationDuration; //How many frames the spider takes to complete rotation
    public Vector3 direction;

    [Header("Ground Detection")]
    public float groundCheckDistance;
    public float groundCheckOffset;
    public float frontCheckDistance;
    public LayerMask groundLayer;

    [Header("References")]
    public GameObject sprite;

    //private Animator animator;

    private IEnumerator rotateCoroutine;

    void Update()
    {
        BaseEnemyUpdate();

        if (sr[0].isVisible) //Will trigger if renderer is visible in scene view!
        {
            //Move the spider
            transform.position += (direction.normalized * walkSpeed);

            //Check if there is no floor below the spider (descend the ledge)
            if (!Physics2D.Raycast(transform.position + (transform.right * groundCheckOffset), -transform.up, groundCheckDistance, groundLayer) &&
                !Physics2D.Raycast(transform.position - (transform.right * groundCheckOffset), -transform.up, groundCheckDistance, groundLayer))
            {
                //Change the direction of movement
                direction = Quaternion.Euler(0, 0, 90) * direction;
                //Rotate to the new direction
                transform.Rotate(transform.forward, 90);
                //Start the coroutine for the sprite's rotation (separate from object rotation)
                rotateCoroutine = Rotate(false);
                StartCoroutine(rotateCoroutine);
            }
            //Check if there is a wall in front of the spider (climb the wall)
            if (Physics2D.Raycast(transform.position, -transform.right, frontCheckDistance, groundLayer))
            {
                //Change the direction of movement
                direction = Quaternion.Euler(0, 0, -90) * direction;
                //Rotate to the new direction
                transform.Rotate(transform.forward, -90);
                //Start the coroutine for the sprite's rotation (separate from object rotation)
                rotateCoroutine = Rotate(true);
                StartCoroutine(rotateCoroutine);
            }
        }
    }

    IEnumerator Rotate(bool clockwise)
    {
        //Subtract 90 degrees from the sprite's rotation to counteract the parent's rotation
        sprite.transform.eulerAngles += (clockwise ? Vector3.forward : -Vector3.forward) * 90;
        //Rotates the sprite 90 degrees spanning a given number of frames
        for (int t = 0; t < rotationDuration; t++)
        {
            sprite.transform.eulerAngles += (clockwise ? -Vector3.forward : Vector3.forward) * (90 / rotationDuration);
            yield return null;
        }
    }
}

//USAGE

//The position and speed must be synced such that its corner and the ground's corner will line up *on the frame*
//E.g. Position = (0,   0, 0), Speed = 1 works, but
//     Position = (0.5, 0, 0), Speed = 1 does not
//because the spider will gap itself from the corner in between frames, causing its legs to be separated from the ground.