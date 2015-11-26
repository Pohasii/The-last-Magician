using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CreateSpell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    Menu menu;
    SpellSDataBase SpellDB;
    [SerializeField]
    GameObject SpellObj;

    void Start()
    {
        menu = GetComponentInParent<Menu>();
        SpellDB = GetComponentInParent<SpellSDataBase>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        menu.ElementCheck(SpellDB.Spells, SpellObj);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
