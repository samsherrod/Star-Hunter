using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private FadeScreen fadeScreen;

    private void Awake()
    {
        fadeScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// Loads the scene with the same name as the sceneName paramater.
    /// <a herf = "https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html"></a>
    /// </summary>
    public void LoadNextScene()
    {
        StartCoroutine(GoToSceneAsyncRoutine(SceneManager.GetActiveScene().buildIndex + 1, fadeScreen.fadeDuration));
    }

    /// <summary>
    /// Loads the first scene in the build
    /// </summary>
    public void LoadMainMenu()
    {
        StartCoroutine(GoToSceneAsyncRoutine(0, fadeScreen.fadeDuration));
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=CE9VOZivb3I
    /// Creates a fade transition and loads the next scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator GoToSceneAsyncRoutine(int sceneNumber, float transitionTime)
    {
        fadeScreen.FadeOut();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNumber);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
