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
    private readonly float playerSpeed = 7.0f;
    float lastfired;
    public bool isShooting = false;
    float heavylastfired;
    
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
        entity = CharacterManager.Instance.characterDatabase.GetCharacter(CharacterManager.Instance.selectedOption);
        health = entity.health;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraTransform = Camera.main.transform;
        lastfired = 0;
        GetComponent<SpriteRenderer>().sprite=entity.sprite;
        
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.TryGetComponent<Entity>(out var coin)) {
            if (coin.entity.entityType == EntityScriptableObject.EntityType.Droppable)
            {
                Debug.Log("Collision Coin");
                EventManager.OnCoinCollected();
                Destroy(collider.gameObject);
            }

        }
    }

    void Update()
    {
        if (Pause())
        {
            EventManager.OnPause();
        }
        if (GameManager.Instance.isPaused==false)
        {
            LightShoot();
            HeavyShoot();
        }
        if (health<=0)
        {
            EventManager.OnDeath();
        }


    }
    private void FixedUpdate()
    {
        if (GetPlayerInput() != Vector2.zero)
        {
            movement = GetPlayerInput();
            rb.velocity = movement * playerSpeed;
        }
        else rb.velocity = Vector2.zero;
    }
    private void OnEnable()
    {
        PlayerInput.Enable();
        EventManager.Death += EventDeath;
    }
    private void OnDisable()
    {
        PlayerInput.Disable();
        EventManager.Death -= EventDeath;
    }
    public void EventDeath() 
    {
        Time.timeScale = 0;
    }
    public Vector2 GetPlayerInput()
    {
        return PlayerInput.Player.Movement.ReadValue<Vector2>();
    }
    public bool Pause() 
    {
        return PlayerInput.Player.Pause.triggered;
    }
   
    

    public void LightShoot()
    {
        
            PlayerInput.Player.LightShoot.started += context =>
        {
            
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
            if (context.interaction is HoldInteraction)
            {
                isShooting = false;
            }
        };
        
    }

    private void ShootSingle()
    {
        if (Time.time - lastfired > 10 / entity.weapon.fireRate/ GameManager.Instance.upgrades[1])
        {
            ShootProjectile();
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (isShooting)
        {
            if (Time.time - lastfired > 10 / entity.weapon.fireRate / GameManager.Instance.upgrades[1])
            {
                lastfired = Time.time;  
                ShootProjectile();
            }
            yield return null; 
        }
    }

    private void ShootProjectile()
    {
        GameObject clone = Instantiate(GameManager.Instance.WeaponGameObject, gameObject.GetComponentInChildren<Transform>().Find("LightWeaponTransform").position,transform.rotation);
        clone.GetComponent<Weapon>().entity = entity;
        clone.GetComponent<SpriteRenderer>().sprite = entity.weapon.sprite;
        clone.GetComponent<Weapon>().damage = entity.weapon.damage*GameManager.Instance.upgrades[0];
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector3.up * entity.weapon.projectileSpeed * GameManager.Instance.upgrades[2]);
    }

    public void HeavyShoot()
    {
        
            PlayerInput.Player.HeavyShoot.performed += context =>
        {

            if (context.interaction is TapInteraction)
            {

                if (Time.time - heavylastfired > 10 / entity.heavyweapon.fireRate / GameManager.Instance.upgrades[1])
                {
                    heavylastfired = Time.time;
                    GameObject clone = Instantiate(GameManager.Instance.WeaponGameObject, gameObject.GetComponentInChildren<Transform>().Find("HeavyWeaponTransform").position, transform.rotation);
                    Weapon weaponGet = clone.GetComponent<Weapon>();
                    weaponGet.entity = entity;
                    weaponGet.entity.heavyweapon = entity.heavyweapon;
                    weaponGet.damage = entity.heavyweapon.damage * GameManager.Instance.upgrades[0];
                    clone.GetComponent<SpriteRenderer>().sprite = entity.heavyweapon.sprite;
                    Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                    rb.velocity = transform.TransformDirection(entity.heavyweapon.projectileSpeed * GameManager.Instance.upgrades[2] * Vector3.up);
                }
            }
        };
        
    }
    



}
