using UnityEngine;
using UnityEngine.UI;
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
                case "Continue": CharacterUIController.charUIController.CloseInGameMenu(); break;
                case "Settings": CharacterUIController.charUIController.HelpWindow.gameObject.SetActive(true); break;
                case "Exit": CharactersDB.characterDB.Save();Time.timeScale = 1; StartCoroutine(LevelManager.levelManager.LoadLevelWithFade(LevelManager.levelManager.LoadLevelFromGame, "Menu")); break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonEventFunc("PointerDown");
    }
}
