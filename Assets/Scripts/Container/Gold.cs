using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{

    public int Amount
    {
        get;
        set;
    }

    public Gold(string name, int minGold, int maxGold)
    {
        Name = name;
        Amount = Random.Range(minGold, maxGold);
    }

}
