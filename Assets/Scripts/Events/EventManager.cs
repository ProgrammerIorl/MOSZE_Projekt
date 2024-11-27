using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.Events;
public static class EventManager
{
    public static event UnityAction StageEnd;
    public static event UnityAction Pause;
 
    public static void OnStageEnd() => StageEnd?.Invoke();
    public static void OnPause() => Pause?.Invoke();
>>>>>>> Stashed changes

public class EventManager : MonoBehaviour
{

    public Dictionary<int, Enemy> enemyIDs;
    public int enemys = 0;
    public static event Action<int> EnemyDeathAfterEvent;

    public static void EnemyDeath(int id) {
        EnemyDeathAfterEvent?.Invoke(id);
        
    }
}
