using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;



public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private PlayerInput PlayerInput;
    
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        PlayerInput = new PlayerInput();
        
    }
    private void OnEnable()
    {
        PlayerInput.Enable();
    }
    private void OnDisable()
    {
        PlayerInput.Disable();
    }
    public Vector2 GetPlayerInput()
    {
        return PlayerInput.Player.Movement.ReadValue<Vector2>();
    }
    public bool LightShoot()
    {
        return PlayerInput.Player.LightShoot.triggered;
    }
    public bool HeavyShoot()
    {
        return PlayerInput.Player.HeavyShoot.triggered;
    }


}
