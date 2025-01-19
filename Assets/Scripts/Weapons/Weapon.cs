using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public EntityScriptableObject entity;
    public float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var collisionEntity)) 
        {
            if (collisionEntity.entity.entityType == EntityScriptableObject.EntityType.Player && entity.entityType== EntityScriptableObject.EntityType.Enemy)
            {
                collisionEntity.health -= damage;
                Destroy(gameObject);
            }
            if (collisionEntity.entity.entityType == EntityScriptableObject.EntityType.Enemy && entity.entityType == EntityScriptableObject.EntityType.Player)
            {
                collisionEntity.health -= damage;
                Destroy(gameObject);
            }
            if (collisionEntity.entity.entityType ==  EntityScriptableObject.EntityType.Wall) 
            {
                Destroy(gameObject);
            }
        }
        
    }
    

}


