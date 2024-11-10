using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
          
          GameMenu gameMenu = GetComponentInParent<GameMenu>() as GameMenu;
            gameMenu.CharEXP += 5;
            Destroy(gameObject);
        }
    }
}
