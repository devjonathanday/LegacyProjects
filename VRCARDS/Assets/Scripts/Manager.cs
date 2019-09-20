using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public int playerEssence, enemyEssence;
    public int playerTurnCount, enemyTurnCount;
    public bool playerTurn, enemyTurn;
    public static int deckSize = 23;
    public int deckCount = deckSize;
    public bool gameLost;
    public bool combat;

    public GameObject[] allCards;
    public GameObject[] allEnemyCards;
    public List<GameObject> playerDeck = new List<GameObject>();
    public List<GameObject> playerHand = new List<GameObject>();
    public List<GameObject> enemyDeck = new List<GameObject>();
    public List<GameObject> enemyHand = new List<GameObject>();
    public GameObject hand;
    public GameObject enemy;

    public bool[] availableSpots = new bool[6];
    public Vector3[] cardSpots = new Vector3[6];

    public TextMesh PlayerHP;
    public TextMesh EnemyHP;
    public int playerHP, enemyHP;

    public int turnCount = 0;

    public float combatTimer;
    public float combatTimeToGo;

    public float turnTimer;
    public float timeToTurn;

    public GameObject essenceCounter;

    //Testing
    public GameObject Target, Source;
    public GameObject testRight, testLeft;
    public List<GameObject> onField = new List<GameObject>();

    public GameObject winSprite, loseSprite;

    public float changeTimer;
    public float timeToChange;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        //GAME START PROCESS ---------------------------------------
        //1. Assign each card prefab to allCards[] (1 per card)
        //2. playerDeck fills itself with random picks from allCards[]
        //3. playerHand assigns its first 3 spots with the first 3 from playerDeck
        //4. The player's first turn starts, each side starts with 3 essence

        for (int i = 0; i < deckSize; i++) //Sets every card in the deck randomly
        {
            int myCard = Random.Range(0, allCards.Length);
            playerDeck.Add((GameObject)Instantiate(allCards[myCard]));
            //playerDeck[i].SendMessage("AssignIndex", i, SendMessageOptions.DontRequireReceiver);
            playerDeck[i].GetComponent<BaseCard>().myIndex = i;
        }
        for (int i = 0; i < deckSize; i++) //Sets every card in the deck randomly
        {
            int myCard = Random.Range(0, allEnemyCards.Length);
            enemyDeck.Add((GameObject)Instantiate(allEnemyCards[myCard]));
            //playerDeck[i].SendMessage("AssignIndex", i, SendMessageOptions.DontRequireReceiver);
            enemyDeck[i].GetComponent<BaseCard>().myIndex = i + 24;
        }
        PlayerDraw(3, false);
        EnemyDraw(3, false);
        //for (int i = 0; i < 3; i++)
        //{
        //    int myCard = Random.Range(0, allCards.Length);
        //    playerHand.Add((GameObject)Instantiate(allCards[myCard]));
        //    //playerHand[i].SendMessage("AssignIndex", i + playerDeck.Count, SendMessageOptions.DontRequireReceiver);
        //    playerDeck[i].GetComponent<BaseCard>().AssignIndex(i+20);
        //}
        //for (int i = 0; i < 3; i++)
        //{
        //    playerHand.Add(null);
        //}

        //for (int i = 0; i < deckSize; i++) //Sets every card in the deck randomly
        //{
        //    int myCard = Random.Range(0, allCards.Length);
        //    playerDeck.Add(allCards[myCard]);
        //}

        //cardSpots[0] = new Vector3(0, 0, 0);
        //cardSpots[1] = new Vector3(0, 0, 0);
        //cardSpots[2] = new Vector3(0, 0, 0);
        //cardSpots[3] = new Vector3(0, 0, 0);
        //cardSpots[4] = new Vector3(0, 0, 0);
        //cardSpots[5] = new Vector3(0, 0, 0);

        playerEssence = 5;
        enemyEssence = 5;

        playerTurn = true;
        enemyTurn = false;

        FitCards();
    }

    void Update()
    {
        combatTimer += Time.deltaTime;
        turnTimer += Time.deltaTime;

        PlayerHP.text ="Player Health \n " + playerHP.ToString();
        EnemyHP.text ="Enemy Health \n " + enemyHP.ToString();

        if(testLeft.GetComponent<SteamVR_TrackedController>().padPressed == true)
        {
            playerEssence = 10;
        }
        if(testRight.GetComponent<SteamVR_TrackedController>().padPressed)
        {
            PlayerDraw(1, false);
            PlayerDraw(1, false);
            PlayerDraw(1, false);
            PlayerDraw(1, false);
            PlayerDraw(1, false);
            PlayerDraw(1, false);
            Debug.Log("Player Draw Cheat");
        }
        if(testRight.GetComponent<SteamVR_TrackedController>().gripped == true || testLeft.GetComponent<SteamVR_TrackedController>().gripped)
        {
            FitCards();
        }
        if (playerEssence > 10)
        {
            playerEssence = 10;
        }
        if (enemyEssence > 10)
        {
            enemyEssence = 10;
        }
        if (Input.GetAxis("RightTrigger") > 0.5f)
        {
            Debug.Log("bioiiiiiiiiiiiii");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            FitCards();
            howManyAdded = 0;
        }

        //HpPT.text = "Player Health \n" + playerHP;
        //HpET.text = "Enemy Health \n" + enemyHP;

        //Testing
        //if (Input.GetKeyDown(KeyCode.X) == true)
        //{
        //    playerTurn = true;
        //    ChangeTurn();
        //    Debug.Log("Draw");
        //}
        //if (Input.GetKeyDown(KeyCode.C) == true)
        //{
        //    TakeDamage(Target, Source);
        //}
        //if (Input.GetKeyDown(KeyCode.L) == true)
        //{
        //    for (int i = 0; i < playerDeck.Count; i++)
        //    {
        //        Instantiate(playerDeck[i].gameObject, (hand.transform.position), Quaternion.identity);
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerTurn = false;
            ChangeTurn();
        }
        //
        //if (playerTurn == false)
        //{
        //  ChangeTurn();
        //}
        if (playerTurn)
        {
            if (timeToTurn <= turnTimer)
            {
                if (testLeft.GetComponent<SteamVR_TrackedController>().gripped == true && testRight.GetComponent<SteamVR_TrackedController>().gripped == true)
                {
                    turnTimer = 0;
                    ChangeTurn();

                    ChangeTurn();
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    for (int i = 0; i < playerHand.Count; i++)
        //    {
        //        Instantiate(playerHand[i].gameObject, (hand.transform.position), Quaternion.identity);
        //    }
        //}

        if (playerTurn)
        {
            if (combatTimer >= combatTimeToGo)
            {
                switch (combat)
                {
                    case false:
                        if (testLeft.GetComponent<SteamVR_TrackedController>().menuPressed == true || testRight.GetComponent<SteamVR_TrackedController>().menuPressed == true)
                        {
                            combatTimer = 0;
                            testRight.GetComponent<LaserCollision>().selectFirst = false;
                            combat = true;
                        }
                        break;
                    case true:
                        if (testLeft.GetComponent<SteamVR_TrackedController>().menuPressed == true || testRight.GetComponent<SteamVR_TrackedController>().menuPressed == true)
                        {
                            combatTimer = 0;
                            testRight.GetComponent<LaserCollision>().selectFirst = false;
                            combat = false;
                        }
                        break;
                }
            }

        }
    

        // end of testing

        if(playerHP <= 0)
        {
            playerHP = 0;
        }
        if (enemyHP <= 0)
        {
            enemyHP = 0;
        }

        if (playerHP <= 0)
        {
            enemy.SetActive(false);
            essenceCounter.SetActive(false);
            Debug.Log("You died!");
            loseSprite.SetActive(true);
            changeTimer += Time.deltaTime;
            if(changeTimer >= timeToChange)
            {
                ChangeScene("StartScreen");
            }

            gameLost = true;
        }
        if (enemyHP <= 0)
        {
            enemy.SetActive(false);
            essenceCounter.SetActive(false);
            winSprite.SetActive(true);
            Debug.Log("Enemy Died!");
            changeTimer += Time.deltaTime;
            if (changeTimer >= timeToChange)
            {
                ChangeScene("StartScreen");
            }
        }
    }

    public void ChangeTurn()
    {
        switch(playerTurn)
        {
            case true:
                //if (playerTurnCount % 2 > 0) //Odd Turn
                //{
                //    playerEssence += 1;
                //}
                //if (playerTurnCount % 2 == 0) //Even Turn
                //{
                //    playerEssence += 2;
                //}
                ////for (int i = 0; i < playerHand.Count; i++)
                ////{
                ////    //Destroy(playerHand[i]);
                ////    playerHand[i] = null;
                ////}
                //for (int i = 0; i > onField.Count; i++)
                //{
                //    onField[i].GetComponent<BaseCard>().canAttack = true;
                //}
                //PlayerDraw(1, true);
                //for (int i = 0; i < playerHand.Count; i++)
                //{
                //    Instantiate(playerHand[i]);
                //}
                FitCards();
                //for(int i = 0; i < availableSpots.Length; i++)
                //{
                //
                //}
                //enemyTurn = true;
                playerTurn = false;
                //Debug.Log("enemyturn");
                break;
            case false:
                Debug.Log("I actually fucking hate VR why did we do this");
                //enemyTurn = false;
                if (enemyTurnCount % 2 > 0) //Odd Turn
                {
                    enemyEssence += 1;
                }
                if (enemyTurnCount % 2 == 0) //Even Turn
                {
                    enemyEssence += 2;
                }
                foreach(GameObject card in enemy.GetComponent<Enemy>().onField)
                //for (int i = 0; i > enemy.GetComponent<Enemy>().onField.Count; i++)
                {
                    //enemy.GetComponent<Enemy>().onField[i].GetComponent<BaseCard>().canAttack = true;
                    card.GetComponent<BaseCard>().canAttack = true;
                    Debug.Log(onField.Count);
                }
                turnCount++;
                enemy.GetComponent<Enemy>().ScriptedAI(turnCount);
                if (playerTurnCount % 2 > 0) //Odd Turn
                {
                    playerEssence += 1;
                }
                if (playerTurnCount % 2 == 0) //Even Turn
                {
                    playerEssence += 2;
                }
                //for (int i = 0; i < playerHand.Count; i++)
                //{
                //    //Destroy(playerHand[i]);
                //    playerHand[i] = null;
                //}
                //for (int i = 0; i > onField.Count; i++)
                foreach(GameObject card in onField)
                {
                    //onField[i].GetComponent<BaseCard>().canAttack = true;
                    card.GetComponent<BaseCard>().canAttack = true;
                }
                PlayerDraw(1, true);
                playerTurn = true;
                //playerTurn = true;
                //ChangeTurn();
                break;
        }
    }
    public void PlayerDraw(int count, bool firstDraw)
    {
        if (firstDraw)
        {
            if (playerHand.Count < 6)
            {
                //playerDeck.CopyTo(playerHand, 0);
                //playerHand[playerHand.Count + 1] = playerDeck[deckCount];
                //hand.GetComponent<Hand>().FitCards();
                //deckCount--;

                playerHand.Add(playerDeck[playerDeck.Count - 1]);
                playerDeck.Remove(playerDeck[playerDeck.Count - 1]);
                FitCards();
                //Debug.Log(playerHand.Count);
            }
        }
        else
        {
            if (playerHand.Count < 6)
            {
                for (int i = 0; i < count; i++)
                {
                    playerHand.Add(playerDeck[playerDeck.Count - 1]);
                    playerDeck.Remove(playerDeck[playerDeck.Count - 1]);
                    FitCards();
                    //Debug.Log(playerHand.Count);
                }
            }
        }
    }

    public void EnemyDraw(int count, bool firstDraw)
    {
        if (firstDraw)
        {
            if (enemyHand.Count < 6)
            {
                //enemyDeck.CopyTo(enemyHand, 0);
                //enemyHand[enemyHand.Count + 1] = enemyDeck[deckCount];
                //hand.GetComponent<Hand>().FitCards();
                //deckCount--;

                enemyHand.Add(enemyDeck[enemyDeck.Count - 1]);
                enemyDeck.Remove(enemyDeck[enemyDeck.Count - 1]);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                enemyHand.Add(enemyDeck[enemyDeck.Count - 1]);
                enemyDeck.Remove(enemyDeck[enemyDeck.Count - 1]);
            }
        }
    }

    //Damage Variables

    public GameObject attackParticle;

    public void TakeDamage(GameObject target, GameObject source)
    {
        Debug.Log("take damage = work.jpg");
        if (target.tag == "Card")
        {
            GameObject particle = Instantiate(attackParticle, source.transform.position, Quaternion.identity) as GameObject;
            particle.GetComponent<AttackParticle>().Fly(source, target);
            Debug.Log("Issue is tag");
            if (target.GetComponent<BaseCard>().armor > 0)
            {
                if (source.GetComponent<BaseCard>().attack > target.GetComponent<BaseCard>().armor)
                {
                    target.GetComponent<BaseCard>().health -= source.GetComponent<BaseCard>().attack - target.GetComponent<BaseCard>().armor;
                    target.GetComponent<BaseCard>().armor = 0;
                    Debug.Log("armor should equal zero");
                }
                else if (source.GetComponent<BaseCard>().attack <= target.GetComponent<BaseCard>().armor)
                {
                    target.GetComponent<BaseCard>().armor -= source.GetComponent<BaseCard>().attack;
                    //Mathf.Max(target.GetComponent<BaseCard>().armor -= source.GetComponent<BaseCard>().attack, 0)
                }
            }
            else if (target.GetComponent<BaseCard>().armor <= 0)
            {
                target.GetComponent<BaseCard>().health -= source.GetComponent<BaseCard>().attack;
            }

            else if (source.GetComponent<BaseCard>().armor > 0)
            {
                if (target.GetComponent<BaseCard>().attack > source.GetComponent<BaseCard>().armor)
                {
                    source.GetComponent<BaseCard>().health -= target.GetComponent<BaseCard>().attack - source.GetComponent<BaseCard>().armor;
                    source.GetComponent<BaseCard>().armor = 0;
                }
                else if (target.GetComponent<BaseCard>().attack <= source.GetComponent<BaseCard>().armor)
                {
                    source.GetComponent<BaseCard>().armor -= target.GetComponent<BaseCard>().attack;
                    //Mathf.Max(source.GetComponent<BaseCard>().armor -= target.GetComponent<BaseCard>().attack, 0)
                }
            }
            else if (source.GetComponent<BaseCard>().armor <= 0)
            {
                source.GetComponent<BaseCard>().health -= target.GetComponent<BaseCard>().attack;
            }
        }
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            if (target.tag == "Player")
            {
                GameObject particle = Instantiate(attackParticle, source.transform.position, Quaternion.identity) as GameObject;
                particle.GetComponent<AttackParticle>().Fly(source, target);
                playerHP -= source.GetComponent<BaseCard>().attack;
                //Play that fatty take damage animation
            }
            if (target.tag == "Enemy")
            {
                GameObject particle = Instantiate(attackParticle, source.transform.position, Quaternion.identity) as GameObject;
                particle.GetComponent<AttackParticle>().Fly(source, target);
                enemyHP -= source.GetComponent<BaseCard>().attack;
                //play that fatty take damage animation
            }
        }
    }

    //public Transform HandDeck; //The hand panel reference
    public float howManyAdded; // How many cards I added so far
    public float gapFromOneItemToTheNextOne; //the gap I need between each card
    public Transform start;  //Location where to start adding my cards

    public void FitCards()
    {
        //if (myHand.Length == 0) //if list is null, stop function
        //    return;
        for (int i = 0; i < this.GetComponent<Manager>().playerHand.Count; i++)
        {
            Debug.Log("FitCards");
            playerHand[i].transform.position = start.position + new Vector3((i * gapFromOneItemToTheNextOne) / 2, 0, 0);
            playerHand[i].transform.localEulerAngles = new Vector3(0, 180, 0);
            playerHand[i].GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}