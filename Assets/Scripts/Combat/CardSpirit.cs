using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSpirit : MonoBehaviour
{
    [Header("Base")]
    public int idGlobal;
    public string idName;
    public string description;

    public List<Skill> skills;

    public SpiritTag spiritTag;

    public Rarity rarity;

    [Header("Modifiers")]
    public float addHealth;
    public float addAttack;
    public float addDefense;
    public float addSpeed;
    public float addEvasion;

    [Range(1f, 2f)]
    public float multiplerAttack = 1f;
    [Range(1f, 2f)]
    public float multiplerDefense = 1f;
    [Range(1f, 2f)]
    public float multiplerSpeed = 1f;
    [Range(1f, 2f)]
    public float multiplerEvasion = 1f;
}
