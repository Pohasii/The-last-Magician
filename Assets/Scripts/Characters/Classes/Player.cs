using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : Character
{
    Transform myTransform;
    Rigidbody myRigidbody;

    Vector3 PlayerMovement;

    float flashSpeed = 5;
    Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    public Image damageImage;
    bool damaged = false;
    public bool isDead { get { return CurHP1 <= 0; } }

    Transform cameraTransform;
    private float cameraRotation;
    public float minimumY = -35;
    public float maximumY = 45;
    float rotationSpeed = 30;

    public Player(float p_HP, float p_HPRegen, float p_Armor,float p_MoveSpeed, SpellResistance p_MagResist)
        : base(0, p_HP, p_HPRegen, p_Armor,p_MoveSpeed, p_MagResist)
    {
        myTransform = PlayerScript.myTransform;
        myRigidbody = PlayerScript.myRigidBody;
        damageImage = PlayerScript.playerScript.DamageImage;
        hpSlider = PlayerScript.playerScript.HpSlider;

        MaxHPSet = MaxHP;

        cameraTransform = myTransform.GetChild(0);//Camera.main.transform;
    }

    public void Move(float h, float v)
    {
        PlayerMovement.Set(h, 0f, v);

        PlayerMovement = PlayerMovement.normalized * MoveSpeed1 * Time.deltaTime;
        PlayerMovement = myTransform.TransformDirection(PlayerMovement);

        myRigidbody.MovePosition(myTransform.position + PlayerMovement);
    }

    public void Turning()
    {
        myTransform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0) * Time.deltaTime);
        cameraRotation -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        cameraRotation = Mathf.Clamp(cameraRotation, minimumY, maximumY);
        cameraTransform.localEulerAngles = new Vector3(cameraRotation, cameraTransform.localEulerAngles.y, 0);
    }

    public override void Attac()
    {
        
    }

    public void DamagedEffect()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakePhysicalDamage(float Damage)
    {
        damaged = true;
        CurHP1 -= DamageCanculate(Damage);
    }
}
