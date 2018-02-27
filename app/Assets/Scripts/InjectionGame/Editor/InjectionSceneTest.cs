using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class InjectionSceneTest {

	[Test]
	public void InjectionSceneTestSimplePasses() {
        Assert.AreEqual(1, 1);
	}

    [Test]
    public void DoesNotGoToDoneStateImmediately() { // test to see that it doesn't just go to the finished state already
        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.DONE);

        Assert.IsFalse(success);
    }

    [UnityTest]
	public IEnumerator InjectionSceneTestWithEnumeratorPasses() {
        Assert.AreEqual(1, 1);
		yield return null;
	}
}
