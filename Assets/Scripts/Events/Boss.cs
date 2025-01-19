using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    private void Awake()
    {
        entity = GameManager.Instance.EnemyDatabase.GetCharacter(0);
    }
    private void Start()
    {
        transform.localScale = new Vector2(5, 5);
        GetComponent<Boss>().health = entity.health * GameManager.Instance.stageNumber * GameManager.Instance.roundNumber * 10;
        GameManager.Instance.AddEnemyToList(gameObject);
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = entity.sprite;
    }
    private void Update()
    {
        Vector2 line = new(transform.position.x, transform.position.y - 10);
        hit = Physics2D.Linecast(transform.position, line, layerMask);
        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (Time.time - lastfired > 1 / entity.weapon.fireRate)
                {
                    Fire();
                }
            }
        }
        if (health <= 0)
        {
            Death();
        }
        if (Time.time - timeMoved > 5)
        {
            Move();
        }

    }

    public new void Fire()
    {
        lastfired = Time.time;
        GameObject clone = Instantiate(GameManager.Instance.WeaponGameObject, transform.position, transform.rotation);
        WeaponDataSet(clone);
        ProjectileShoot(clone);

    }

    private new void WeaponDataSet(GameObject clone)
    {
        clone.GetComponent<SpriteRenderer>().sprite = entity.weapon.sprite;
        clone.GetComponent<Weapon>().entity = entity;
        clone.GetComponent<Weapon>().damage = entity.weapon.damage * GameManager.Instance.stageNumber*3;
        clone.transform.localScale = new Vector2(3, 3);

    }
}
