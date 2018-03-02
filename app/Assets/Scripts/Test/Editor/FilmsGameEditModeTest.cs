using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class FilmsGameEditModeTest {

    [Test]
    public void TestTemplate() { // a template for tests in edit mode
        EditorSceneManager.OpenScene("Assets/Scenes/11-films-game.unity", OpenSceneMode.Single);

        Assert.Pass();
    }

    [Test]
    public void AllComponentsPresent()
    { // a template for tests in edit mode
        EditorSceneManager.OpenScene("Assets/Scenes/11-films-game.unity", OpenSceneMode.Single);

        GameObject b1, b2, b3, b4, b5, b6, b7, b8, script, panel, scrollPanel;

        b1 = GameObject.Find("Button");
        b2 = GameObject.Find("Button (1)");
        b3 = GameObject.Find("Button (2)");
        b4 = GameObject.Find("Button (3)");
        b5 = GameObject.Find("Button (4)");
        b6 = GameObject.Find("Button (5)");
        b7 = GameObject.Find("Button (6)");
        b8 = GameObject.Find("Button (7)");
        script = GameObject.Find("Script");
        panel = GameObject.Find("Panel");
        scrollPanel = GameObject.Find("scrollPanel");

        Assert.IsNotNull(b1);
        Assert.IsNotNull(b2);
        Assert.IsNotNull(b3);
        Assert.IsNotNull(b4);
        Assert.IsNotNull(b5);
        Assert.IsNotNull(b6);
        Assert.IsNotNull(b7);
        Assert.IsNotNull(b8);
        Assert.IsNotNull(script);
        Assert.IsNotNull(panel);
        Assert.IsNotNull(scrollPanel);
    }

}
