using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICost : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Upgrade += EventManagerUpgrade;
        EventManager.GameEnd += EventManagerGameEnd;
    }
    private void OnDisable()
    {
        EventManager.Upgrade -= EventManagerUpgrade;
        EventManager.GameEnd -= EventManagerGameEnd;
    }
    private void EventManagerUpgrade() 
    {
        GetComponent<TextMeshProUGUI>().text = "Cost: " + 5 * GameManager.Instance.timesUpgraded * GameManager.Instance.stageNumber;
    }
    private void EventManagerGameEnd()
    {
        GetComponent<TextMeshProUGUI>().text = "Cost: ";
    }
}
