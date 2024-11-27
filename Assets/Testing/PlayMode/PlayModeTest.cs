using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.InputSystem.Interactions;
using UnityEditor;

public class PlayModeTest : InputTestFixture
{
    [UnityTest]
    public IEnumerator _0PlayModeMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        yield return null;
        var NewGame = GameObject.Find("NewGame");
        Assert.IsNotNull(NewGame);
        NewGame.GetComponent<Button>().onClick.Invoke();
        var Play = GameObject.Find("Play");
        Assert.IsNotNull(Play);
        Play.GetComponent<Button>().onClick.Invoke();
        

    }
    [UnityTest]
    public IEnumerator _1PlayModePlayerShootTest()
    {
        var mouse = InputSystem.AddDevice<Mouse>();
        var player = GameObject.Find("Player");
        Assert.IsNotNull(player);
        var playerScriptableObject = player.GetComponent<InputManager>().entity;
        Assert.IsNotNull(playerScriptableObject);
        
        player.GetComponent<InputManager>().isShooting = true;
        yield return null;
        Press(mouse.leftButton);
        yield return null;
        Assert.IsTrue(mouse.leftButton.isPressed);
        Release(mouse.leftButton);
        yield return null;
        Assert.IsFalse(mouse.leftButton.isPressed);
        yield return null;
        var projectile = GameObject.FindObjectsOfType<Weapon>();
        Assert.IsNotNull(projectile);

        bool isTrue = false;
        for (int i = 0; i < projectile.Length; i++)
        {
            Debug.Log(projectile[i]);
            Debug.Log(projectile[i].entity);
            if (projectile[i].entity == playerScriptableObject)
            {
                isTrue = true;
            }

        }
        Assert.IsTrue(isTrue);
        
    }
    [UnityTest]
    public IEnumerator _2PlayModeEnemyShootTest()
    {
        var enemy = GameObject.FindObjectOfType<Enemy>();

        Assert.IsNotNull(enemy);
        var enemyScriptableObject = enemy.GetComponent<Enemy>().entity;
        Assert.IsNotNull(enemyScriptableObject);
        enemy.Fire();
        yield return null;
        var projectile = GameObject.FindObjectsOfType<Weapon>();
        Assert.IsNotNull(projectile);
        bool isTrue = false;
        for (int i = 0; i < projectile.Length; i++)
        {
            if (projectile[i].entity == enemyScriptableObject)
            {
                isTrue = true;
            }

        }
        Assert.IsTrue(isTrue);


    }

}
