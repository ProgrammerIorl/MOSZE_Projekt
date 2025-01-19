using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{

    public List<EntityScriptableObject> character;
    public int CharacterCount
    {
        get
        {
            return character.Count;
        }
    }
    public EntityScriptableObject GetCharacter(int index) 
    {
        return character[index]; 
    } 
}