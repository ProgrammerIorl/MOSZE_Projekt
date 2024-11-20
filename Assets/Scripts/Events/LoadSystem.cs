using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.IO;

public class LoadSystem : MonoBehaviour
{

    public void LoadByXML()
    {
        Debug.Log("Load");
        string filePath = Application.dataPath + "/Data.text";
        if (File.Exists(filePath))
        {
            Save save = new ();

            XmlDocument xmlDocument = new ();
            xmlDocument.Load(filePath);

            XmlNodeList stageNumberElement = xmlDocument.GetElementsByTagName("stageNumber");
            GameManager.Instance.stageNumber = int.Parse(stageNumberElement[0].InnerText);
            XmlNodeList coinNumElement = xmlDocument.GetElementsByTagName("coinNumber");
            GameManager.Instance.coinNumber = int.Parse(coinNumElement[0].InnerText);

        }

    }
}
