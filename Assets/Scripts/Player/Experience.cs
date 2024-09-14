using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController pc = collision.collider.GetComponent<PlayerController>();
            pc.exp += Random.Range(0, 10);
        }
    }
}
