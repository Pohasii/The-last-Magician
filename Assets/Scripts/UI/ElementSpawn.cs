using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ElementSpawn : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    Menu menu;
    Element element;
    public GameObject ElementObj;
    [SerializeField]
    private int CurElement;

    void Start()
    {
        menu = GetComponentInParent<Menu>();

        element = SpellSDataBase.ElementsInDB[CurElement];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        menu.ElementSpawn(ElementObj, element);
    }
}
