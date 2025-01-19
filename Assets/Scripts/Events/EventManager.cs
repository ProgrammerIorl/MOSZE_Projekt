
using UnityEngine;
using UnityEngine.Events;
public static class EventManager
{
    public static event UnityAction StageEnd;
    public static event UnityAction Pause;
    public static event UnityAction CoinCollected;
    public static event UnityAction Upgrade;
    public static event UnityAction GameEnd;
    public static event UnityAction Death;
    public static void OnStageEnd() => StageEnd?.Invoke();
    public static void OnPause() => Pause?.Invoke();
    public static void OnCoinCollected() => CoinCollected?.Invoke();
    public static void OnUpgrade() => Upgrade?.Invoke();
    public static void OnGameEnd() => GameEnd?.Invoke();
    public static void OnDeath() => Death?.Invoke();
}
