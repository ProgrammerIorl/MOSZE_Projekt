using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public EntityScriptableObject entity;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity collisionEntity = collision.GetComponent<Entity>();
        if (collisionEntity != null) 
        {
            if (collisionEntity.entity.entityType != entity.entityType)
            {
                collisionEntity.health -= entity.weapon.damage;
                Destroy(gameObject);
                if (collisionEntity.health <= 0) {
                    Destroy(collisionEntity.gameObject);
                }
            }
            
        }
        
    }
    

}


