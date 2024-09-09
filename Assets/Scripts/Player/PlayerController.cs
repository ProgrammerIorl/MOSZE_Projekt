using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private InputManager inputManager;
    public Transform cameraTransform;
    Vector2 movement;
    Vector3 move;
    private float playerSpeed = 5.0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        //-----------------------Movement-------------------
        if (inputManager.GetPlayerInput() != Vector2.zero)
        {
            movement = inputManager.GetPlayerInput();
            move = new Vector3(movement.x, 0, 0);
            controller.Move(playerSpeed * Time.deltaTime * move);
        }




    }

}
