using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    Transform myTransform;
    Animator anim;

    void Start()
    {
        myTransform = GetComponent<Transform>();

        if (tag == "PopUpMenu")
        {
            anim = GetComponent<Animator>();
            MenuController.PopUpMenuAnimList.Add(anim);
        }
    }

    void PopUpMenuControler()
    {
        if (tag != "PopUpMenu")
            return;
        anim.SetBool("Open", true);
        MenuController.menuController.Panel.SetActive(true);
        myTransform.SetAsLastSibling();
        MenuController.menuController.Panel.transform.SetAsLastSibling();
    }

    void ButtonEventFunc(string EventType)
    {
        if (EventType == "PointerDown")
        {
            PopUpMenuControler();

            switch (name)
            {
                case "CreateElement": MenuController.menuController.ShowCreateElementBar(); break;
                case "Accept": Menu.SMenu.ElementCheck(); break;
                case "Back": StartCoroutine(MenuController.menuController.NextMenu(MenuController.menuController.gameMenu, MenuController.menuController.mainMenu)); break;
                case "Start": Menu.SMenu.LoadLevel(); StartCoroutine(LevelManager.levelManager.LoadLevelWithFade(LevelManager.levelManager.LoadLevelAsync, "Game")); break;
                case "Panel": MenuController.menuController.CloseAllMenu(); break;
            }
        }
        if (EventType == "PointerEnter")
        {
            switch (name)
            {
                case "CreateElement": break;
                case "Accept": break;
                case "Start": break;
            }
        }
        if (EventType == "PointerExit")
        {
            switch (name)
            {
                case "CreateElement": break;
                case "Accept": break;
                case "Start": break;
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonEventFunc("PointerDown");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ButtonEventFunc("PointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ButtonEventFunc("PointerExit");
    }
}
