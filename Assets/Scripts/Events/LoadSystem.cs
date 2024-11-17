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

            XmlNodeList coinNum = xmlDocument.GetElementsByTagName("CoinNum");
            int coinNumCount = int.Parse(coinNum[0].InnerText);
            save.coinsNum = coinNumCount;

        }

    }
}
