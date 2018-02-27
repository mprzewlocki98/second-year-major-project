using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewPlayModeTest {

    [UnityTest]
    public IEnumerator ChronologicalStateTest() // to check the states go in chronological order
    {
        SceneManager.LoadScene("7-injection", LoadSceneMode.Single);

        yield return null;

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.APPLY_CREAM);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.MOVE_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.INJECT_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.DONE);

        yield return new WaitForSeconds(0.25f);

        Assert.IsTrue(success);
    }

    [UnityTest]
    public IEnumerator WrongChronologicalStateTest() // to check the state order does not go an incorrect way
    {
        SceneManager.LoadScene("7-injection", LoadSceneMode.Single);

        yield return null;

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.APPLY_CREAM);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.INJECT_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.MOVE_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.DONE);

        yield return new WaitForSeconds(0.25f);

        Assert.IsFalse(success);
    }
}
