using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CreateSpell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    Menu menu;
    [SerializeField]
    GameObject SpellObj;

    void Start()
    {
        menu = GetComponentInParent<Menu>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        menu.ElementCheck();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
