using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public Rigidbody2D rb;
    protected float lastfired;
    protected RaycastHit2D hit;
    protected LayerMask layerMask = ~0 << 6;
    protected float timeMoved;
    protected bool isMoving=false;
    protected GameObject HealthBar;
    private void Awake()
    {
        entity = GameManager.Instance.EnemyDatabase.GetCharacter(0);
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = entity.sprite;
        health=entity.health*GameManager.Instance.roundNumber*GameManager.Instance.stageNumber;
        GameManager.Instance.AddEnemyToList(gameObject);
        rb = GetComponent<Rigidbody2D>();
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
        if (health<=0)
        {
            Death();
        }
        if (Time.time - timeMoved > 2) 
        {
            Move();
        }
        
    }
    public void Move()
    {


        if (!isMoving)
        {
            rb.velocity = new Vector2(1, 0) * 2;
        }
        else
        {
            rb.velocity = new Vector2(-1, 0) * 2;
        }

        isMoving = !isMoving;
        timeMoved = Time.time;
        


    }
    public void Fire() {
        lastfired=Time.time;
        GameObject clone = Instantiate(GameManager.Instance.WeaponGameObject, transform.position, transform.rotation);
        WeaponDataSet(clone);
        ProjectileShoot(clone);
        
    }
    protected void Death()
    {
        GameObject clone = Instantiate(GameManager.Instance.coin, transform.position, transform.rotation);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.down * 3);
        GameManager.Instance.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }
    protected void WeaponDataSet(GameObject clone) 
    {
        clone.GetComponent<SpriteRenderer>().sprite = entity.weapon.sprite;
        clone.GetComponent<Weapon>().entity = entity;
    }
    protected void ProjectileShoot(GameObject clone)
    {
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.down * entity.weapon.projectileSpeed);
    }

}
