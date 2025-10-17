using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Ability/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string ability;
    public string abilityID;
    public Sprite icon;
    public KeyCode keybind;
    public float duration;
    public float cooldown;
    public int cost;
    public float castTime;
    public AudioClip audioClip;

    public List<ScriptableObject> effectData = new();
}