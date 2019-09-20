using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GameManager : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public GameObject pauseMenu;
    public bool paused;
    public bool pauseButtonPressed;

    GamePadState controllerState;

    void Update()
    {
        controllerState = GamePad.GetState(PlayerIndex.One);

        if (controllerState.Buttons.Start == ButtonState.Released)
            pauseButtonPressed = false;

        if (controllerState.Buttons.Start == ButtonState.Pressed && !pauseButtonPressed)
        {
            pauseButtonPressed = true;
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                paused = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }
        }
    }
}