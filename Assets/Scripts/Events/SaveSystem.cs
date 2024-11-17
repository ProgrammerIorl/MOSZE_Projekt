using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
class Save
{
    public int coinsNum;
    public int diamondsNum;

}
public class SaveSystem : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Save += EventManagerOnSave;
    }
    private void OnDisable()
    {
        EventManager.Save -= EventManagerOnSave;
    }

    private void EventManagerOnSave()
    {
        SaveByXML();
    }



    private Save CreateSaveGameObject()
    {
        Save save = new ();
        return save;
    }

    private void SaveByXML()
    {
        Save save = CreateSaveGameObject();
        XmlDocument xmlDocument = new ();

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        XmlElement coinNumElement = xmlDocument.CreateElement("CoinNum");
        //MARKER Gets or sets the concatenated 串联值 values of the node and all its child nodes.
        coinNumElement.InnerText = save.coinsNum.ToString();//Return string type
        root.AppendChild(coinNumElement);

        XmlElement diamondNumElement = xmlDocument.CreateElement("DiamondNum");
        diamondNumElement.InnerText = save.diamondsNum.ToString();
        root.AppendChild(diamondNumElement);

        //OPTIONAL ADVANCED


        xmlDocument.AppendChild(root);
        xmlDocument.Save(Application.dataPath + "/Data.text");

        if (File.Exists(Application.dataPath + "/Data.text"))
        {
            Debug.Log("XML FILED SAVED");
        }
    }
  
}
