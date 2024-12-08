using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entity")]
public class EntityScriptableObject : ScriptableObject
{
    public enum EntityType
    {
        Enemy,
        Player,
        Wall,
        Droppable
    }
    public EntityType entityType;
    public float health;
    public Sprite sprite;
    public WeaponScriptableObject weapon;
    public WeaponScriptableObject heavyweapon;
    public string characterName;
}
