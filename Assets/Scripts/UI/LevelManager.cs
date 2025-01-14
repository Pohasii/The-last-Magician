﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    [HideInInspector]
    public float FadeSpeed = 1f;//скорость затухания
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
    }

    void Start()
    {
        loadingImage.enabled = true;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartScene"))
            StartCoroutine(LoadLevelWithFade(LoadLevelAsync, "Menu", 2, 0));
    }

    void Update()
    {
        alpha += fadeDirection * FadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        loadingImage.color = new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, alpha);

        if (async != null && Input.anyKeyDown)
            async.allowSceneActivation = true;
    }

    public void BeginFade(int fadeDir)
    {
        fadeDirection = fadeDir;
    }

    AsyncOperation async;

    public void Loadlevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelFromGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
        StartCoroutine(LoadMenuFromGame());
    }

    IEnumerator LoadMenuFromGame()
    {
        yield return new WaitForSeconds(0);
        MenuController.menuController.mainMenu.SetActive(false);
        MenuController.menuController.gameMenu.SetActive(true);
    }

    public void LoadLevelAsync(string levelName, float loadingDuration)
    {
        async = SceneManager.LoadSceneAsync(levelName);
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
        yield return new WaitForSeconds(FadeSpeed);
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
