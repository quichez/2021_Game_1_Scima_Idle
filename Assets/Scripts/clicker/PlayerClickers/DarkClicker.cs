using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkClicker : PlayerClicker, IPlayerClick
{
    public override string Description()
    {
        return "Gain extra current health damage on click every 20 Dark levels!";
    }

    public void PlayerClick()
    {
        if (Unlocked)
            Debug.Log(ToString() + " activated!");
    }

    public override string ToString()
    {
        return "Dark Clicker";
    }
}
