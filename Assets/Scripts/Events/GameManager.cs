using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject coin;
    public GameObject WeaponGameObject;
    public static GameManager Instance;
    public CharacterDatabase EnemyDatabase;
    public int stageNumber=0;
    public int roundNumber=0;
    public int coinNumber = 0;
    public EntityScriptableObject RoundEnemy;
    public bool isPaused = false;
    public GameObject InGameMenu;

    public List<GameObject> enemies = new ();
    public List<CharacterDatabase> enemyDatabases;
   

    private void OnEnable()
    {
        EventManager.Pause += PauseResume;
    }
    private void OnDisable()
    {
        EventManager.Pause -= PauseResume;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RemoveEnemyFromList(GameObject gameObject) 
    {
        enemies.Remove(gameObject);
    }
    public void AddEnemyToList(GameObject gameObject)
    {
        enemies.Add(gameObject);
    }
    public void NextRound()
    {
        roundNumber++;
        RoundEnemy =EnemyDatabase.GetCharacter(roundNumber);
        
    }
    
    public void PauseResume()
    {
        if (isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            InGameMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            InGameMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void NextStage()
    {
        stageNumber++;
        SceneManager.LoadScene("Stage" + stageNumber);
        EnemyDatabase = enemyDatabases[stageNumber];
        NextRound();
    }

}
