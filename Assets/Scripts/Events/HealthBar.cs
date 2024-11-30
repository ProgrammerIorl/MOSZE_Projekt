using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Image image;
    Entity enemy;
    float maxHealth;
    private void Start()
    {
        image = GetComponent<Image>();
        enemy = GetComponentInParent<Entity>();
        maxHealth=enemy.health;
    }
    // Update is called once per frame
    void Update()
    {
        image.fillAmount = enemy.health / maxHealth;
    }
}
