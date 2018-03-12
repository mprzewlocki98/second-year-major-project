using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaterGamePlayModeTest {

    [SetUp]
    public void Init() {
        SceneManager.LoadScene("15-water-game", LoadSceneMode.Single);

    }

    [UnityTest] // Test for loading the scene successfully 
    public IEnumerator TestLoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual("15-water-game", currentScene.name);
        yield return null;
    }

    [UnityTest]  //Checks to see whether the glass is draggable at the beginning of the game
    public IEnumerator GlassIsDraggableAtStartOfGame() {

        GameObject glass = GameObject.Find("glass");
        GlassDrag gd = glass.GetComponent<GlassDrag>();

        Assert.IsTrue(gd.ReturnCanDrag());
        yield return null;
    }
}
