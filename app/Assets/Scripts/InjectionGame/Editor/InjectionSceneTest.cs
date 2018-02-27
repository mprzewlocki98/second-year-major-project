using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class InjectionSceneTest : MonoBehaviour {

    [Test] // to see that it does not go to DONE straight away
    public void DoesNotGoToDoneStateImmediately() {
        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.DONE);

        Assert.IsFalse(success);
    }

    [Test] // to check that it does go from OPEN_CREAM to APPLY_CREAM
    public void GoesToCorrectStateFromStart() {
        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.APPLY_CREAM);

        Assert.IsTrue(success);
    }

    [Test]
    public void AllObjectsPresent() { // to check that all the objects in the scene are there
        GameObject child, vein, cream, creamLid, syringe1, syringe2, creamBlob, wellDone, gameHandler;

        child = GameObject.Find("Child");
        vein = GameObject.Find("Vein");
        cream = GameObject.Find("Cream");
        creamLid = GameObject.Find("CreamLid");
        syringe1 = GameObject.Find("Syringe1");
        syringe2 = GameObject.Find("Syringe2");
        creamBlob = GameObject.Find("CreamBlob");
        wellDone = GameObject.Find("wellDone");
        gameHandler = GameObject.Find("GameHandler");

        Assert.IsNotNull(child);
        Assert.IsNotNull(vein);
        Assert.IsNotNull(cream);
        Assert.IsNotNull(creamLid);
        Assert.IsNotNull(syringe1);
        Assert.IsNotNull(syringe2);
        Assert.IsNotNull(creamBlob);
        Assert.IsNotNull(wellDone);
        Assert.IsNotNull(gameHandler);
    }

}
