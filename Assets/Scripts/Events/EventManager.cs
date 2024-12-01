using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
<<<<<<< Updated upstream

    public Dictionary<int, Enemy> enemyIDs;
    public int enemys = 0;
    public static event Action<int> EnemyDeathAfterEvent;

    public static void EnemyDeath(int id) {
        EnemyDeathAfterEvent?.Invoke(id);
        
    }
=======
    public static event UnityAction StageEnd;
    public static event UnityAction Pause;
    public static event UnityAction CoinCollected;
    public static event UnityAction Upgrade;
    public static event UnityAction GameEnd;
    public static void OnStageEnd() => StageEnd?.Invoke();
    public static void OnPause() => Pause?.Invoke();
    public static void OnCoinCollected() => CoinCollected?.Invoke();
    public static void OnUpgrade() => Upgrade?.Invoke();

    public static void OnGameEnd()=> GameEnd?.Invoke();
>>>>>>> Stashed changes
}
