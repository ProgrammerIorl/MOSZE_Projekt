using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;



public class InputManager : Entity
{
    private static InputManager _instance;
    private PlayerInput PlayerInput;
    Rigidbody2D rb;
    public Transform cameraTransform;
    Vector2 movement;
    private float playerSpeed = 5.0f;
    public float exp = 0;
    float lastfired;
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
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraTransform = Camera.main.transform;
        lastfired = 0;
        GetComponent<SpriteRenderer>().sprite=entity.sprite;
        health=entity.health;
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
    /*public void LightShoot()
    {
        
        PlayerInput.Player.LightShoot.performed += context =>
        {
           

            // Check if it's either a tap or hold interaction
            if (context.interaction is TapInteraction || context.interaction is HoldInteraction)
            {
               

                // Check the fire rate
                if (Time.time - lastfired > 1 / lightWeapon.fireRate)
                {
                    

                    lastfired = Time.time;

                    // Instantiate the projectile and set properties
                    GameObject clone = Instantiate(
                        lightWeapon.projectile,
                        gameObject.GetComponentInChildren<Transform>().Find("LightWeaponTransform").position,
                        transform.rotation
                    );

                    clone.GetComponent<Weapon>().weapon = lightWeapon;
                    clone.GetComponent<SpriteRenderer>().sprite = lightWeapon.sprite;

                    Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                    rb.velocity = transform.TransformDirection(Vector3.up * lightWeapon.projectileSpeed);
                }
            }
        };
    }
    */
    private bool isShooting = false;

    public void LightShoot()
    {
        PlayerInput.Player.LightShoot.started += context =>
        {
            // Kezdje el a l�v�st, ha a Hold interaction elindul
            if (context.interaction is HoldInteraction)
            {
                isShooting = true;
                StartCoroutine(ShootContinuously());
            }
            else if (context.interaction is TapInteraction)
            {
                ShootSingle();
            }
        };

        PlayerInput.Player.LightShoot.canceled += context =>
        {
            // �ll�tsa le a l�v�st, ha a Hold interaction befejez�dik
            if (context.interaction is HoldInteraction)
            {
                isShooting = false;
            }
        };
    }

    private void ShootSingle()
    {
        if (Time.time - lastfired > 1 / entity.weapon.fireRate)
        {
            
            ShootProjectile();
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (isShooting)
        {
            if (Time.time - lastfired > 1 / entity.weapon.fireRate)
            {
                lastfired = Time.time;  
                ShootProjectile();
            }
            yield return null; 
        }
    }

    private void ShootProjectile()
    {
        GameObject clone = Instantiate(entity.weapon.projectile,gameObject.GetComponentInChildren<Transform>().Find("LightWeaponTransform").position,transform.rotation);
        clone.GetComponent<Weapon>().entity.weapon = entity.weapon;
        clone.GetComponent<SpriteRenderer>().sprite = entity.weapon.sprite;
        clone.GetComponent<Weapon>().entity = entity;
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.up * entity.weapon.projectileSpeed);
    }

    public void HeavyShoot()
    {
        PlayerInput.Player.HeavyShoot.performed += context =>
        {
            if (context.interaction is TapInteraction)
            {
                if (Time.time - lastfired > 1 /  entity.heavyweapon.fireRate)
                {
                    lastfired = Time.time;
                    GameObject clone = Instantiate(entity.heavyweapon.projectile, gameObject.GetComponentInChildren<Transform>().Find("LightWeaponTransform").position, transform.rotation);
                    clone.GetComponent<Weapon>().entity.weapon = entity.heavyweapon;
                    clone.GetComponent<SpriteRenderer>().sprite = entity.heavyweapon.sprite;
                    Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                    rb.velocity = transform.TransformDirection(Vector3.up * entity.heavyweapon.projectileSpeed);
                }
            }
        };
        PlayerInput.Player.HeavyShoot.performed += context =>
        {
            if (context.interaction is HoldInteraction)
            {
                if (Time.time - lastfired > 1 / entity.heavyweapon.fireRate)
                {
                    lastfired = Time.time;
                    GameObject clone = Instantiate(entity.heavyweapon.projectile, gameObject.GetComponentInChildren<Transform>().Find("LightWeaponTransform").position, transform.rotation);
                    clone.GetComponent<Weapon>().entity.weapon = entity.heavyweapon;
                    clone.GetComponent<SpriteRenderer>().sprite = entity.heavyweapon.sprite;
                    Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                    rb.velocity = transform.TransformDirection(Vector3.up * entity.heavyweapon.projectileSpeed);
                }
            }
        };
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("EXP"))
        {
            Destroy(collider.gameObject);
            exp += Random.Range(0, 10);
        }

    }

    void Update()
    {
        //-----------------------Movement-------------------
        if (GetPlayerInput() != Vector2.zero)
        {
            movement = GetPlayerInput();
            rb.velocity = movement * playerSpeed;
        }
        else rb.velocity = Vector2.zero;
        LightShoot();
        HeavyShoot();
    }



}
