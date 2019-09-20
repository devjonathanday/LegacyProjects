using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameManager GM;

    [Header("Health Bar")]
    public Image healthBarFG;
    public float defaultBarWidth;
    private Vector3 cachedHealthBarScale;

    void Start()
    {
        cachedHealthBarScale = Vector3.one;
        if (!GM) GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        cachedHealthBarScale.x = (float)GM.health / 100;
        healthBarFG.rectTransform.sizeDelta = new Vector2((float)GM.health / 100 * defaultBarWidth, 16);
    }
}