using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/New Ability")]
public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public KeyCode keybind;
    public float cooldown;
    public int manaCost;
    public float castTime;
    public AudioClip audioClip;

    public List<ScriptableObject> effectData = new();
}