using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    public int minGold = 1;
    public int maxGold = 50;
    public int goldChance = 65;
    public int PotionChance = 25;
    public int ScrollChance = 5;
    public int SkinChance = 5;


    public bool type = true; //false : gold uniquement, true : tous types d'items

    public string Name
    {
        get;
        set;
    }

    public void generateLoot(Player player)
    {
        //TODO quand les autres types d'items seront implémentés
        //int itemNum = Random.Range(1, 4);
        if (type == true)
        {
            int itemChance = Random.Range(1, 101);
            if (itemChance <= goldChance)
            {
                Gold gold = new Gold("gold", minGold, maxGold);
                player.AddGold(gold.Amount);

            }
            else if (goldChance < itemChance && itemChance <= (goldChance + PotionChance))
            {
                //TODO pour une potion de vie
                Gold gold = new Gold("gold", minGold, maxGold);
            }
            else if ((goldChance + PotionChance) < itemChance && itemChance <= (goldChance + PotionChance + ScrollChance))
            {
                //TODO pour un parchemin de changement de perk
                Gold gold = new Gold("gold", minGold, maxGold);
            }
            else if ((goldChance + PotionChance + ScrollChance) < itemChance)
            {
                //TODO pour un déguisement
                Gold gold = new Gold("gold", minGold, maxGold);
            }
            else
            {
                Gold gold = new Gold("gold", minGold, maxGold);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO : Gérer le personnage
        Player player = null;

        generateLoot(player);
    }
}
