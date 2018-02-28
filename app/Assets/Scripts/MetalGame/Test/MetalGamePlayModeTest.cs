using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class MetalGamePlayModeTest {

    [UnityTest]
    public void TestTemplate() // a template for unit tests in play mode
    {
        SceneManager.LoadScene("5-metal-game", LoadSceneMode.Single);

        Assert.Pass();
    }

}
