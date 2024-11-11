using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public EntityScriptableObject[] character;
    public int CharacterCount
    {
        get
        {
            return character.Length;
        }
    }
    public EntityScriptableObject GetCharacter(int index) 
    {
        return character[index]; 
    } 
}