using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
class Save
{
    public int stageNumber;
    public int coinNumber;

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

    public void SaveByXML()
    {
        Save save = CreateSaveGameObject();
        XmlDocument xmlDocument = new ();

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        XmlElement coinNumElement = xmlDocument.CreateElement("coinNumber");
        //MARKER Gets or sets the concatenated 串联值 values of the node and all its child nodes.
        coinNumElement.InnerText = GameManager.Instance.stageNumber.ToString();//Return string type
        root.AppendChild(coinNumElement);

        XmlElement stageNumberElement = xmlDocument.CreateElement("stageNumber");
        stageNumberElement.InnerText = GameManager.Instance.stageNumber.ToString();
        root.AppendChild(stageNumberElement);

        //OPTIONAL ADVANCED


        xmlDocument.AppendChild(root);
        xmlDocument.Save(Application.dataPath + "/Data.text");

        if (File.Exists(Application.dataPath + "/Data.text"))
        {
            Debug.Log("XML FILED SAVED");
        }
    }
  
}
