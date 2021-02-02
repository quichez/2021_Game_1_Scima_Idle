using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightClicker : PlayerClicker, IPlayerClick
{
    public override string Description()
    {
        return "Gain extra missing health damage on click every 20 Light levels!";
    }

    public void PlayerClick()
    {
        if (Unlocked)
            Debug.Log(ToString() + " activated!");
    }

    public override string ToString()
    {
        return "Light Clicker";
    }
}
