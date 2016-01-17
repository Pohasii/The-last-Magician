using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    Menu menu;
    Vector3 MoveVector;
    GameObject DraggingObj;
    public static bool isDrag;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        menu = GetComponent<Menu>();
        isDrag = false;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        MoveSpells();
        MoveElements();
    }

    void MoveSpells()
    {
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag == "Spell")
            {
                if (!isDrag)
                    menu.ShowToolTip(hit.transform.GetComponent<RectTransform>().localPosition, hit.transform.GetComponent<SpellScript>().spell);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    isDrag = true;
                    DraggingObj = hit.collider.gameObject;
                    DraggingObj.GetComponent<RectTransform>().localPosition -= Vector3.forward;

                    SpellScript tempSpellScript = DraggingObj.GetComponent<SpellScript>();

                    Menu.SMenu.SpellInSlot[tempSpellScript.inSlot] = new Spell();
                    Menu.SMenu.SpellsObjInSlot[tempSpellScript.inSlot] = null;
                }
            }
            else
                if (hit.collider.tag != "Element")
                {
                    menu.CloseToolTip();
                }

            if (isDrag && DraggingObj.tag == "Spell")
            {
                MoveVector = new Vector3(hit.point.x, hit.point.y, DraggingObj.transform.position.z);
                ReshuffleSpell(hit);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && DraggingObj && DraggingObj.tag == "Spell")
        {
            isDrag = false;
        }

        if (DraggingObj && isDrag)
        {
            DraggingObj.transform.position = Vector3.Lerp(DraggingObj.transform.position, MoveVector, 10 * Time.deltaTime);
        }
    }

    void MoveElements()
    {
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag == "Element")
            {
                if (!isDrag)
                    menu.ShowToolTip(hit.transform.GetComponent<RectTransform>().localPosition, hit.transform.GetComponent<ElementScript>().element);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    isDrag = true;
                    DraggingObj = hit.collider.gameObject;
                    DraggingObj.GetComponent<Collider>().enabled = false;
                    DraggingObj.GetComponent<RectTransform>().localPosition -= Vector3.forward;
                    ElementScript tempEScript = DraggingObj.GetComponent<ElementScript>();

                    if (tempEScript.InSlot)
                    {
                        menu.ElementsInSlots[tempEScript.inSlot] = new Element();
                        menu.ElementsObjInSlot[tempEScript.inSlot] = null;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Mouse1) && hit.collider.GetComponent<ElementScript>().InSlot)
                {
                    menu.ElementsInSlots[hit.collider.GetComponent<ElementScript>().inSlot] = new Element();
                    menu.ElementsObjInSlot[hit.collider.GetComponent<ElementScript>().inSlot] = null;
                }

                if (Input.GetKeyDown(KeyCode.Mouse1) && !hit.collider.GetComponent<ElementScript>().InSlot)
                {
                    menu.ElementReshuffle(hit.collider.GetComponent<ElementScript>().inSlot, hit.collider.gameObject, true);
                }
            }
            else
                if (hit.collider.tag != "Spell")
                {
                    menu.CloseToolTip();
                }

            if (isDrag && DraggingObj.tag == "Element")
            {
                MoveVector = new Vector3(hit.point.x, hit.point.y, DraggingObj.transform.position.z);
                ReshuffleElement(hit);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && DraggingObj && DraggingObj.tag == "Element")
        {
            DraggingObj.GetComponent<Collider>().enabled = true;
            isDrag = false;
        }

        if (DraggingObj && isDrag)
        {
            DraggingObj.transform.position = Vector3.Lerp(DraggingObj.transform.position, MoveVector, 10 * Time.deltaTime);
        }
    }

    public void MoveThisSpell(int inSlot, ref RectTransform ObjTransform)
    {
        if (!isDrag)
        {
            Vector3 WayPoint = Menu.SMenu.SlotsForSpells[inSlot].GetComponent<RectTransform>().position;
            ObjTransform.position = Vector3.Lerp(ObjTransform.position, WayPoint, 0.3f);
            ObjTransform.localPosition -= Vector3.forward;
        }
    }

    public void MoveThisElement(int inSlot, ref RectTransform ObjTransform, Vector3 StartPos)
    {
        Vector3 WayPoint = StartPos;
        if (!isDrag)
        {
            if (menu.ElementsObjInSlot[inSlot] == ObjTransform.gameObject)
            {
                ObjTransform.GetComponent<ElementScript>().InSlot = true;
            }
            else
            {
                ObjTransform.GetComponent<ElementScript>().InSlot = false;
            }

            if (ObjTransform.GetComponent<ElementScript>().InSlot)
            {
                WayPoint = menu.SlotsForElements[inSlot].GetComponent<RectTransform>().position;
                ObjTransform.position = Vector3.Lerp(ObjTransform.position, WayPoint, 0.3f);
                ObjTransform.localPosition -= Vector3.forward;
            }
            else
            {
                ObjTransform.anchoredPosition3D = Vector3.Lerp(ObjTransform.anchoredPosition3D, StartPos - Vector3.forward, 0.3f);
            }
        }
    }

    void ReshuffleSpell(RaycastHit hit)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (hit.collider.tag == "SlotForSpells")
            {
                Slot tempESlot = hit.collider.GetComponent<Slot>();
                menu.SpellReshuffle(tempESlot.CurSlot, DraggingObj);
                MoveVector = hit.collider.GetComponent<RectTransform>().position;
            }
            else
                if (hit.collider.tag == "BackGround")
                {
                    menu.SpellReshuffle(DraggingObj.GetComponent<SpellScript>().inSlot, DraggingObj);
                }

        }
    }

    void ReshuffleElement(RaycastHit hit)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (hit.collider.tag == "SlotForElements")
            {
                Slot tempESlot = hit.collider.GetComponent<Slot>();
                menu.ElementReshuffle(tempESlot.CurSlot, DraggingObj, false);
            }
            if (hit.collider.tag == "Element")
            {
                ElementScript tempEScript = DraggingObj.GetComponent<ElementScript>();
                if (tempEScript.InSlot)
                    menu.ElementReshuffle(tempEScript.inSlot, hit.collider.gameObject, false);
            }

            if (hit.collider.tag == "BackGround")
            {
                DraggingObj.GetComponent<ElementScript>().InSlot = false;
            }
        }
    }
}