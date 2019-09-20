using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject manager;
    //public Transform testTransform;
    //public GameObject turn1, turn2, turn3;

    //public GameObject card1, card2, card3, card4, card5, card6;

    public List<GameObject> enemyCards = new List<GameObject>();

    public Vector3[] cardSpots = new Vector3[6];
    public bool[] availableSpots = new bool[6];

    public List<GameObject> onField = new List<GameObject>();

    public int turn = 1;

    public GameObject attackParticle;

    public GameObject Player;

    public Animator animator;

    public float animTimer;
    public float timeToAnim;

    // Update is called once per frame
    void Update()
    {
        animTimer += Time.deltaTime;
        if(animTimer >= timeToAnim)
        {
            animator.SetBool("Playing", false);
        }
    }

    void Start()
    {
        //card1.SetActive(true);
    }

    public void ScriptedAI(int turnCount)
    {
        animTimer = 0;
        animator.SetBool("Playing", true);


        Debug.Log("ScriptedAI outside first Foreach");
        for (int i = 0; i < manager.GetComponent<Manager>().enemyHand.Count; i++)

        //foreach(GameObject card in manager.GetComponent<Manager>().enemyHand)
        {
            GameObject card = manager.GetComponent<Manager>().enemyHand[i];
            if (card.GetComponent<BaseCard>().essenceNeeded <= manager.GetComponent<Manager>().enemyEssence)
            {
                manager.GetComponent<Manager>().enemyEssence -= card.GetComponent<BaseCard>().essenceNeeded;
                card.SendMessage("EnterField");
                //delete?
                manager.GetComponent<Manager>().enemyHand.RemoveAt(i);
                i--;

                Debug.Log("ScriptedAI inside first Foreach");
            }
        }
        Debug.Log("Exited the first foreach");

        //foreach (bool availableSpot in manager.GetComponent<Manager>().availableSpots)
        bool found = false;

        for (int i = 0; i < manager.GetComponent<Manager>().availableSpots.Length; i++)
        {
            if(!manager.GetComponent<Manager>().availableSpots[i])
            {
                found = true;
            }
        }
            if(found)
            {
                //found = true;
                foreach (GameObject card in onField)
                {
                    if (manager.GetComponent<Manager>().onField.Count > 0)
                    {
                        if (card.GetComponent<BaseCard>().canAttack == true)
                        {
                            manager.GetComponent<Manager>().TakeDamage(manager.GetComponent<Manager>().onField[Random.Range(0, manager.GetComponent<Manager>().onField.Count)], card);
                            card.GetComponent<BaseCard>().canAttack = false;
                        }
                    }

                    Debug.Log("ScriptedAI inside second Foreach");
                }
            }
            if(!found)
            {
                foreach (GameObject card in onField)
                {
                    if (card.GetComponent<BaseCard>().canAttack == true && manager.GetComponent<Manager>().onField.Count <= 0)
                    {
                        Debug.Log("Attack Player");
                        GameObject particle = Instantiate(attackParticle, card.transform.position, Quaternion.identity) as GameObject;
                        particle.GetComponent<AttackParticle>().Fly(card, Player);
                        manager.GetComponent<Manager>().playerHP -= card.GetComponent<BaseCard>().attack;
                        card.GetComponent<BaseCard>().canAttack = false;
                    }

                }
            }
            Debug.Log("escaped for loop 1");
        Debug.Log("escaped for loop 2");
        manager.GetComponent<Manager>().playerTurn = true;
    }
    public void CheckCost()
    {
        for (int i = 0; i < manager.GetComponent<Manager>().enemyHand.Count;)
        {
            if (manager.GetComponent<Manager>().enemyHand[i].GetComponent<BaseCard>().essenceNeeded <= manager.GetComponent<Manager>().enemyEssence)
            {
                //play the card
            }
        }
    }
}