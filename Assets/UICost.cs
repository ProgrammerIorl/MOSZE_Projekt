using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICost : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Upgrade += EvenManagerUpgrade;
    }
    private void OnDisable()
    {
        EventManager.Upgrade -= EvenManagerUpgrade;
    }
    private void EvenManagerUpgrade() 
    {
        GetComponent<TextMeshProUGUI>().text = "Cost: " + 5 * GameManager.Instance.timesUpgraded * GameManager.Instance.stageNumber;
    }
}
