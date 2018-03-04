using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public string Name
    {
        get;
        set;
    }

    public int Gold
    {
        get;
        set;
    }

    public Player(string name, int gold)
    {
        Name = name;
        Gold = gold;
    }

    
    public void AddGold(int gain)
    {
        Gold = Gold + gain;
    }
}
