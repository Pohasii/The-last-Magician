using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spell
{
    private string SpellName;
    public string SpellName1
    {
        get { return SpellName; }
        set { SpellName = value; }
    }

    private string SpellDescription;
    public string SpellDescription1
    {
        get { return SpellDescription; }
        set { SpellDescription = value; }
    }

    protected float SpellCastTime;

    private float SpellLifeTime;
    public float SpellLifeTime1
    {
        get { return SpellLifeTime; }
        set { SpellLifeTime = value; }
    }

    Vector3 StartPos;
    public Vector3 StartPos1
    {
        get { return StartPos; }
        set { StartPos = value; }
    }

    Material material;
    public Material Material1
    {
        get { return material; }
        set { material = value; }
    }

    private List<Element> ComponentsOfSpell;
    public List<Element> ComponentsOfSpell1
    {
        get { return ComponentsOfSpell; }
        set { ComponentsOfSpell = value; }
    }

    public List<SpellPoints> sp = new List<SpellPoints>();

    public enum SpellBehaviourType { Single, DOT }
    private SpellBehaviourType Sbt;
    public SpellBehaviourType SBT
    {
        get { return Sbt; }
        set { Sbt = value; }
    }

    public enum SpellCastType { NoPoints, OnePoint, TwoPoints }
    private SpellCastType Sct;
    public SpellCastType SCT
    {
        get { return Sct; }
        set { Sct = value; }
    }

    protected PlayerScript playerScript;
    public GameObject PointObj;//префаб точки

    public Spell()
    { }

    public Spell(SpellCastType p_SCT, SpellBehaviourType p_SBT, string p_Name, string p_Description, Material p_Material, List<Element> Components, float CastTime, float p_LifeTime)
    {
        Sct = p_SCT;
        Sbt = p_SBT;
        SpellName = p_Name;
        SpellDescription = p_Description;
        material = p_Material;
        ComponentsOfSpell = Components;
        SpellCastTime = CastTime;
        SpellLifeTime = p_LifeTime;
    }

    public virtual void StartPointsSetup(Transform SpawnPos, Transform cameraTransform)
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (Input.GetKeyDown(KeyCode.Q) && playerScript.CheckRuneCD(this))
        {
            if (sp.Count > 0)//если экземпляров класса SpellPoints больше чем 1, то проверят возможность создания нового екземпляра
            {
                if (!sp[sp.Count - 1].pointsSetup)
                    sp.Add(new SpellPoints());
                else//убить последний экземпляр класса SpellPoints
                {
                    RemoveSpellPoints();
                }
            }
            else
                sp.Add(new SpellPoints());//если экземпляров класса SpellPoints еще нет, то создает новый экземпляр этого класса
        }
        for (int i = 0; i < sp.Count; i++)
            sp[i].SetapPoints(SpawnPos, cameraTransform, this);//вызов у последнего экземпляра класса SpellPoints функции уставки точек
    }

    public virtual void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {

    }

    public virtual void RemoveSpellPoints(SpellPoints p_sp)
    {
        sp.Remove(p_sp);
        if (sp.Count > 0)
        {
            if (sp[sp.Count - 1].pointsSetup)
            {
                sp[sp.Count - 1].DestroyPoints(true);
                sp[sp.Count - 1] = null;
                sp.RemoveAt(sp.Count - 1);
            }
        }
    }

    public virtual void RemoveSpellPoints()
    {
        playerScript.RuneCoolDown(this, true);
        if (sp.Count > 0)
        {
            if (sp[sp.Count - 1].pointsSetup)
            {
                sp[sp.Count - 1].DestroyPoints(true);
                sp[sp.Count - 1] = null;
                sp.RemoveAt(sp.Count - 1);
            }
        }
    }

    public void SpellBehaviour(Transform SpellTransform)
    {
        if (Sbt == SpellBehaviourType.Single)
        {
            SpellTransform.Translate(Vector3.forward * 2, Space.Self);
            GameObject.Destroy(SpellTransform.gameObject, SpellLifeTime);
        }
        else
            GameObject.Destroy(SpellTransform.gameObject, SpellLifeTime);
    }
}

[System.Serializable]
public class DamageSpell : Spell
{
    float SpellDamage;
    public float SpellDamage1
    {
        get { return SpellDamage; }
        set { SpellDamage = value; }
    }

    SpellDamageTypes SpellDamageType;
    public enum SpellDamageTypes { Fire, Frost }

    GameObject SpellObj;//префаб скила
    public GameObject SpellObj1
    {
        get { return SpellObj; }
        set { SpellObj = value; }
    }

    public DamageSpell(SpellCastType p_SCT, SpellBehaviourType p_SBT, string p_Name, string p_Description, Material p_Material, GameObject pointObj, GameObject p_SpellObj, List<Element> Components, float Damage, float CastTime, float p_LifeTime, SpellDamageTypes DamageType)
        : base(p_SCT, p_SBT, p_Name, p_Description, p_Material, Components, CastTime, p_LifeTime)
    {
        PointObj = pointObj;
        SpellDamage = Damage;
        SpellObj = p_SpellObj;
        SpellDamageType = DamageType;
    }

    public override void StartPointsSetup(Transform SpawnPos, Transform cameraTransform)
    {
        base.StartPointsSetup(SpawnPos, cameraTransform);
    }

    public override void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (SCT == SpellCastType.NoPoints)
        {
            base.SpellCast(SpawnPos, cameraTransform);
            if (playerScript.CheckRuneCD(this))
            {
                GameObject ob = (GameObject)GameObject.Instantiate(SpellObj, SpawnPos.position, cameraTransform.rotation);
                ob.GetComponent<SpellBehaviour>().spell = this;
                playerScript.RuneCoolDown(this);
            }
        }
        else
            if (SCT == SpellCastType.OnePoint)
            {
                if (sp[sp.Count - 1].pointsSetup)
                {
                    GameObject ob = (GameObject)GameObject.Instantiate(SpellObj, sp[sp.Count - 1].mainPoint.transform.position - Vector3.up, Quaternion.identity);
                    ob.GetComponent<SpellBehaviour>().spell = this;
                    playerScript.RuneCoolDown(this, true);
                    sp[sp.Count - 1].DestroyPoints(true);
                    RemoveSpellPoints(sp[sp.Count - 1]);
                }
            }
            else
                if (SCT == SpellCastType.TwoPoints)
                {
                    foreach (SpellPoints SP in sp)
                    {
                        if (SP.pointsReady)
                        {
                            GameObject ob = (GameObject)GameObject.Instantiate(SpellObj, SP.point1Obj.transform.position, SP.point1Obj.transform.rotation);//SpawnPos.position, cameraTransform.rotation);//SP.pointsPos, SP.point1Obj.transform.rotation);
                            ob.GetComponent<SpellBehaviour>().spell = this;
                            SP.DestroyPoints(false);
                            playerScript.RuneCoolDown(this, true);
                        }
                    }
                    for (int i = 0; i < sp.Count; i++)
                    {
                        if (sp[i].pointsReady)
                        {
                            RemoveSpellPoints(sp[i]);
                        }
                    }
                }
    }
}

[System.Serializable]
public class MovementSpell : Spell
{
    float SpellTeleportDistance;
    LayerMask BarrierMask = LayerMask.GetMask("Barrier");
    LayerMask FloorMask = LayerMask.GetMask("Floor");

    public MovementSpell(SpellCastType p_SCT, SpellBehaviourType p_SBT, string p_Name, string p_Description, Material p_Material, List<Element> Components, float CastTime, float p_LifeTime, float TeleportDistance)
        : base(p_SCT, p_SBT, p_Name, p_Description, p_Material, Components, CastTime, p_LifeTime)
    {
        SpellTeleportDistance = TeleportDistance;
    }

    public override void SpellCast(Transform myTransform, Transform cameraTransform)
    {
        base.SpellCast(myTransform, cameraTransform);
        PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (playerScript.CheckRuneCD(this))
        {
            playerScript.RuneCoolDown(this);
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, SpellTeleportDistance, FloorMask))
            {
                myTransform.position = new Vector3(hit.point.x, myTransform.position.y, hit.point.z);
            }
            else
            {
                Collider[] coll = Physics.OverlapSphere(new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z + SpellTeleportDistance), 2, BarrierMask);
                if (coll.Length == 0)
                {
                    if (!Physics.Raycast(ray, out hit, Mathf.Infinity, FloorMask))
                    {
                        myTransform.position += cameraTransform.forward * SpellTeleportDistance;
                    }
                    else
                    {
                        myTransform.position += myTransform.forward * SpellTeleportDistance;
                    }
                }
                else
                    if (Physics.Raycast(ray, out hit, SpellTeleportDistance, BarrierMask))
                        myTransform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - myTransform.localScale.z / 2);
            }
        }
    }
}

[System.Serializable]
public class SpellPoints
{
    public Vector3 pointsPos;//позиция точек
    public bool pointsSetup, //пока true можно становливать точки
        pointsReady = false,//если true - две точки установлены
        once = false;//для одноразового срабатывания функции
    public GameObject point1Obj, poin2Obj, mainPoint;//объекты точек

    public GameObject lineObj, lineObj1;//объект на котором висит компонент LineRenderer
    public LineRenderer line, line1, mainLine;//ссылка на компонент LineRenderer
    public static Material lineMaterial;

    PlayerScript playerScript;

    public SpellPoints()
    {
        once = false; pointsSetup = false; pointsReady = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    public void SetapPoints(Transform SpawnPos, Transform cameraTransform, Spell thisSpell)
    {
        if ((thisSpell.SCT == Spell.SpellCastType.TwoPoints || thisSpell.SCT == Spell.SpellCastType.OnePoint))
        {
            if (!pointsSetup && !pointsReady)
            {
                pointsSetup = true;
                once = false;
                playerScript.RuneCoolDown(thisSpell, false);
            }
            if (pointsSetup)
            {
                Ray ray = new Ray(SpawnPos.position, cameraTransform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
                {
                    if (!once)//одноразовый спавн главной точки
                    {
                        mainPoint = (GameObject)GameObject.Instantiate(thisSpell.PointObj, hit.point + Vector3.up, Quaternion.identity);
                        once = true;
                        mainLine = mainPoint.AddComponent<LineRenderer>();
                        mainLine.SetWidth(0.05f, 0.05f);
                    }
                    pointsPos = hit.point + Vector3.up;
                    mainPoint.transform.position = pointsPos;

                    if (thisSpell.SCT == Spell.SpellCastType.TwoPoints && Input.GetKeyDown(KeyCode.E) && Vector3.Distance(pointsPos, SpawnPos.position) < 10)
                    {
                        if (!point1Obj)//спавн первой точки
                        {
                            point1Obj = (GameObject)GameObject.Instantiate(thisSpell.PointObj, pointsPos, Quaternion.identity);
                            lineObj = new GameObject();
                            line = lineObj.AddComponent<LineRenderer>();
                            line.SetWidth(0.05f, 0.05f);
                            line.SetPosition(1, pointsPos);

                            line1 = point1Obj.AddComponent<LineRenderer>();
                            line1.SetWidth(0.05f, 0.05f);
                            line1.SetPosition(0, point1Obj.transform.position);
                        }
                        else//если первая точка установлена, то устанавливает вторую точку
                            if (Vector3.Distance(pointsPos, point1Obj.transform.position) < 10)
                            {
                                poin2Obj = (GameObject)GameObject.Instantiate(thisSpell.PointObj, pointsPos, Quaternion.identity);
                                point1Obj.transform.LookAt(poin2Obj.transform);
                                GameObject.Destroy(mainPoint);
                                //GameObject.Destroy(line1);
                                pointsSetup = false;
                                pointsReady = true;
                            }
                    }
                }
            }
            if (mainPoint)
            {
                mainLine.SetPosition(0, pointsPos);
                mainLine.SetPosition(1, SpawnPos.position);
                ColorByDistance(pointsPos, SpawnPos.position, ref mainLine);
            }

            if (line1)
            {
                line1.SetPosition(1, pointsPos);
                ColorByDistance(pointsPos, point1Obj.transform.position, ref line1);
            }
            if (lineObj)
            {
                line.SetPosition(0, SpawnPos.position);
                ColorByDistance(SpawnPos.position, point1Obj.transform.position, ref line);

                if (Vector3.Distance(SpawnPos.position, point1Obj.transform.position) > 15)
                {
                    playerScript.RuneCoolDown(thisSpell, true);
                    DestroyPoints(true);
                    thisSpell.RemoveSpellPoints(this);
                }
            }
        }
    }

    void ColorByDistance(Vector3 firstPoint, Vector3 secondPoint, ref LineRenderer p_line)
    {
        float dist = Vector3.Distance(firstPoint, secondPoint);
        p_line.material = lineMaterial;
        if (dist < 5)
        {
            p_line.SetColors(Color.green, Color.green);
        }
        else
            if (dist < 10)
            {
                p_line.SetColors(Color.yellow, Color.yellow);
            }
            else
                if (dist < 15)
                {
                    p_line.SetColors(Color.red, Color.red);
                }
    }

    public void DestroyPoints(bool DestroyMain)
    {
        if (DestroyMain) GameObject.Destroy(mainPoint);
        if (point1Obj) GameObject.Destroy(point1Obj);
        if (poin2Obj) GameObject.Destroy(poin2Obj);
        if (lineObj) GameObject.Destroy(lineObj);
    }
}