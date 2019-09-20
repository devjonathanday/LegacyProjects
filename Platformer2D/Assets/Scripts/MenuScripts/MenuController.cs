using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class MenuController : MonoBehaviour
{
    GamePadState controllerState;
    public bool selectionChanged;

    public List<MenuItem> menuItems = new List<MenuItem>();
    public int focusedItem;

    private void OnEnable()
    {
        focusedItem = 0;
    }

    void Update()
    {
        controllerState = GamePad.GetState(PlayerIndex.One);

        //Reset for first-frame-only inputs
        if (controllerState.ThumbSticks.Left.Y == 0 &&
            controllerState.DPad.Down == ButtonState.Released &&
            controllerState.DPad.Up == ButtonState.Released)
            selectionChanged = false;

            //Resets all menuItem's focused state
            for (int i = 0; i < menuItems.Count; i++)
            menuItems[i].focused = false;
        //Sets the state of the focused MenuItem
        menuItems[focusedItem].focused = true;

        //Changing selected MenuItem
        if (!selectionChanged)
        {
            if (controllerState.ThumbSticks.Left.Y > 0 || controllerState.DPad.Up == ButtonState.Pressed)
            {
                focusedItem--;
                selectionChanged = true;
            }
            if (controllerState.ThumbSticks.Left.Y < 0 || controllerState.DPad.Down == ButtonState.Pressed)
            {
                focusedItem++;
                selectionChanged = true;
            }
        }

        //Looping menu to prevent out-of-bounds
        if (focusedItem < 0) focusedItem = menuItems.Count - 1;
        if (focusedItem > menuItems.Count - 1) focusedItem = 0;

    }
}
