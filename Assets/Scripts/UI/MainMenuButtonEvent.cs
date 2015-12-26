using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenuButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    void Exit(bool Exit)
    {
       MenuController.menuController.anim.SetBool("Exit", Exit);
    }

    void ButtonEventFunc(string EventType)
    {
        if (EventType == "PointerDown")
        {
            switch (name)
            {
                case "Start": StartCoroutine(MenuController.menuController.NextMenu(MenuController.menuController.mainMenu, MenuController.menuController.gameMenu)); break;
                case "Yes": Application.Quit(); break;
                case "No": Exit(false); break;
                case "Exit": Exit(true); break;
            }
        }

        if (EventType == "PointerEnter")
        {

        }

        if (EventType == "PointerExit")
        {

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonEventFunc("PointerDown");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonEventFunc("PointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonEventFunc("PointerExit");
    }
}
