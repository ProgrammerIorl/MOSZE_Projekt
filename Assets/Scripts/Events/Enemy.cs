using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public Rigidbody2D rb;
    float lastfired;
    RaycastHit2D hit;
    LayerMask layerMask = ~0<< 6;
    private void Awake()
    {
        entity = GameManager.Instance.EnemyDatabase.GetCharacter(0);
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = entity.sprite;
        health=entity.health;
        GameManager.Instance.AddEnemyToList(gameObject);
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
        GameObject clone = Instantiate(GameManager.Instance.WeaponGameObject, transform.position, transform.rotation);
        clone.GetComponent<SpriteRenderer>().sprite = entity.weapon.sprite;
        clone.GetComponent<Weapon>().entity = entity;
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.down * entity.weapon.projectileSpeed);
    }
    private void OnDestroy()
    {
        GameObject clone = Instantiate(GameManager.Instance.coin, transform.position, transform.rotation);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.down * 3);
        GameManager.Instance.RemoveEnemyFromList(gameObject);
    }

}
