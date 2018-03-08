using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConcept : MonoBehaviour
{
  public int AttrBaseHitbox;
  public int AttrBaseMouvementSpeed;
  public int AttrBaseHitPoints;
  public int AttrBasePhysicalPower;
  public int AttrBaseMagicalPower;
  public int AttrBaseAttackSpeed;
  public int AttrBaseCourage;

  public int AttrHitbox;
  public int AttrMouvementSpeed;
  public int AttrHitPoints;
  public int AttrPhysicalPower;
  public int AttrMagicalPower;
  public int AttrAttackSpeed;
  public int AttrCourage;

  public int Age;
  public int Corpulence;

  public RaceModifiersData Race;
  public string Genre; // Skin
  public string Classe;

  public int RollForce;
  public int RollAdresse;
  public int RollChance;
  public int RollSavoir;
  public int RollCourage;

  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
  }

  /**
   * Si attrBaseValue = 100 / 100
   * Si modifierPct = -10 / 10
   * Alors ret -10 / 10
   */
  public int getAttributeModifier(int attrBaseVal, int modifierPct)
  {
    return Mathf.RoundToInt(attrBaseVal * modifierPct / 100);
  }
}