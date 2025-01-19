using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject coin;
    public GameObject WeaponGameObject;
    public GameObject Enemy;
    public GameObject SpawnPoint;
    public static GameManager Instance;
    public CharacterDatabase EnemyDatabase;
    public int stageNumber = 0;
    public int roundNumber = 0;
    public int coinNumber = 0;
    public int timesUpgraded = 0;
    public EntityScriptableObject RoundEnemy;
    public bool isPaused = false;
    public bool isRoundEnded = false;
    public GameObject InGameMenu;
    public GameObject StageEndScreen;
    public GameObject CoinCounter;
    public GameObject Boss;
    public Dictionary<int, int> upgrades = new()
    {
        
        { 0, 1},
        { 1, 1},
        { 2, 1}
    };
    public List<GameObject> enemies = new();
    public List<CharacterDatabase> enemyDatabases;


    private void OnEnable()
    {
        EventManager.Pause += PauseResume;
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventManager.StageEnd += StageEnd;
        EventManager.CoinCollected += EventManagerCoinAdd;
        EventManager.CoinCollected += EventManagerCoinCounterChange;

    }

    private void OnDisable()
    {
        EventManager.Pause -= PauseResume;
        EventManager.CoinCollected -= EventManagerCoinCounterChange;
        EventManager.CoinCollected -= EventManagerCoinAdd;
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

    public void EventManagerCoinCounterChange()
    {
        CoinCounter.GetComponent<TextMeshProUGUI>().text = coinNumber.ToString();
    }
    public void EventManagerCoinAdd()
    {
        GameManager.Instance.coinNumber += 1 * roundNumber * stageNumber;

    }
    public void Upgrade(int id)
    {
        int coinsAfterUpgrade = 5 * timesUpgraded * stageNumber;
        upgrades[id] += 1;
        if (coinNumber - coinsAfterUpgrade > 0)
        {
            coinNumber -= coinsAfterUpgrade;
            timesUpgraded++;
            EventManagerCoinCounterChange();
            EventManager.OnUpgrade();
        }


    }
    private void Update()
    {
        if (isRoundEnded && roundNumber <= 3)
        {
            NextRound();
        }
        if (roundNumber == 4 && isRoundEnded)
        {
            EventManager.OnStageEnd();
        }


    }

    public void RemoveEnemyFromList(GameObject gameObject)
    {
        enemies.Remove(gameObject);
        if (enemies.Count == 0 && roundNumber <= 4)
        {
            isRoundEnded = true;
            
        }

    }
    public void AddEnemyToList(GameObject gameObject)
    {
        enemies.Add(gameObject);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name!="MainMenu")
        {
            roundNumber = 0;
            NextRound();
        }
        
    }
    public void MainMenuLoadScene() 
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StageEnd()
    {

        if (stageNumber == 4)
        {
            
            Endgame();
        }
        else
        {
            Wait();
        }


    }

    public void Wait()
    {
        StageEndScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void NextRound()
    {
        roundNumber++;
        SpawnPoint = GameObject.Find("SpawnPoint");
        RoundEnemy = EnemyDatabase.GetCharacter(roundNumber - 1);
        SpawnEnemies();
        isRoundEnded = false;
    }
    public void NextStage()
    {
        StageEndScreen.SetActive(false);
        Time.timeScale = 1.0f;
        stageNumber++;
        roundNumber = 0;
        isRoundEnded = false;
        EnemyDatabase = enemyDatabases[stageNumber - 1];
        SceneManager.LoadScene("Stage" + stageNumber);
        SaveByXML();
    }
    public void StartNewGame()
    {
        stageNumber = 0;
        stageNumber++;
        roundNumber = 0;
        coinNumber = 0;
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i] = 1;
        }
        timesUpgraded = 0;
        EventManagerCoinCounterChange();
        SceneManager.LoadScene("Stage" + stageNumber);
        EnemyDatabase = enemyDatabases[stageNumber - 1];
        SaveByXML();
    }
    public void SpawnEnemies()
    {
        if (roundNumber <= 3)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < roundNumber; j++)
                {
                    Vector2 coords = new(SpawnPoint.transform.position.x + i * 2, SpawnPoint.transform.position.y - j);
                    GameObject enemy = Instantiate(Enemy, coords, transform.rotation);
                    enemy.GetComponent<Enemy>().health = RoundEnemy.health * i * stageNumber;
                    Debug.Log("Instantiated");


                }

            }
        }
        else if (roundNumber == 4)
        {
            Debug.Log("Instantiated");
            Vector2 coords = new(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y - 1.5f);
            Instantiate(Boss, coords, transform.rotation);

        }


    }
    public void PauseResume()
    {
        if (isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            InGameMenu.SetActive(false);

        }
        else if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            InGameMenu.SetActive(true);

        }
    }


    public void LoadByXML()
    {

        string filePath = Application.dataPath + "/Save.text";
        if (File.Exists(filePath))
        {

            XmlDocument xmlDocument = new();
            xmlDocument.Load(filePath);

            XmlNodeList coinNum = xmlDocument.GetElementsByTagName("coinNumberElement");
            int coinNumCount = int.Parse(coinNum[0].InnerText);
            GameManager.Instance.coinNumber = coinNumCount;

            XmlNodeList characterNumber = xmlDocument.GetElementsByTagName("characterNumberElement");
            int characterNum = int.Parse(characterNumber[0].InnerText);
            CharacterManager.Instance.selectedOption = characterNum;

            XmlNodeList stageNumberElement = xmlDocument.GetElementsByTagName("stageNumberElement");
            int stageNumber = int.Parse(stageNumberElement[0].InnerText);
            GameManager.Instance.stageNumber = stageNumber;
            GameManager.Instance.EnemyDatabase = GameManager.Instance.enemyDatabases[stageNumber];

            SceneManager.LoadScene("Stage" + GameManager.Instance.stageNumber);
        }

    }
    public void SaveByXML()
    {

        XmlDocument xmlDocument = new();

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        XmlElement coinNumElement = xmlDocument.CreateElement("coinNumberElement");
        //MARKER Gets or sets the concatenated 串联值 values of the node and all its child nodes.
        coinNumElement.InnerText = GameManager.Instance.coinNumber.ToString();//Return string type
        root.AppendChild(coinNumElement);

        XmlElement stageNumberElement = xmlDocument.CreateElement("stageNumberElement");
        stageNumberElement.InnerText = GameManager.Instance.stageNumber.ToString();
        root.AppendChild(stageNumberElement);

        XmlElement characterNumberElement = xmlDocument.CreateElement("characterNumberElement");
        characterNumberElement.InnerText = CharacterManager.Instance.selectedOption.ToString();
        root.AppendChild(characterNumberElement);

        //OPTIONAL ADVANCED


        xmlDocument.AppendChild(root);
        xmlDocument.Save(Application.dataPath + "/Save.text");

        if (File.Exists(Application.dataPath + "/Save.text"))
        {
            Debug.Log("XML FILED SAVED");
        }
    }
    public void Endgame()
    {
        CoinCounter.SetActive(false);
        EventManager.OnGameEnd();



    }
    public void QuitGame()
    {
        Application.Quit();
    }

}