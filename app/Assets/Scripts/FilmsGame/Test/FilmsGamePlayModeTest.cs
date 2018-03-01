using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class FilmsGamePlayModeTest {

    [UnityTest]
    public IEnumerator TestTemplate() // a template for unit tests in play mode
    {
        SceneManager.LoadScene("11-films-game", LoadSceneMode.Single);

        yield return null;

        Assert.AreEqual(1, 1);
    }

}
