using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AttributsBase", fileName = "AttributsBase")]
public class AttributsBaseData : ScriptableObject
{
  [Serializable]
  public class CharacterAttributsBase
  {
    public int MouvementSpeed = 100;
    public int HitPoints = 100;
    public int MagicalPower = 100;
    public int PhysicalPower = 100;
    public int AttackSpeed = 100;
    public int Courage = 20;
  }

  public CharacterAttributsBase Character;

  [Serializable]
  public class RollStatsBase
  {
    public int AgeMin = 90;
    public int AgeMax = 110;
    
    [Space(1)]
    public int CorpulenceMin = 90;
    public int CorpulenceMax = 110;
    
    [Space(1)]
    public int ForceMin = 80;
    public int ForceMax = 120;
    
    [Space(1)]
    public int AdresseMin = 80;
    public int AdresseMax = 120;
    
    [Space(1)]
    public int ChanceMin = 80;
    public int ChanceMax = 120;
    
    [Space(1)]
    public int SavoirMin = 80;
    public int SavoirMax = 120;
    
    [Space(1)]
    public int CourageMin = 20;
    public int CourageMax = 40;
  }

  public RollStatsBase RollStats;
}