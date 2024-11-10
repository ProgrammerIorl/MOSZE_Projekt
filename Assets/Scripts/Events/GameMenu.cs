using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class GameMenu : MonoBehaviour
{
    public int CharNum;       // Character number
    public int CharEXP=0;       // Character experience points
    public int levelNumber;   // Level number, assumed to be between 1 and 4

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void SaveByXML()
    {
        XmlDocument xmlDocument = new XmlDocument();

        #region Create XMLDocument Elements
        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "CharacterData");

        XmlElement levelElement = xmlDocument.CreateElement("LevelNumber");
        levelElement.InnerText = levelNumber.ToString();
        root.AppendChild(levelElement);

        XmlElement charNumElement = xmlDocument.CreateElement("CharNum");
        charNumElement.InnerText = CharNum.ToString();
        root.AppendChild(charNumElement);

        XmlElement charExpElement = xmlDocument.CreateElement("CharEXP");
        charExpElement.InnerText = CharEXP.ToString();
        root.AppendChild(charExpElement);
        #endregion

        xmlDocument.AppendChild(root);
        xmlDocument.Save(Application.dataPath + "/CharacterData.xml");

        if (File.Exists(Application.dataPath + "/CharacterData.xml"))
        {
            Debug.Log("XML FILE SAVED");
        }
    }

    private void LoadByXML()
    {
        string filePath = Application.dataPath + "/CharacterData.xml";
        if (File.Exists(filePath))
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList levelNodes = xmlDocument.GetElementsByTagName("LevelNumber");
            levelNumber = int.Parse(levelNodes[0].InnerText);

            XmlNodeList charNumNodes = xmlDocument.GetElementsByTagName("CharNum");
            CharNum = int.Parse(charNumNodes[0].InnerText);

            XmlNodeList charExpNodes = xmlDocument.GetElementsByTagName("CharEXP");
            CharEXP = int.Parse(charExpNodes[0].InnerText);

            Debug.Log("XML FILE LOADED");
        }
        else
        {
            Debug.Log("XML FILE NOT FOUND");
        }
    }

    private void SaveByPlayerPrefs()
    {
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
        PlayerPrefs.SetInt("CharNum", CharNum);
        PlayerPrefs.SetInt("CharEXP", CharEXP);
        Debug.Log("DATA SAVED TO PLAYERPREFS");
    }

    private void LoadByPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("LevelNumber") && PlayerPrefs.HasKey("CharNum") && PlayerPrefs.HasKey("CharEXP"))
        {
            levelNumber = PlayerPrefs.GetInt("LevelNumber");
            CharNum = PlayerPrefs.GetInt("CharNum");
            CharEXP = PlayerPrefs.GetInt("CharEXP");
            Debug.Log("DATA LOADED FROM PLAYERPREFS");
        }
        else
        {
            Debug.Log("DATA NOT FOUND IN PLAYERPREFS");
        }
    }

    // These methods will be triggered by buttons or other UI interactions.
    public void SaveData()
    {
        SaveByXML();
        SaveByPlayerPrefs();
    }

    public void LoadData()
    {
        LoadByXML();
        LoadByPlayerPrefs();
    }
}
