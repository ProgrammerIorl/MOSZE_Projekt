using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public WeaponScriptableObject lightWeapon;
    public WeaponScriptableObject heavyWeapon;
    private CharacterController controller;
    private InputManager inputManager;
    public Transform cameraTransform;
    Vector2 movement;
    Vector2 move;
    private float playerSpeed = 5.0f;
    float lastfired;

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
            move = new Vector3(movement.x, movement.y);
            controller.Move(playerSpeed * Time.deltaTime * move);
        }
        //-----------------------Shooting-------------------
        if (inputManager.LightShoot())
        {
            if (Time.time - lastfired > 1 / lightWeapon.fireRate)
            {
                lastfired = Time.time;
                GameObject clone = Instantiate(lightWeapon.projectile, transform.position, transform.rotation);
                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                rb.velocity = transform.TransformDirection(Vector3.up * lightWeapon.projectileSpeed);
            }
        }
        if (inputManager.LightShoot())
        {
            if (Time.time - lastfired > 1 / heavyWeapon.fireRate)
            {
                lastfired = Time.time;
                GameObject clone = Instantiate(heavyWeapon.projectile, transform.position, transform.rotation);
                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                rb.velocity = transform.TransformDirection(Vector3.up * heavyWeapon.projectileSpeed);
            }
        }




    }

}
