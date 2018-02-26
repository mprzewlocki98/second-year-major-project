using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InjectionSceneTest {

	[Test]
	public void InjectionSceneTestSimplePasses() {
        Assert.AreEqual(1, 1);
	}

	[UnityTest]
	public IEnumerator InjectionSceneTestWithEnumeratorPasses() {
        Assert.AreEqual(1, 1);
		yield return null;
	}
}
