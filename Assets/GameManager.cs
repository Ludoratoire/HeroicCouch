using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public RaceModifiersData DwarfModifiersData;
	public RaceModifiersData ElfModifiersData;
	public RaceModifiersData HumanModifiersData;
	public RaceModifiersData OrcModifiersData;
	public RaceModifiersData TrollModifiersData;

	public List<RaceModifiersData> RaceModifiersDatas;
	
	public AttributsBaseData AttributsBaseData;

	public CharacterConcept CharacterModel;
	
	// Use this for initialization
	void Start ()
	{
		RaceModifiersDatas.Add(DwarfModifiersData);
		RaceModifiersDatas.Add(ElfModifiersData);
		RaceModifiersDatas.Add(HumanModifiersData);
		RaceModifiersDatas.Add(OrcModifiersData);
		RaceModifiersDatas.Add(TrollModifiersData);
		var characterA = this.CreateCharacter();
		
		Debug.Log(characterA);
	}

	private CharacterConcept CreateCharacter()
	{
		var player = Instantiate(CharacterModel, gameObject.transform);
		
		// Assigner les valeurs de base
		player.AttrHitbox = player.AttrBaseHitbox;
		player.AttrMouvementSpeed = player.AttrBaseMouvementSpeed;
		player.AttrHitPoints = player.AttrBaseHitPoints;
		player.AttrPhysicalPower = player.AttrBasePhysicalPower;
		player.AttrMagicalPower = player.AttrBaseMagicalPower;
		player.AttrAttackSpeed = player.AttrBaseAttackSpeed;
		player.AttrCourage = player.AttrBaseCourage;
		
		// Modifiers de base
		player.Age = UnityEngine.Random.Range(AttributsBaseData.RollStats.AgeMin, AttributsBaseData.RollStats.AdresseMax);
		player.AttrAttackSpeed += player.getAttributeModifier(player.AttrBaseAttackSpeed, player.Age - 100);
		player.AttrMouvementSpeed += player.getAttributeModifier(player.AttrBaseMouvementSpeed, player.Age - 100);
		player.AttrHitPoints += player.getAttributeModifier(player.AttrBaseHitPoints, player.Age - 100);
		
		player.Corpulence = UnityEngine.Random.Range(AttributsBaseData.RollStats.CorpulenceMin, AttributsBaseData.RollStats.CorpulenceMax);
		player.AttrMouvementSpeed = player.getAttributeModifier(player.AttrBaseMouvementSpeed, player.Corpulence - 100);
		
		player.RollForce = UnityEngine.Random.Range(AttributsBaseData.RollStats.ForceMin, AttributsBaseData.RollStats.ForceMax);
		player.AttrHitPoints = player.getAttributeModifier(player.AttrBaseHitPoints, player.RollForce - 100);
		player.AttrPhysicalPower = player.getAttributeModifier(player.AttrBasePhysicalPower, player.RollForce - 100);

		player.RollAdresse = UnityEngine.Random.Range(AttributsBaseData.RollStats.AdresseMin, AttributsBaseData.RollStats.AdresseMax);
		player.AttrAttackSpeed = player.getAttributeModifier(player.AttrBaseAttackSpeed, player.RollAdresse - 100);
		player.AttrMouvementSpeed = player.getAttributeModifier(player.AttrBaseMouvementSpeed, player.RollAdresse - 100);
		
		player.RollChance = UnityEngine.Random.Range(AttributsBaseData.RollStats.ChanceMin, AttributsBaseData.RollStats.ChanceMax);
		// Gold +
		
		player.RollSavoir = UnityEngine.Random.Range(AttributsBaseData.RollStats.SavoirMin, AttributsBaseData.RollStats.SavoirMax);
		player.AttrMagicalPower = player.getAttributeModifier(player.AttrBaseMagicalPower, player.RollSavoir- 100);
		
		player.RollCourage = UnityEngine.Random.Range(AttributsBaseData.RollStats.CourageMin, AttributsBaseData.RollStats.CourageMax);

		player.Race = RaceModifiersDatas[UnityEngine.Random.Range(0, RaceModifiersDatas.Count)];
		
		// Modifiers de race
		player.AttrHitbox = player.getAttributeModifier(player.AttrBaseHitbox, player.Race.Hitbox);
		player.AttrHitPoints = player.getAttributeModifier(player.AttrBaseHitPoints, player.Race.HitPoints);
		player.AttrPhysicalPower = player.getAttributeModifier(player.AttrBasePhysicalPower, player.Race.PhysicalPower);
		player.AttrMagicalPower = player.getAttributeModifier(player.AttrBaseMagicalPower, player.Race.MagicalPower);
		player.AttrMouvementSpeed = player.getAttributeModifier(player.AttrBaseMouvementSpeed, player.Race.MouvementSpeed);
		player.AttrAttackSpeed = player.getAttributeModifier(player.AttrBaseAttackSpeed, player.Race.AttackSpeed);
		
		return player;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
