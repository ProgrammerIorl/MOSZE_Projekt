using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    public TextMeshProUGUI nameText;
    public Image image;
    private int selectedOption = 0;
    void Start()
    {
        Debug.Log(characterDatabase);
        UpdateCharacter();
    }

    public void NextOption()
    {
        Debug.Log("NextOption: ");
        Debug.Log(selectedOption);
        selectedOption++;
        Debug.Log(selectedOption);
        if (selectedOption >= characterDatabase.CharacterCount) 
        {
            Debug.Log(selectedOption);
            selectedOption = 0; 
        }
        UpdateCharacter();


    }
    public void PreviousOption()
    {
        Debug.Log("PreviousOption: ");
        Debug.Log(selectedOption);
        selectedOption--;
        Debug.Log(selectedOption);
        if (selectedOption < 0)
        {
            Debug.Log(selectedOption);
            selectedOption = characterDatabase.CharacterCount;
        }
        UpdateCharacter();
    }
    private void UpdateCharacter()
    {

        EntityScriptableObject character = characterDatabase.GetCharacter(selectedOption);
        Debug.Log(character);
        nameText.text = character.characterName;
        Debug.Log(character.sprite);
        image.sprite= character.sprite;

    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

