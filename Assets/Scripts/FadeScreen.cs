using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken from https://www.youtube.com/watch?v=JCyJ26cIM0Y&t=1122s

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private bool fadeOnStart = true;
    public float fadeDuration = 2;
    [SerializeField] private Color fadeColor;
    private Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    /// <summary>
    /// Loads the scene with the same name as the sceneName paramater.
    /// <a herf = "https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html"></a>
    /// </summary>
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=CE9VOZivb3I
    /// Creates a fade transition and loads the next scene.
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.color = newColor2;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
