using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{

    public float damage = 1f;
    public float projectileSpeed = 1f;
    public float fireRate = 1f;
    public GameObject projectile;
    public Sprite sprite;
    
}
