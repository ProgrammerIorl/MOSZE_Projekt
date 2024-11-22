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
    public int selectedOption = 0;
    public static CharacterManager Instance;
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
    
    void Start()
    {
        UpdateCharacter();
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption > characterDatabase.CharacterCount-1) 
        {
            selectedOption = 0; 
        }
        UpdateCharacter();
    }
    public void PreviousOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = characterDatabase.CharacterCount;
        }
        UpdateCharacter();
    }
    private void UpdateCharacter()
    {

        EntityScriptableObject character = characterDatabase.GetCharacter(selectedOption);
        nameText.text = character.characterName;
        image.sprite= character.sprite;
        

    }

}

