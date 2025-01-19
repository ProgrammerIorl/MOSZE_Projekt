using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{
    public static bool isGameOver;
    private void OnEnable()
    {
        // Feliratkozás az eseményre
        EventManager.GameEnd += UIEventManagerGameEnd;
       

    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Leiratkozás az eseményrõl, hogy elkerüljük a memóriaszivárgást
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
