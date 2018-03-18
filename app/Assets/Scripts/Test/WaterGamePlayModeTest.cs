using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaterGamePlayModeTest {

	private GameObject glass, water1;

	[SetUp]
	public void Init(){

		SceneManager.LoadScene("15-water-game", LoadSceneMode.Single);
	}

    [UnityTest]
    public IEnumerator TestTemplate() // a template for unit tests in play mode
    {

        yield return null;

       // Assert.AreEqual(1, 1);
    }

	[UnityTest]
	public IEnumerator test() {
			
		glass = GameObject.Find ("glass");

		water1 = GameObject.Find ("water1");

		Assert.IsNull(water1);

		yield return null;
	}

}
