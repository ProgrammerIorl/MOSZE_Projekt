
using UnityEngine;
using UnityEngine.Events;
public static class EventManager
{
    public static event UnityAction Save;
    public static event UnityAction Pause;
 
    public static void OnSave() =>Save?.Invoke();
    public static void OnPause() => Pause?.Invoke();

}
