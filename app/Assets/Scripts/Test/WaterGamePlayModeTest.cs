﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaterGamePlayModeTest {

    [SetUp]
    public void Init() {
        SceneManager.LoadScene("15-water-game", LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator TestTemplate() // a template for unit tests in play mode
    {
        SceneManager.LoadScene("5-metal-game", LoadSceneMode.Single);

        yield return null;

        Assert.AreEqual(1, 1);
    }

}
