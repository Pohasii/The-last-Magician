using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GameButtonEvent : MonoBehaviour, IPointerDownHandler
{
    void ButtonEventFunc(string EventType)
    {
        if (EventType == "PointerDown")
        {
            switch (name)
            {
                case "Continue": ; break;
                case "Settings": break;
                case "Exit": StartCoroutine(LevelManager.levelManager.LoadLevelWithFade(LevelManager.levelManager.Loadlevel, "Menu")); break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonEventFunc("PointerDown");
    }
}
