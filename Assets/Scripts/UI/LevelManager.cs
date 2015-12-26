using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    [HideInInspector]
    public float FadeSpeed;//скорость затухания
    float alpha = 1f;//значение непрозрачности тестуры затухания
    float fadeDirection = -1;//направление затухания

    public Image loadingImage;
    public Text loadingText;

    public delegate void LoadLevel(string levelName);
    public delegate void LoadLevelWP(string levelName, float LoadingDuration);

    void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            if (levelManager != this)
            {
                Destroy(gameObject);
            }
        FadeSpeed = 1f;
    }

    void Start()
    {
        loadingImage.enabled = true;
        if (Application.loadedLevelName == "StartScene")
            StartCoroutine(LoadLevelWithFade(LoadLevelAsync, "Menu", 2, 0));
    }

    void Update()
    {
        alpha += fadeDirection * FadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        loadingImage.color = new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, alpha);

        if (Input.anyKeyDown)
            async.allowSceneActivation = true;
    }

    public void BeginFade(int fadeDir)
    {
        fadeDirection = fadeDir;
    }

    AsyncOperation async;

    public void Loadlevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void LoadLevelAsync(string levelName, float loadingDuration)
    {
        async = Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        StartCoroutine(LoadingTextAnim(loadingDuration));
    }

    public IEnumerator LoadingTextAnim(float loadingDuration)
    {
        loadingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(loadingDuration);
        async.allowSceneActivation = true;
    }

    public IEnumerator LoadLevelWithFade(LoadLevelWP level, string levelName, float loadingDuration, float FadeDuration)
    {
        BeginFade(1);
        yield return new WaitForSeconds(FadeDuration);
        level(levelName, 4.65f);
    }

    public IEnumerator LoadLevelWithFade(LoadLevelWP level, string levelName)
    {
        BeginFade(1);
        yield return new WaitForSeconds(0.5f);
        level(levelName, 2.3f);
    }

    public IEnumerator LoadLevelWithFade(LoadLevel level, string levelName)
    {
        BeginFade(1);
        yield return new WaitForSeconds(FadeSpeed);
        level(levelName);
    }

    void OnLevelWasLoaded()
    {
        BeginFade(-1);
        loadingText.gameObject.SetActive(false);
    }
}
