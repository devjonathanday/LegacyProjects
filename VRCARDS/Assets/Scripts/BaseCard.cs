using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    //Basic Variables
    public int maxHealth;
    public int health;
    public int armor;
    public int attack;
    public int essenceNeeded;
    public GameObject Model;
    public bool inField;
    public bool dead;
    public bool enemyCard;
    public Transform graveyardPile;
    //Assigned Objects
    public GameObject GM; //Game Manager, assigned automatically
    private Manager manager; //Manager script, assigned automatically
    public GameObject enemy;

    //Card Positions
    public int cardSpot;
    public Vector3 assignedCardSpot;
    public bool go;

    public TextMesh StatDisplay;
    public TextMesh essenceText;
    public TextMesh attackText;
    public TextMesh HPText;
    public TextMesh armorText;

    public GameObject goldParticle;

    public Transform statRef;
    public Transform Attack;
    public Transform Armour;
    public Transform Health;

    //Testing
    public bool canAttack;

    public int myIndex;

    public bool destroy;

    void Start()
    {
        manager = GM.GetComponent<Manager>();
        health = maxHealth;
        destroy = false;
    }

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("Manager");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        manager = GM.GetComponent<Manager>();
        health = maxHealth;
    }

    void Update()
    {

        //if (!inField)
        //{
        //    myIndex = manager.playerHand.FindIndex(i => i.Equals(this));
        //}
        //StatDisplay.text = " " + attack + armor + health;
        if (this.health <= 0)
        {
            Debug.Log("no health statement");
            if (enemyCard == false)
            {
                dead = true;
                Debug.Log("in enemy card statement");
                //for (int o = 0; o < manager.onField.Count; o++)
                //{
                //    if (myIndex == manager.onField[o].GetComponent<BaseCard>().myIndex)
                //    {
                //        manager.playerHand.Remove(manager.onField[o]);
                //        manager.availableSpots[o] = true;
                //        Debug.Log("My Index removing something working i think its onfield but maybe not Working");
                //    }
                //}
                for (int i = 0; i < manager.onField.Count; i++)

                //foreach(GameObject card in manager.GetComponent<Manager>().enemyHand)
                {
                    GameObject card = manager.GetComponent<Manager>().onField[i];
                    Debug.Log("in for statement");
                    if (myIndex == card.GetComponent<BaseCard>().myIndex)
                    {
                        manager.availableSpots[i] = true;
                        Debug.Log("in My Index if statement");
                        if (manager.availableSpots[i] == true)
                        {
                            Debug.Log("in manager avalible spots statement");
                            manager.onField.Remove(card);
                            destroy = true;
                        }
                    }

                }
            }
            if (enemyCard == true)
            {
                dead = true;
                for (int i = 0; i < enemy.GetComponent<Enemy>().onField.Count; i++)

                //foreach(GameObject card in manager.GetComponent<Manager>().enemyHand)
                {
                    GameObject card = enemy.GetComponent<Enemy>().onField[i];
                    {
                        if (myIndex == card.GetComponent<BaseCard>().myIndex)
                        {
                            enemy.GetComponent<Enemy>().availableSpots[i] = true;
                            enemy.GetComponent<Enemy>().onField.Remove(card);
                            destroy = true;
                        }
                    }
                }
                //for (int o = 0; o < enemy.GetComponent<Enemy>().onField.Count; o++)
                //{
                //    if (myIndex == enemy.GetComponent<Enemy>().onField[o].GetComponent<BaseCard>().myIndex)
                //    {
                //        manager.enemyHand.Remove(enemy.GetComponent<Enemy>().onField[o]);
                //        Debug.Log("My Index removing something Working");
                //    }
                //    if (myIndex == enemy.GetComponent<Enemy>().onField[o].GetComponent<BaseCard>().myIndex)
                //    {
                //        enemy.GetComponent<Enemy>().availableSpots[o] = true;
                //        Debug.Log("My Index making spots available Working");
                //    }
                //}
            }
            if (destroy)
            {
                //transform.position = graveyardPile.position;
                Model.SetActive(false);
                Instantiate(goldParticle);
                Destroy(this.gameObject);
            }
        }
        essenceText.text = essenceNeeded.ToString();
        attackText.text = attack.ToString();
        HPText.text = health.ToString();
        armorText.text = armor.ToString();
    }

    public void ActivatePointer()
    {

    }

    public void EnterField()
    {
        //transform.eulerAngles = new Vector3(0, 0, 0);
        //this.GetComponent<Rigidbody>().isKinematic = true;
        //bool found = false;
        if (enemyCard == false)
        {
            for (int i = 0; i < manager.availableSpots.Length && inField == false; i++)
            {
                if (manager.availableSpots[i] == true)
                {
                    Debug.Log("available spot " + i);
                    if (inField == false && dead == false && manager.playerEssence >= essenceNeeded)
                    {
                        Debug.Log("Inside Enter Field second if");
                        manager.playerEssence -= essenceNeeded;
                        manager.availableSpots[i] = false;
                        assignedCardSpot = manager.cardSpots[i];
                        transform.position = assignedCardSpot;
                        Model.SetActive(true);
                        //RotateNumbers();

                        inField = true;
                        canAttack = true;

                        Debug.Log("current i " + i);

                        for (int o = 0; o < manager.playerHand.Count; o++)
                        {
                            if (myIndex == manager.playerHand[o].GetComponent<BaseCard>().myIndex)
                            {
                                manager.onField.Add(this.gameObject);
                                manager.playerHand.Remove(manager.playerHand[o]);
                                Debug.Log("My Index Working");
                                RotateNumbers();

                            }
                        }
                    }

                }
                //manager.onField.Add(this.gameObject);
                manager.FitCards();

            }
            if (inField == true)
            {
                transform.position = assignedCardSpot;
            }
        }
        if (enemyCard == true)
        {
            for (int i = 0; i < enemy.GetComponent<Enemy>().availableSpots.Length && inField == false; i++)
            {
                Debug.Log("Inside Enter Field For LOOP");
                if (enemy.GetComponent<Enemy>().availableSpots[i] == true)
                {
                    Debug.Log("Inside Enter Field availableSpots if");
                    if (inField == false && dead == false)
                    {
                        Debug.Log("Inside Enter Field second if");
                        enemy.GetComponent<Enemy>().availableSpots[i] = false;
                        assignedCardSpot = enemy.GetComponent<Enemy>().cardSpots[i];
                        transform.position = assignedCardSpot;
                        Model.SetActive(true);
                        //RotateNumbers();

                        inField = true;
                        canAttack = true;
                        enemy.GetComponent<Enemy>().onField.Add(this.gameObject);
                        RotateNumbers();

                    }

                }


            }
            if (inField == true)
            {
                transform.position = assignedCardSpot;
            }
        }

        //go = false;
    }
    public void AssignIndex(int i)
    {
        myIndex = i;
    }
    public void RotateNumbers()
    {
        //HPText.gameObject.transform.position = new Vector3(0, 0.1f + this.gameObject.transform.position.y, 0);
        //armorText.gameObject.transform.position = new Vector3(0, 0.1f + this.gameObject.transform.position.y, 0);
        //attackText.gameObject.transform.position = new Vector3(0, 0.1f + this.gameObject.transform.position.y, 0);
        //HPText.gameObject.transform.eulerAngles = new Vector3(HPText.gameObject.transform.eulerAngles.x - 90, 180, 0);
        //armorText.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        //attackText.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        //HPText.transform.position = Health.transform.position;
        Debug.Log("I done got called");
        //        HPText.gameObject.transform.position = new Vector3(HPText.transform.position.x, Attack.position.y, HPText.transform.position.z);
        HPText.gameObject.transform.position = (Health.position + new Vector3(0, 0, -0.01f));
        armorText.gameObject.transform.position = (Armour.position + new Vector3(0, 0, -0.01f));
        attackText.gameObject.transform.position = (Attack.position + new Vector3(0, 0, -0.01f));
        //        HPText.transform.eulerAngles = statRef.eulerAngles;
        armorText.transform.eulerAngles = statRef.eulerAngles;
        attackText.transform.eulerAngles = statRef.eulerAngles;
        HPText.transform.eulerAngles = statRef.eulerAngles;
        essenceText.gameObject.SetActive(false);
    }
}