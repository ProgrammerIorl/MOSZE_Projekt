using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject experience;
    public Rigidbody2D rb;
    float lastfired;
    RaycastHit2D hit;
    LayerMask layerMask = ~0<< 6;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = entity.sprite;
        health=entity.health;

    }
    private void Update()
    {
        Vector2 line= new (transform.position.x,transform.position.y-10);
        hit = Physics2D.Linecast(transform.position, line, layerMask);
        if (hit)
        {
            if (hit.collider.CompareTag("Player")) {
                if (Time.time - lastfired > 1 / entity.weapon.fireRate)
                {
                    Fire();
                }
            }
            
        }
            
    }
    private void Fire() {
        lastfired=Time.time;
        GameObject clone = Instantiate(entity.weapon.projectile, transform.position, transform.rotation);
        clone.GetComponent<SpriteRenderer>().sprite = entity.weapon.sprite;
        clone.GetComponent<Weapon>().entity = entity;
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.down * entity.weapon.projectileSpeed);
    }
    private void OnDestroy()
    {
        
    }

}
