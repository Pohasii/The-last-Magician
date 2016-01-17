using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spell
{
    string SpellName;
    string SpellDescription;
    float SpellLifeTime;
    List<Element> ComponentsOfSpell;
    Material UImaterial;
    Sprite SpellImage;

    public static float castTimer;
    public static bool isCast;
    protected float SpellCastTime;

    public List<SpellPoints> sp = new List<SpellPoints>();
    public GameObject PointObj;//префаб точки
    public static bool keyClick;
    public static PlayerScript playerScript;

    public string SpellName1
    {
        get { return SpellName; }
        set { SpellName = value; }
    }
    public string SpellDescription1
    {
        get { return SpellDescription; }
        set { SpellDescription = value; }
    }
    public float SpellLifeTime1
    {
        get { return SpellLifeTime; }
        set { SpellLifeTime = value; }
    }

    public Material Material1
    {
        get { return UImaterial; }
        set { UImaterial = value; }
    }
    public Sprite SpellImage1
    {
        get { return SpellImage; }
        set { SpellImage = value; }
    }
    public List<Element> ComponentsOfSpell1
    {
        get { return ComponentsOfSpell; }
        set { ComponentsOfSpell = value; }
    }

    float radius;
    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    float ExplosionForce;
    public float ExplosionForce1
    {
        get { return ExplosionForce; }
        set { ExplosionForce = value; }
    }

    public Spell()
    { }

    public Spell(string p_Name, string p_Description, List<Element> Components, float CastTime, float p_LifeTime)
    {
        SpellName = p_Name;
        SpellDescription = p_Description;
        ComponentsOfSpell = Components;
        SpellCastTime = CastTime;
        SpellLifeTime = p_LifeTime;
        UImaterial = Resources.Load<Material>("SpellMaterial/" + p_Name);
        SpellImage = Resources.Load<Sprite>("SpellImage/" + p_Name);
        isCast = false;
    }

    public Spell(string p_Name, string p_Description, float CastTime, float p_LifeTime)
    {
        SpellName = p_Name;
        SpellDescription = p_Description;
        SpellCastTime = CastTime;
        SpellLifeTime = p_LifeTime;
        isCast = false;
    }

    public virtual void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        PlayerScript.playerScript.anim.SetTrigger("Cast");
    }

    public void CastSpellWithPoint(Transform SpawnPos, Transform cameraTransform)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (playerScript.CheckRuneCD(this))
            {
                if (sp.Count > 0)//если экземпляров класса SpellPoints больше чем 1, то проверят возможность создания нового екземпляра
                {
                    if (!sp[sp.Count - 1].PointsSetup)
                        sp.Add(new SpellPoints());
                    else//убить последний экземпляр класса SpellPoints
                    {
                        RemoveSpellPoints();
                    }
                }
                else
                    sp.Add(new SpellPoints());//если экземпляров класса SpellPoints еще нет, то создает новый экземпляр этого класса
            }
            else
            {
                CharacterUIController.SetTextTrigger("Not enough runes", Color.red, 25);
            }
        }
        SpellCastInvoke(SpawnPos, cameraTransform);
    }

    public virtual void SpellCastBase(Transform SpawnPos, Transform cameraTransform)
    {
    }

    public virtual void SpellCastInvoke(Transform SpawnPos, Transform cameraTransform)
    {
        if (CastTimer(ref castTimer, ref isCast))
        {
            SpellCastBase(SpawnPos, cameraTransform);
            StopCast();
        }
    }

    public void RemoveSpellPointsBase()
    {
        if (sp.Count > 0)
        {
            if (sp[sp.Count - 1].PointsSetup)
            {
                sp[sp.Count - 1].DestroyPoints();
                sp[sp.Count - 1] = null;
                sp.RemoveAt(sp.Count - 1);
            }
        }
    }

    public void RemoveSpellPoints(SpellPoints p_sp)
    {
        RemoveSpellPointsBase();
        sp.Remove(p_sp);
    }

    public void RemoveSpellPoints()
    {
        playerScript.RuneCoolDown(this, true);
        RemoveSpellPointsBase();
    }

    bool CastTimer(ref float p_CastTimer, ref bool p_isCast)
    {
        PlayerScript.OnPlayerMove += StopCast;
        if (p_isCast)
        {
            playerScript.CastBar.gameObject.SetActive(true);
            if (SpellCastTime == 0)
                return true;
            p_CastTimer += Time.deltaTime;
            playerScript.CastBar.maxValue = SpellCastTime;
            playerScript.CastBar.value = p_CastTimer;

            if (p_CastTimer >= SpellCastTime)
                return true;
        }
        return false;
    }
    public void StopCast()
    {
        playerScript.CastBar.gameObject.SetActive(false);
        isCast = false;
        castTimer = 0;
        PlayerScript.OnPlayerMove -= StopCast;
    }
}

[System.Serializable]
public class HZSpell : Spell
{
    float SpellDamage;
    public float SpellDamage1
    {
        get { return SpellDamage; }
        set { SpellDamage = value; }
    }

    public SpellDamageType SpellDamageType;

    GameObject SpellObj;//префаб скила
    public GameObject SpellObj1
    {
        get { return SpellObj; }
        set { SpellObj = value; }
    }

    public HZSpell(string p_Name, string p_Description, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, CastTime, p_LifeTime)
    {
        SpellDamage = Damage;
        SpellDamageType = DamageType;

        SpellObj = Resources.Load<GameObject>("SpellsPrefab/" + p_Name);
    }

    public HZSpell(string p_Name, string p_Description, GameObject pointObj, List<Element> Components, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, Components, CastTime, p_LifeTime)
    {
        PointObj = pointObj;
        SpellDamage = Damage;
        SpellDamageType = DamageType;

        SpellObj = Resources.Load<GameObject>("SpellsPrefab/" + p_Name);
    }

    public HZSpell(string p_Name, string p_Description, List<Element> Components, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, Components, CastTime, p_LifeTime)
    {
        SpellDamage = Damage;
        SpellDamageType = DamageType;

        SpellObj = Resources.Load<GameObject>("SpellsPrefab/" + p_Name);
    }

    public HZSpell(string p_Name, string p_Description, List<Element> Components, float CastTime, float p_LifeTime)
        : base(p_Name, p_Description, Components, CastTime, p_LifeTime)
    {
        SpellObj = Resources.Load<GameObject>("SpellsPrefab/" + p_Name);
    }
}

[System.Serializable]
public class SWNP : HZSpell
{
    public SWNP(string p_Name, string p_Description, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, Damage, CastTime, p_LifeTime, DamageType)
    {

    }

    public SWNP(string p_Name, string p_Description, List<Element> Components, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, Components, Damage, CastTime, p_LifeTime, DamageType)
    {
    }

    public SWNP(string p_Name, string p_Description, List<Element> Components, float p_CastTime, float p_Radius, float p_ExpForce, float p_LifeTime)
        : base(p_Name, p_Description, Components, p_CastTime, p_LifeTime)
    {
        ExplosionForce1 = p_ExpForce;
        Radius = p_Radius;
    }

    public void Attac(Transform SpawnPos, Transform cameraTransform)
    {
        base.SpellCast(SpawnPos, cameraTransform);
        GameObject ob = (GameObject)GameObject.Instantiate(SpellObj1, SpawnPos.position, cameraTransform.rotation);
        ob.SendMessage("SpellSetap", this);
    }

    public override void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        if (keyClick)
        {
            base.SpellCast(SpawnPos, cameraTransform);
            if (playerScript.CheckRuneCD(this))
                isCast = true;
            else
                CharacterUIController.SetTextTrigger("Not enough runes", Color.red, 25);
        }
        base.SpellCastInvoke(SpawnPos, cameraTransform);
    }

    public override void SpellCastBase(Transform SpawnPos, Transform cameraTransform)
    {
        GameObject ob = (GameObject)GameObject.Instantiate(SpellObj1, SpawnPos.position, cameraTransform.rotation);
        ob.SendMessage("SpellSetap", this);
        playerScript.RuneCoolDown(this);
    }
}

public enum SpellDamageType { Fire, Frost, Arcane}

[System.Serializable]
public class SWOP : HZSpell
{
    public SWOP(string p_Name, string p_Description, GameObject pointObj, List<Element> Components, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, pointObj, Components, Damage, CastTime, p_LifeTime, DamageType)
    {
    }

    public SWOP(string p_Name, string p_Description, GameObject pointObj, List<Element> Components, float p_Radius, float p_ExpForce, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : this(p_Name, p_Description, pointObj, Components, Damage, CastTime, p_LifeTime, DamageType)
    {
        ExplosionForce1 = p_ExpForce;
        Radius = p_Radius;
    }

    bool SpellPointInRange;
    public override void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        base.CastSpellWithPoint(SpawnPos, cameraTransform);
        for (int i = 0; i < sp.Count; i++)
            SpellPointInRange = sp[i].SetapPoint(SpawnPos, cameraTransform, this);//вызов у последнего экземпляра класса SpellPoint функции уставки точек(возвращает True, если возможно поставить точку, т.е. дистнция от игрока до точки меньше 10)
    }

    public override void SpellCastInvoke(Transform SpawnPos, Transform cameraTransform)
    {
        if (keyClick && sp.Count > 0 && sp[sp.Count - 1].PointsSetup && SpellPointInRange)
            isCast = true;
        base.SpellCastInvoke(SpawnPos, cameraTransform);
    }

    public override void SpellCastBase(Transform SpawnPos, Transform cameraTransform)
    {
        GameObject ob = (GameObject)GameObject.Instantiate(SpellObj1, sp[sp.Count - 1].MainPoint.transform.position - Vector3.up, Quaternion.identity);
        ob.SendMessage("SpellSetap", this);
        playerScript.RuneCoolDown(this);
        RemoveSpellPoints(sp[sp.Count - 1]);
    }
}

[System.Serializable]
public class SWTP : HZSpell
{
    public SWTP(string p_Name, string p_Description, GameObject pointObj, List<Element> Components, float Damage, float CastTime, float p_LifeTime, SpellDamageType DamageType)
        : base(p_Name, p_Description, pointObj, Components, Damage, CastTime, p_LifeTime, DamageType)
    {

    }

    public override void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        base.CastSpellWithPoint(SpawnPos, cameraTransform);
        for (int i = 0; i < sp.Count; i++)
            sp[i].SetapPoints(SpawnPos, cameraTransform, this);//вызов у последнего экземпляра класса SpellPoints функции уставки точек
    }

    public override void SpellCastInvoke(Transform SpawnPos, Transform cameraTransform)
    {
        if (keyClick && sp[sp.Count - 1].PointsReady)
            isCast = true;
        base.SpellCastInvoke(SpawnPos, cameraTransform);
    }

    public override void SpellCastBase(Transform SpawnPos, Transform cameraTransform)
    {
        GameObject ob = (GameObject)GameObject.Instantiate(SpellObj1, sp[sp.Count - 1].Point1Obj.transform.position, sp[sp.Count - 1].Point1Obj.transform.rotation);
        ob.SendMessage("SpellSetap", this);
        sp[sp.Count - 1].DestroyPoints();
        RemoveSpellPoints();
    }
}

[System.Serializable]
public class MovementSpell : Spell
{
    float SpellTeleportDistance;
    LayerMask BarrierMask = LayerMask.GetMask("Barrier");
    LayerMask FloorMask = LayerMask.GetMask("Floor");

    public MovementSpell(string p_Name, string p_Description, List<Element> Components, float CastTime, float p_LifeTime, float TeleportDistance)
        : base(p_Name, p_Description, Components, CastTime, p_LifeTime)
    {
        SpellTeleportDistance = TeleportDistance;
    }
    //GameObject lObj;
    //LineRenderer Lline;Для визуализации дальности блинка
    //GameObject ob;
    public override void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        /*   if (!lObj)                                                                    Для визуализации дальности блинка
           {
               lObj = new GameObject();
               Lline = lObj.AddComponent<LineRenderer>();
               Lline.SetWidth(0.4f, 0.4f);
               ob = GameObject.CreatePrimitive(PrimitiveType.Sphere);
               ob.transform.localScale = Vector3.one * 2;
           }
           else
           {
               ob.transform.position = cameraTransform.position + cameraTransform.forward * SpellTeleportDistance;
               Lline.SetPosition(0, cameraTransform.position);
               Lline.SetPosition(1, cameraTransform.position + cameraTransform.forward * SpellTeleportDistance);
           }*/

        if (keyClick)
            if (playerScript.CheckRuneCD(this))
                isCast = true;
            else
                CharacterUIController.SetTextTrigger("Not enough runes", Color.red, 25);
        base.SpellCastInvoke(SpawnPos, cameraTransform);
    }

    public override void SpellCastBase(Transform myTransform, Transform cameraTransform)
    {
        if (playerScript.CheckRuneCD(this))
        {
            playerScript.RuneCoolDown(this);
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, SpellTeleportDistance, FloorMask) && !Physics.Raycast(ray, out hit, SpellTeleportDistance, BarrierMask))
            {
                myTransform.position = new Vector3(hit.point.x, myTransform.position.y, hit.point.z);
            }
            else
            {
                Vector3 spherePos;
                spherePos = cameraTransform.position + cameraTransform.forward * (SpellTeleportDistance - 2);
                Collider[] coll = Physics.OverlapSphere(spherePos, 2, BarrierMask);
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
    Vector3 pointsPos;//позиция точек
    private bool pointsSetup, //пока true можно становливать точки
        pointsReady = false,//если true - две точки установлены
        once = false;//для одноразового срабатывания функции
    public bool PointsReady
    {
        get { return pointsReady; }
        set { pointsReady = value; }
    }
    public bool PointsSetup
    {
        get { return pointsSetup; }
        set { pointsSetup = value; }
    }

    GameObject point1Obj, poin2Obj, mainPoint;//объекты точек
    public GameObject MainPoint
    {
        get { return mainPoint; }
        set { mainPoint = value; }
    }
    public GameObject Point1Obj
    {
        get { return point1Obj; }
        set { point1Obj = value; }
    }

    GameObject lineObj;//бъект на котором висит компонент LineRenderer
    LineRenderer line, line1, mainLine;//ссылка на компонент LineRenderer
    public static Material lineMaterial;

    PlayerScript playerScript;

    Ray ray;
    RaycastHit hit;

    public SpellPoints()
    {
        once = false; pointsSetup = false; pointsReady = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void MainPointSpawn(Transform SpawnPos, Transform cameraTransform, Spell thisSpell)
    {
        if (!once)//одноразовый спавн главной точки
        {
            MainPoint = (GameObject)GameObject.Instantiate(thisSpell.PointObj, hit.point + Vector3.up, Quaternion.identity);
            once = true;
            mainLine = MainPoint.AddComponent<LineRenderer>();
            mainLine.SetWidth(0.05f, 0.05f);
        }
        pointsPos = hit.point + Vector3.up;
        MainPoint.transform.position = pointsPos;
    }

    public bool SetapPoint(Transform SpawnPos, Transform cameraTransform, Spell thisSpell)
    {
        if (!PointsSetup && !PointsReady)
        {
            PointsSetup = true;
            once = false;
        }
        if (PointsSetup)
        {
            ray = new Ray(SpawnPos.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Barrier")) || Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                MainPointSpawn(SpawnPos, cameraTransform, thisSpell);
            }
        }
        if (MainPoint)
        {
            mainLine.SetPosition(0, pointsPos);
            mainLine.SetPosition(1, SpawnPos.position);
            ColorByDistance(pointsPos, SpawnPos.position, ref mainLine);
            if (Vector3.Distance(MainPoint.transform.position, SpawnPos.position) > 10)
                return false;
        }
        return true;
    }

    public void SetapPoints(Transform SpawnPos, Transform cameraTransform, Spell thisSpell)
    {
        if (!PointsSetup && !PointsReady)
        {
            PointsSetup = true;
            once = false;
        }
        if (PointsSetup)
        {
            ray = new Ray(SpawnPos.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Barrier")) || Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                MainPointSpawn(SpawnPos, cameraTransform, thisSpell);
                if (Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(pointsPos, SpawnPos.position) < 10)
                {
                    if (!Point1Obj)//спавн первой точки
                    {
                        Point1Obj = (GameObject)GameObject.Instantiate(thisSpell.PointObj, pointsPos, Quaternion.identity);
                        lineObj = new GameObject();
                        line = lineObj.AddComponent<LineRenderer>();
                        line.SetWidth(0.05f, 0.05f);
                        line.SetPosition(1, pointsPos);

                        line1 = Point1Obj.AddComponent<LineRenderer>();
                        line1.SetWidth(0.05f, 0.05f);
                        line1.SetPosition(0, Point1Obj.transform.position);
                        playerScript.RuneCoolDown(thisSpell, false);
                    }
                    else//если первая точка установлена, то устанавливает вторую точку
                        if (Vector3.Distance(pointsPos, Point1Obj.transform.position) < 10)
                        {
                            poin2Obj = (GameObject)GameObject.Instantiate(thisSpell.PointObj, pointsPos, Quaternion.identity);
                            Point1Obj.transform.LookAt(poin2Obj.transform);
                            GameObject.Destroy(MainPoint);
                            //GameObject.Destroy(line1);
                            PointsSetup = false;
                            PointsReady = true;
                        }
                }
            }
        }
        if (MainPoint)
        {
            mainLine.SetPosition(0, pointsPos);
            mainLine.SetPosition(1, SpawnPos.position);
            ColorByDistance(pointsPos, SpawnPos.position, ref mainLine);
        }

        if (line1)
        {
            line1.SetPosition(1, pointsPos);
            ColorByDistance(pointsPos, Point1Obj.transform.position, ref line1);
        }
        if (lineObj)
        {
            line.SetPosition(0, SpawnPos.position);
            ColorByDistance(SpawnPos.position, Point1Obj.transform.position, ref line);

            if (Vector3.Distance(SpawnPos.position, Point1Obj.transform.position) > 15)
            {
                playerScript.RuneCoolDown(thisSpell, true);
                DestroyPoints();
                thisSpell.RemoveSpellPoints(this);
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

    public void DestroyPoints()
    {
        if (MainPoint) GameObject.Destroy(MainPoint);
        if (Point1Obj) GameObject.Destroy(Point1Obj);
        if (poin2Obj) GameObject.Destroy(poin2Obj);
        if (lineObj) GameObject.Destroy(lineObj);
    }
}