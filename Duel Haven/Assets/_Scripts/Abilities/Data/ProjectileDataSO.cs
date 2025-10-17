using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ProjectileData", menuName = "Ability/ProjectileData")]

public class ProjectileDataSO : ScriptableObject
{
    public float speed;
    public float priority;
    public GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab);
    }
}