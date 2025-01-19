using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeathScreen : MonoBehaviour
{
    public static bool isDeath;
    private void OnEnable()
    {
        // Feliratkoz�s az esem�nyre
        EventManager.Death += EventDeathScreen;


    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (isDeath)
        {
            EventManager.Death -= EventDeathScreen;

        }

    }

    
    void EventDeathScreen() 
    { 
        gameObject.SetActive(true);    
    }
}
