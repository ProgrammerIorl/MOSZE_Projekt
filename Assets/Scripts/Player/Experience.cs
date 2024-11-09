using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            InputManager inputManager = collision.collider.GetComponent<InputManager>();
            inputManager.exp += 5;
        }
    }
}
