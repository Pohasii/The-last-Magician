using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    public static MenuController menuController;

    public GameObject mainMenu, gameMenu;//Главное меню и игровое меню
    public Animator anim;//Для анимации подтверждения выхода из игры
    public GameObject ElementCreateBar;//Панель покупки рун
    public GameObject Panel;//Невидимая панель при нажатии на которой закрываются все елементы интерфеса
    public GameObject HelpWindow;//Помощь
    public static List<Animator> PopUpMenuAnimList = new List<Animator>();//анимация всплывающего меню

    void Awake()
    {
        PopUpMenuAnimList.Clear();
        menuController = this;
        Cursor.visible = true;
    }

    public void ShowCreateElementBar()
    {
        ElementCreateBar.SetActive(true);
        Panel.SetActive(true);
        Panel.transform.SetAsLastSibling();
        ElementCreateBar.transform.SetAsLastSibling();
    }

    public void CloseAllMenu()
    {
        Panel.SetActive(false);
        foreach (Animator f_anim in PopUpMenuAnimList)
        {
            f_anim.SetBool("Open", false);
        }
        ElementCreateBar.SetActive(false);
    }

    public IEnumerator NextMenu(GameObject CurUI, GameObject NextUI)
    {
        LevelManager.levelManager.BeginFade(1);
        yield return new WaitForSeconds(LevelManager.levelManager.FadeSpeed);
        LevelManager.levelManager.BeginFade(-1);
        CurUI.SetActive(false);
        NextUI.SetActive(true);
    }

    public IEnumerator NextMenu()
    {
        LevelManager.levelManager.BeginFade(1);
        yield return new WaitForSeconds(LevelManager.levelManager.FadeSpeed);
        LevelManager.levelManager.BeginFade(-1);
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
}
