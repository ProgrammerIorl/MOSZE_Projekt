using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public WeaponScriptableObject lightWeapon;
    public WeaponScriptableObject heavyWeapon;
    private InputManager inputManager;
    Rigidbody2D rb;
    public Transform cameraTransform;
    Vector2 movement;
    private float playerSpeed = 5.0f;
    public float exp = 0;
    float hp = 3;
    float lastfired;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collide");
        if (collider.CompareTag("EXP"))
        {
            Destroy(collider.gameObject);
            exp += Random.Range(0, 10);
        }
        if (collider.CompareTag("EnemyProjectile"))
        {

            hp -= collider.GetComponent<WeaponScriptableObject>().damage;
            Destroy(collider.gameObject);
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        //-----------------------Movement-------------------
        if (inputManager.GetPlayerInput() != Vector2.zero)
        {
            movement = inputManager.GetPlayerInput();
            rb.velocity = movement*playerSpeed;
        }else rb.velocity = Vector2.zero;
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
        if (inputManager.HeavyShoot())
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
