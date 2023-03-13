using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Pause Settings")]
    [Tooltip("Shows whether the game is paused or not. Necessary for if the user presses a button" +
    "to open the pause menu.")]
    public static bool GameIsPaused = false;

    [Header("Pause Menu Pages")]
    [Tooltip("The pause menu panel")]
    [SerializeField] private GameObject pauseMenuUI;
    [Tooltip("The main page of the pause menu")]
    [SerializeField] private GameObject mainPage;
    [Tooltip("The first instructions page of the pause menu")]
    [SerializeField] private GameObject insructionsPage1;
    [Tooltip("The second instructions page of the pause menu")]
    [SerializeField] private GameObject insructionsPage2;
    [Tooltip("The credits page of the pause menu")]
    [SerializeField] private GameObject creditsPage;

    /// <summary>
    /// Unfreezes the game, and closes the pause screen
    /// </summary>
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            mainPage.SetActive(false);
            insructionsPage1.SetActive(false);
            insructionsPage2.SetActive(false);
            pauseMenuUI.SetActive(false);
            creditsPage.SetActive(false);
        }
        GameIsPaused = false;
    }

    /// <summary>
    /// Freezes the game, and opens the pause screen
    /// </summary>
    public void Pause()
    {
        if (pauseMenuUI != null)
        {
            insructionsPage1.SetActive(false);
            insructionsPage2.SetActive(false);
            creditsPage.SetActive(false);

            mainPage.SetActive(true);
            pauseMenuUI.SetActive(true);
        }
        GameIsPaused = true;
    }

    public void OpenPauseMenu()
    {
        if (!GameIsPaused)
        {
            Pause();
        }

        else
        {
            Resume();
        }
    }

    /// <summary>
    /// Turns the level select screen page off and opens the main page
    /// </summary>
    public void OpenMenuPage()
    {
        mainPage.SetActive(true);
        insructionsPage1.SetActive(false);
        insructionsPage2.SetActive(false);
        creditsPage.SetActive(false);
    }

    /// <summary>
    /// Turns the main page off and opens the controls screen page
    /// </summary>
    public void OpenInstructionsPage1()
    {
        mainPage.SetActive(false);
        insructionsPage1.SetActive(true);
        insructionsPage2.SetActive(false);
        creditsPage.SetActive(false);
    }

    /// <summary>
    /// Turns the main page off and opens the controls screen page
    /// </summary>
    public void OpenInstructionsPage2()
    {
        mainPage.SetActive(false);
        insructionsPage1.SetActive(false);
        insructionsPage2.SetActive(true);
        creditsPage.SetActive(false);
    }

    /// <summary>
    /// Turns the main page off and opens the controls screen page
    /// </summary>
    public void OpenCreditsPage()
    {
        mainPage.SetActive(false);
        insructionsPage1.SetActive(false);
        insructionsPage2.SetActive(false);
        creditsPage.SetActive(true);
    }

    /// <summary>
    /// Quits the game regardless if it's a build or in the engine editor.
    /// < a href = "https://docs.unity3d.com/ScriptReference/EditorApplication-isPlaying.html"></a>
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}