using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
    public void QuitGame()
    {
        Application.Quit();
    }
}
