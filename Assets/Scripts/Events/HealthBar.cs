using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Image image;
    Entity entity;
    float maxHealth;
    private void Start()
    {
        image = GetComponent<Image>();
        entity = GetComponentInParent<Entity>();
        maxHealth = entity.health;
    }
    // Update is called once per frame
    void Update()
    {
        image.fillAmount = entity.health / maxHealth;
    }
}
