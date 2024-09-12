using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
       

    public Dictionary<int, Enemy> enemyIDs;
    public int enemys = 0;
    public static event Action<int> EnemyDeathAfterEvent;

    public static void EnemyDeath(int id) {
        EnemyDeathAfterEvent?.Invoke(id);
        
    }
}
