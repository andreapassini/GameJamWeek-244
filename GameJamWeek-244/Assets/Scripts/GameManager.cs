using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager: MonoBehaviour
{
	// I could use an FSM

	public Animator Animator;
	public float TransitionTime = 1f;

	public void PlayStartScene()
	{
		StartCoroutine(C_PlayStartScene());
	}


	#region Coruotines

	private IEnumerator C_PlayStartScene()
    {
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("Start");
	}

	#endregion

}
