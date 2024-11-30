using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Entity
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity)) 
        {
            if (entity.entity.entityType == EntityScriptableObject.EntityType.Wall)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
