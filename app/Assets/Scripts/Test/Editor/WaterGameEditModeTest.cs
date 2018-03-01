using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class WaterGameEditModeTest {

    [Test]
    public void TestTemplate() { // a template for tests in edit mode
        EditorSceneManager.OpenScene("Assets/Scenes/5-metal-game.unity", OpenSceneMode.Single);

        Assert.Pass();
    }

}
