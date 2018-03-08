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

    /*[UnityTest] // Test for player to go next scene once game has finished
    public IEnumerator AllWaterSpritesAreNotActiveAtTheBeginningOfTheGame() {

        GameObject water1 = GameObject.Find("water1");
        GameObject water2 = GameObject.Find("water2");
        GameObject water3 = GameObject.Find("water3");
        GameObject water4 = GameObject.Find("water4");
        GameObject water5 = GameObject.Find("water5");
        GameObject water6 = GameObject.Find("water6");

        Assert.IsFalse(water1.activeInHierarchy);
        Assert.IsFalse(water2.activeInHierarchy);
        Assert.IsFalse(water3.activeInHierarchy);
        Assert.IsFalse(water4.activeInHierarchy);
        Assert.IsFalse(water5.activeInHierarchy);
        Assert.IsFalse(water6.activeInHierarchy);

        yield return null;
    }*/

    [UnityTest]  //Checks to see whether the glass is draggable at the beginning of the game
    public IEnumerator GlassIsDraggableAtStartOfGame() {

        GameObject glass = GameObject.Find("glass");
        GlassDrag gd = glass.GetComponent<GlassDrag>();

        Assert.IsTrue(gd.ReturnCanDrag());
        yield return null;
    }
}
