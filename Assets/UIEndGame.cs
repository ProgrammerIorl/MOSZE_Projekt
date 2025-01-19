using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{
    public static bool isGameOver;
    private void OnEnable()
    {
        // Feliratkoz�s az esem�nyre
        EventManager.GameEnd += UIEventManagerGameEnd;
       

    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Leiratkoz�s az esem�nyr�l, hogy elker�lj�k a mem�riasziv�rg�st
    }

    private void UIEventManagerGameEnd()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameObject.SetActive(true);
            Debug.Log("set active true");
        }
        
    }
}
