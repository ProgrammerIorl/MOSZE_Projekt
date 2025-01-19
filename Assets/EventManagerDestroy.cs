using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerDestroy : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
