using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject experience;
    public Rigidbody2D rb;
    /*GameObject eventManagerGameObject;
    EventManager eventManager;
    int id;
    Enemy enemy;*/
    float health = 10f;
    public WeaponScriptableObject weapon;
    /*private void Awake()
    {
        eventManagerGameObject = GameObject.Find("EventManager") as GameObject;
        eventManager = eventManagerGameObject.GetComponent<EventManager>();
        enemy = GetComponent<Enemy>();
    }
    
    private void Start()
    {
        
        id = eventManager.enemys + 1;
        eventManager.enemyIDs.Add(id,enemy);
        EventManager.EnemyDeathAfterEvent += Death;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        health -= 1;
        if (health <= 0) 
        {
            EventManager.EnemyDeath(id);
        }
    }
    public void Death(int id) 
    {
        for (int i = 0; i < eventManager.enemyIDs.Count; i++) {
            if (eventManager.enemyIDs[id] ==gameObject) {
                Debug.Log(id);
            }
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile")) 
        {
            health -= collision.GetComponent<Weapon>().weapon.damage;
            Destroy(collision.gameObject);
            
            
        }

        
        if (health <= 0)
        {
            GameObject exp = Instantiate(experience, transform.position, transform.rotation);
            rb = exp.GetComponent<Rigidbody2D>();
            rb.velocity = transform.TransformDirection(Vector3.down * 2);
            Destroy(gameObject);
        }
    }

}
