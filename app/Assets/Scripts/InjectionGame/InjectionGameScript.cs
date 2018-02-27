using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InjectionGameScript : MonoBehaviour {

	public enum State {
		OPEN_CREAM, APPLY_CREAM, MOVE_SYRINGE, INJECT_SYRINGE, DONE
	};

	private State currentState = State.OPEN_CREAM;
    public bool easyMode = false;

    private Animation wellDoneAnimation;

    // execute at start of game
	void Start () {
        wellDoneAnimation = GameObject.Find("wellDone").GetComponent<Animation>();
    }

    // delay setting of state
    private IEnumerator SetState(State targetState, float time = 0.25f) {
        yield return new WaitForSeconds(time);
        currentState = targetState;
    }

    public State GetState() {
        return currentState;
    }

    // attempt to change the state and return if it was a success
    public bool ChangeState(State targetState) {
        // can only to this state if it directly precedes the current one
        if((int)currentState == (int)targetState - 1) {
            StartCoroutine(SetState(targetState));
            if(targetState == State.DONE) {
                StartCoroutine(Done());
            }
            return true;
        }

        return false;
    }

    // executed when the game is done
	private IEnumerator Done() {
        yield return new WaitForSeconds(1);
        wellDoneAnimation.Play();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("8-cutscene");
	}
}
