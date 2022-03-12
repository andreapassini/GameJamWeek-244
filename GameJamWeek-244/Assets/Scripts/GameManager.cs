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

	public void PlayMainScene()
	{
		StartCoroutine(C_PlayMainScene());
	}

	public void PlayLvlStringente()
    {
		StartCoroutine(C_PlayLvlStringente());
	}

	public void PlayLvlFinale()
    {
		StartCoroutine(C_PlayLvlFinale());
	}

	public void PlayLvlMainMenu()
    {
		StartCoroutine(C_PlayLvlMainMenu());
	}

	public void PlayLvlDistorted()
    {
		StartCoroutine(C_PlayLvlMainMenu());
	}

	public void PlayLvlMain()
	{
		StartCoroutine(C_PlayLvlMain());
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

	private IEnumerator C_PlayMainScene()
	{
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("Main");
	}

	private IEnumerator C_PlayLvlStringente()
	{
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("LivelloStringente");
	}

	private IEnumerator C_PlayLvlFinale()
	{
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("Final");
	}

	private IEnumerator C_PlayLvlMainMenu()
	{
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("MainMenu");
	}

	private IEnumerator C_PlayLvlDistorted()
	{
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("Distorted");
	}

	private IEnumerator C_PlayLvlMain()
	{
		// Play animation
		Animator.SetTrigger("transition");

		// Wait for the end of the animation
		yield return new WaitForSeconds(TransitionTime);

		// Load new Scene
		SceneManager.LoadScene("Main");
	}

	#endregion

}
