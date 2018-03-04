using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RaceModifiers", fileName = "RaceModifiers")]
public class RaceModifiersData : ScriptableObject
{
  public string Name;
  [Range(-50, 50)] public int Hitbox;
  [Range(-50, 50)] public int HitPoints;
  [Range(-50, 50)] public int PhysicalPower;
  [Range(-50, 50)] public int MagicalPower;
  [Range(-50, 50)] public int MouvementSpeed;
  [Range(-50, 50)] public int AttackSpeed;
}